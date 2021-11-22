using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIQRI.ITS.Domain.Helpers;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Domain.Services
{
    public class AssetService
    {
        public async Task<TransactionStatus> SaveAsset(Asset asset, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Asset saveAsset = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (asset.Id == 0)
                {
                    asset.DateCreated = DateTime.UtcNow;
                    asset.UserCreated = userProfile.UserName;
                    asset.DateLastUpdated = DateTime.UtcNow;
                    asset.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Assets.Add(asset);
                }
                else
                {
                    saveAsset = context.Assets.Single(a => a.Id == asset.Id);

                    if (saveAsset.UserId != asset.UserId)
                    {
                        createOwnerUpdate = true;
                    }

                    saveAsset.AssetType = asset.AssetType;
                    saveAsset.AssetNumber = asset.AssetNumber;
                    saveAsset.Manufacture = asset.Manufacture;
                    saveAsset.Model = asset.Model;
                    saveAsset.ManufactureSN = asset.ManufactureSN;
                    saveAsset.ComputerName = asset.ComputerName;
                    saveAsset.UserId = asset.UserId;
                    saveAsset.UserDisplayName = asset.UserDisplayName;
                    saveAsset.Group = asset.Group;
                    saveAsset.Customer = asset.Customer;
                    saveAsset.Availability = asset.Availability;
                    saveAsset.AssetStatus = asset.AssetStatus;
                    saveAsset.AssetOwner = asset.AssetOwner;
                    saveAsset.Vendor = asset.Vendor;
                    saveAsset.DatePurchasedOrleased = asset.DatePurchasedOrleased;
                    saveAsset.WarrantyPeriod = asset.WarrantyPeriod;
                    saveAsset.Location = asset.Location;
                    saveAsset.Memory = asset.Memory;
                    saveAsset.Processor = asset.Processor;
                    saveAsset.Speed = asset.Speed;
                    saveAsset.HDD = asset.HDD;
                    saveAsset.IsSSD = asset.IsSSD;
                    saveAsset.ProblemReported = asset.ProblemReported;
                    saveAsset.NOTE = asset.NOTE;
                    saveAsset.Rating = asset.Rating;
                    saveAsset.Screen = asset.Screen;
                    saveAsset.ConferenceRoom = asset.ConferenceRoom;
                    saveAsset.Building = asset.Building;
                    saveAsset.Floor = asset.Floor;
                    saveAsset.DeviceType = asset.DeviceType;
                    saveAsset.MobileName = asset.MobileName;
                    saveAsset.LeasePeriod = asset.LeasePeriod;
                    saveAsset.Cost = asset.Cost;
                    saveAsset.DateLastUpdated = DateTime.UtcNow;
                    saveAsset.UserLastUpdated = userProfile.UserName;
                }


                #region --- Creating owner update ---

                if (createOwnerUpdate)
                {
                    var assetOwner = new AssetOwnerHistory()
                    {
                        AssetId = asset.Id,
                        AssetNumber = asset.AssetNumber,
                        OwnerId = asset.UserId,
                        DateAssigned = DateTime.UtcNow,
                        DateCreated = DateTime.UtcNow,
                        DateLastUpdated = DateTime.UtcNow,
                        UserCreated = userProfile.UserName,
                        UserLastUpdated = userProfile.UserName
                    };
                    context.AssetOwnerHistories.Add(assetOwner);
                    
                }

                #endregion

                context.SaveChanges(userProfile.UserName);

                if (createOwnerUpdate)
                {
                    await SendOwnerUpdateEmailAsync(saveAsset);
                }
            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveModel(Model model, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Model saveModel = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (model.Id == 0)
                {
                    model.DateCreated = DateTime.UtcNow;
                    model.UserCreated = userProfile.UserName;
                    model.DateLastUpdated = DateTime.UtcNow;
                    model.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Models.Add(model);
                }
                else
                {
                    saveModel = context.Models.Single(a => a.Id == model.Id);

                    saveModel.Name = model.Name;
                    saveModel.DateLastUpdated = DateTime.UtcNow;
                    saveModel.UserLastUpdated = userProfile.UserName;
                }


                 context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }


        public async Task<TransactionStatus> SaveManufacturer(Manufacture manufacture, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Manufacture saveManufacture = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (manufacture.Id == 0)
                {
                    manufacture.DateCreated = DateTime.UtcNow;
                    manufacture.UserCreated = userProfile.UserName;
                    manufacture.DateLastUpdated = DateTime.UtcNow;
                    manufacture.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Manufactures.Add(manufacture);
                }
                else
                {
                    saveManufacture = context.Manufactures.Single(a => a.Id == manufacture.Id);

                    saveManufacture.Name = manufacture.Name;
                    saveManufacture.DateLastUpdated = DateTime.UtcNow;
                    saveManufacture.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveMemory(Memory memory, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Memory saveMemory = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (memory.Id == 0)
                {
                    memory.DateCreated = DateTime.UtcNow;
                    memory.UserCreated = userProfile.UserName;
                    memory.DateLastUpdated = DateTime.UtcNow;
                    memory.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Memories.Add(memory);
                }
                else
                {
                    saveMemory = context.Memories.Single(a => a.Id == memory.Id);

                    saveMemory.Name = memory.Name;
                    saveMemory.DateLastUpdated = DateTime.UtcNow;
                    saveMemory.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveProcessor(Processor processor, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Processor saveProcessor = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (processor.Id == 0)
                {
                    processor.DateCreated = DateTime.UtcNow;
                    processor.UserCreated = userProfile.UserName;
                    processor.DateLastUpdated = DateTime.UtcNow;
                    processor.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Processors.Add(processor);
                }
                else
                {
                    saveProcessor = context.Processors.Single(a => a.Id == processor.Id);

                    saveProcessor.Name = processor.Name;
                    saveProcessor.DateLastUpdated = DateTime.UtcNow;
                    saveProcessor.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveHardDisk(HardDisk hardDisk, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            HardDisk saveHardDisk = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (hardDisk.Id == 0)
                {
                    hardDisk.DateCreated = DateTime.UtcNow;
                    hardDisk.UserCreated = userProfile.UserName;
                    hardDisk.DateLastUpdated = DateTime.UtcNow;
                    hardDisk.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.HardDisks.Add(hardDisk);
                }
                else
                {
                    saveHardDisk = context.HardDisks.Single(a => a.Id == hardDisk.Id);

                    saveHardDisk.Name = hardDisk.Name;
                    saveHardDisk.DateLastUpdated = DateTime.UtcNow;
                    saveHardDisk.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveScreenSize(ScreenSize screenSize, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            ScreenSize saveScreenSize = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (screenSize.Id == 0)
                {
                    screenSize.DateCreated = DateTime.UtcNow;
                    screenSize.UserCreated = userProfile.UserName;
                    screenSize.DateLastUpdated = DateTime.UtcNow;
                    screenSize.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.ScreenSizes.Add(screenSize);
                }
                else
                {
                    saveScreenSize = context.ScreenSizes.Single(a => a.Id == screenSize.Id);

                    saveScreenSize.Name = screenSize.Name;
                    saveScreenSize.DateLastUpdated = DateTime.UtcNow;
                    saveScreenSize.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveVendor(Vendor vendor, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            Vendor saveVendor = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (vendor.Id == 0)
                {
                    vendor.DateCreated = DateTime.UtcNow;
                    vendor.DateLastUpdated = DateTime.UtcNow;
                    vendor.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.Vendors.Add(vendor);
                }
                else
                {
                    saveVendor = context.Vendors.Single(a => a.Id == vendor.Id);

                    saveVendor.Name = vendor.Name;
                    saveVendor.DateLastUpdated = DateTime.UtcNow;
                    saveVendor.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveLeasePeriod(LeasePeriod leasePeriod, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            LeasePeriod saveLeasePeriod = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (leasePeriod.Id == 0)
                {
                    leasePeriod.DateCreated = DateTime.UtcNow;
                    leasePeriod.DateLastUpdated = DateTime.UtcNow;
                    leasePeriod.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.LeasePeriods.Add(leasePeriod);
                }
                else
                {
                    saveLeasePeriod = context.LeasePeriods.Single(a => a.Id == leasePeriod.Id);

                    saveLeasePeriod.Name = leasePeriod.Name;
                    saveLeasePeriod.DateLastUpdated = DateTime.UtcNow;
                    saveLeasePeriod.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public async Task<TransactionStatus> SaveWarrantyPeriod(WarrantyPeriod warrantyPeriod, UserProfile userProfile)
        {
            var status = new TransactionStatus() { IsSuccessfull = true };
            WarrantyPeriod saveWarrantyPeriod = null;
            try
            {
                var context = new Context();
                var createOwnerUpdate = false;
                if (warrantyPeriod.Id == 0)
                {
                    warrantyPeriod.DateCreated = DateTime.UtcNow;
                    warrantyPeriod.DateLastUpdated = DateTime.UtcNow;
                    warrantyPeriod.UserLastUpdated = userProfile.UserName;
                    createOwnerUpdate = true;
                    context.WarrantyPeriods.Add(warrantyPeriod);
                }
                else
                {
                    saveWarrantyPeriod = context.WarrantyPeriods.Single(a => a.Id == warrantyPeriod.Id);

                    saveWarrantyPeriod.Name = warrantyPeriod.Name;
                    saveWarrantyPeriod.DateLastUpdated = DateTime.UtcNow;
                    saveWarrantyPeriod.UserLastUpdated = userProfile.UserName;
                }


                context.SaveChanges(userProfile.UserName);

            }
            catch (Exception exception)
            {
                status.IsSuccessfull = false;
                status.Message = exception.Message;
                status.Exception = exception;
            }

            return status;
        }

        public List<Asset> SearchAsset(SearchQueries.AssetSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Asset> query = context.Assets.Where(c => c.Archived == false);

                if (searchQuery.AssetStatus != null)
                {
                    query = query.Where(c => c.AssetStatus.Contains(searchQuery.AssetStatus));
                }
                if (searchQuery.AssetType != null)
                {
                    query = query.Where(c => c.AssetType == searchQuery.AssetType.Value);
                }
                if (searchQuery.AvailabilityType != null)
                {
                    query = query.Where(c => c.Availability == searchQuery.AvailabilityType.Value);
                }

                if (!string.IsNullOrEmpty(searchQuery.AssetNumber))
                {
                    query = query.Where(c => c.AssetNumber.Contains(searchQuery.AssetNumber));
                }
                if (!string.IsNullOrEmpty(searchQuery.Customer))
                {
                    query = query.Where(c => c.Customer.Contains(searchQuery.Customer));
                }
                if (!string.IsNullOrEmpty(searchQuery.Model))
                {
                    query = query.Where(c => c.Model.Contains(searchQuery.Model));
                }
                if (!string.IsNullOrEmpty(searchQuery.Manufacture))
                {
                    query = query.Where(c => c.Manufacture.Contains(searchQuery.Manufacture));
                }
                if (!string.IsNullOrEmpty(searchQuery.ManufactureSN))
                {
                    query = query.Where(c => c.ManufactureSN.Contains(searchQuery.ManufactureSN));
                }
                if (!string.IsNullOrEmpty(searchQuery.ComputerName))
                {
                    query = query.Where(c => c.ComputerName.Contains(searchQuery.ComputerName));
                }
                if (!string.IsNullOrEmpty(searchQuery.Group))
                {
                    query = query.Where(c => c.Group.Contains(searchQuery.Group));
                }
                if (!string.IsNullOrEmpty(searchQuery.ConferenceRoom))
                {
                    query = query.Where(c => c.ConferenceRoom.Contains(searchQuery.ConferenceRoom));
                }
                if (!string.IsNullOrEmpty(searchQuery.Building))
                {
                    query = query.Where(c => c.Building.Contains(searchQuery.Building));
                }
                if (!string.IsNullOrEmpty(searchQuery.UserId))
                {
                    query = query.Where(c => c.UserId.Contains(searchQuery.UserId));
                }
                if (!string.IsNullOrEmpty(searchQuery.UserName))
                {
                    query = query.Where(c => c.UserDisplayName.Contains(searchQuery.UserName));
                }
                if (!string.IsNullOrEmpty(searchQuery.LeasePeriod))
                {
                    query = query.Where(c => c.LeasePeriod.Contains(searchQuery.LeasePeriod));
                }
                if (searchQuery.DatePurchasedOrleasedFrom != null)
                {
                    query = query.Where(c => c.DatePurchasedOrleased != null && c.DatePurchasedOrleased >= searchQuery.DatePurchasedOrleasedFrom.Value);
                }
                if (searchQuery.DatePurchasedOrleasedTo != null)
                {
                    query = query.Where(c => c.DatePurchasedOrleased != null && c.DatePurchasedOrleased <= searchQuery.DatePurchasedOrleasedTo.Value);
                }
                if (searchQuery.Cost != null)
                {
                    query = query.Where(c => c.Cost != null && c.Cost <= searchQuery.Cost.Value);
                }

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.AssetNumber.Contains(searchQuery.GlobalText)
                        || c.ComputerName.Contains(searchQuery.GlobalText)
                        || c.ConferenceRoom.Contains(searchQuery.GlobalText)
                        || c.Customer.Contains(searchQuery.GlobalText)
                        || c.Group.Contains(searchQuery.GlobalText)
                        || c.Location.Contains(searchQuery.GlobalText)
                        || c.Manufacture.Contains(searchQuery.GlobalText)
                        || c.ManufactureSN.Contains(searchQuery.GlobalText)
                        || c.MobileName.Contains(searchQuery.GlobalText)
                        || c.Model.Contains(searchQuery.GlobalText)
                        || c.NOTE.Contains(searchQuery.GlobalText)
                        || c.UserId.Contains(searchQuery.GlobalText)
                        || c.UserDisplayName.Contains(searchQuery.GlobalText)
                        || c.Vendor.Contains(searchQuery.GlobalText)
                        || c.LeasePeriod.Contains(searchQuery.GlobalText)
                        || c.Cost.ToString().Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.AssetNumber).ToList();
            }
        }

        public List<Model> SearchModel(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Model> query = context.Models;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }
        public List<Manufacture> SearchManufaturer(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Manufacture> query = context.Manufactures;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }

        public List<Memory> SearchMemory(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Memory> query = context.Memories;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }

        public List<Processor> SearchProcessor(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Processor> query = context.Processors;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }

        public List<HardDisk> SearchHardDisk(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<HardDisk> query = context.HardDisks;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }
        public List<ScreenSize> SearchScreenSize(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<ScreenSize> query = context.ScreenSizes;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }
        public List<Vendor> SearchVendor(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<Vendor> query = context.Vendors;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }
        public List<LeasePeriod> SearchLeasePeriod(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<LeasePeriod> query = context.LeasePeriods;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }
        public List<WarrantyPeriod> SearchWarrantyPeriod(SearchQueries.ModelOrManufSearchQuery searchQuery)
        {
            using (var context = new Context())
            {
                IQueryable<WarrantyPeriod> query = context.WarrantyPeriods;

                if (!string.IsNullOrEmpty(searchQuery.GlobalText))
                {
                    query = query.Where(c => c.Name.Contains(searchQuery.GlobalText));
                }
                return query.OrderBy(a => a.Id).ToList();
            }
        }



        public Task SendOwnerUpdateEmailAsync(Asset asset)
        {
            return new EmailHelper().SendEMailsync(asset.UserId, "TIQRI Asset Inventory - New Allocation", string.Format("New asset allocated to you. <br><br>Asset ID: {0}<br>Type : {1}<br>Model: {2}<br><br><br>Thank you.<br><br><br>",
                asset.AssetNumber, asset.AssetType, asset.Model));
        }

    }
}
