using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TIQRI.ITS.Web.ViewModels
{
    public class ReportViewModel
    {
        public Report Report { get; set; }
        public EmbedConfiguration EmbedConfig { get; set; }
    }

    public enum ReportMode
    {
        NoReport,
        ExistingReport,
        NewReport
    }

    public class DatasetViewModel
    {
        public Dataset dataset { get; set; }
        public EmbedConfiguration EmbedConfig { get; set; }
    }

    public class DatasetsViewModel
    {
        public List<Dataset> Datasets { get; set; }
    }

    public class ReportsViewModel
    {
        public string ReportName { get; set; }
        public string ReportUser { get; set; }
        public List<Report> Reports { get; set; }
        public List<Dataset> Datasets { get; set; }
        public ReportMode ReportMode { get; set; }
        public ReportViewModel CurrentReport { get; set; }
        public DatasetViewModel CurrentDataset { get; set; }

        public bool ShowFilterPane { get; set; }
        public bool ShowReportTabs { get; set; }
    }

    public class EmbedConfiguration
    {
        public string Id { get; set; }
        public string DatasetId { get; set; }
        public string EmbedUrl { get; set; }
        public EmbedToken EmbedToken { get; set; }
        public int MinutesToExpiration
        {
            get
            {
                var minutesToExpiration = EmbedToken.Expiration.Value - DateTime.UtcNow;
                return minutesToExpiration.Minutes;
            }
        }
        public string ErrorMessage { get; internal set; }
    }

    public class PbiEmbeddedManager
    {
        static string aadAuthorizationEndpoint = "https://login.windows.net/common/oauth2/authorize";
        static string resourceUriPowerBi = "https://analysis.windows.net/powerbi/api";
        static string urlPowerBiRestApiRoot = "https://api.powerbi.com/";

        static string clientId = string.Empty;
        static string appWorkspaceId = string.Empty;
        static string pbiUserName = string.Empty;
        static string pbiUserPassword = string.Empty;


        public PbiEmbeddedManager(string clientIdPrm, string appWorkspaceIdPrm, string pbiUsernamePrm, string pbiPasswordPrm)
        {
            clientId = clientIdPrm;
            appWorkspaceId = appWorkspaceIdPrm;
            pbiUserName = pbiUsernamePrm;
            pbiUserPassword = pbiPasswordPrm;
        }

        static string GetAccessToken()
        {
            AuthenticationContext authContext = new AuthenticationContext(aadAuthorizationEndpoint);
            var userCredentials = new UserPasswordCredential(pbiUserName, pbiUserPassword);
            return authContext.AcquireTokenAsync(resourceUriPowerBi, clientId, userCredentials).Result.AccessToken;
        }

        static PowerBIClient GetPowerBiClient()
        {
            var tokenCredentials = new TokenCredentials(GetAccessToken(), "Bearer");
            return new PowerBIClient(new Uri(urlPowerBiRestApiRoot), tokenCredentials);
        }

        public async Task<ReportsViewModel> GetReports(string reportId, string datasetId, bool applyRls, string roles, string userName)
        {
            var client = GetPowerBiClient();

            var reports = (await client.Reports.GetReportsInGroupAsync(appWorkspaceId)).Value;
            var datasets = (await client.Datasets.GetDatasetsInGroupAsync(appWorkspaceId)).Value;

            var viewModel = new ReportsViewModel
            {
                Reports = reports.ToList(),
                Datasets = datasets.ToList()
            };

            if (string.IsNullOrEmpty(reportId) && viewModel.Reports != null && viewModel.Reports.Count > 0)
            {
                reportId = viewModel.Reports[0].Id;
            }

            viewModel.ReportMode = !string.IsNullOrEmpty(reportId) ? ReportMode.ExistingReport :
                                   !string.IsNullOrEmpty(datasetId) ? ReportMode.NewReport :
                                                                      ReportMode.NoReport;
            Report report = reports.Where(r => r.Id.ToLower() == reportId.ToLower()).First();


            GenerateTokenRequest generateTokenRequestParameters;
            // This is how you create embed token with effective identities
            if (applyRls)
            {
                var rls = new EffectiveIdentity(userName, new List<string> { report.DatasetId });
                if (!string.IsNullOrWhiteSpace(roles))
                {
                    var rolesList = new List<string>();
                    rolesList.AddRange(roles.Split(','));
                    rls.Roles = rolesList;
                }
                // Generate Embed Token with effective identities.
                generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view", identities: new List<EffectiveIdentity> { rls });
            }
            else
            {   // Generate Embed Token for reports without effective identities.
                generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
            }


            try
            {
                var tokenResponse = await client.Reports.GenerateTokenInGroupAsync(appWorkspaceId, report.Id, generateTokenRequestParameters);
                var embedConfig = new EmbedConfiguration()
                {
                    EmbedToken = tokenResponse,
                    EmbedUrl = report.EmbedUrl,
                    Id = report.Id
                };
                // add report data to view model
                viewModel.CurrentReport = new ReportViewModel
                {
                    Report = report,
                    EmbedConfig = embedConfig
                };
            }
            catch (Exception ex)
            {

                throw;
            }

            return viewModel;
        }

    }
}