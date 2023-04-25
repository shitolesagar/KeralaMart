using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;

namespace KeralaMiniMart.Service
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private IUnitOfWork _unitOfWork;

        public BannerService(IBannerRepository bannerRepository, IUnitOfWork unitOfWork)
        {
            _bannerRepository = bannerRepository;
            _unitOfWork = unitOfWork;
        }

        #region Add Banner
        /// <summary>
        /// This Method is used to add new banner
        /// </summary>
        /// <param name="model"></param>
        /// <param name="imageRelativePath"></param>
        /// <returns></returns>
        public async Task<int> AddBannerAsync(AddBannerViewModel model, string imageRelativePath)
        {
            Banner banner = new Banner()
            {
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                ExpiryDate = model.ExpireDate,
                StartDate = model.StartDate,
                Image = imageRelativePath,
                Title = model.Caption.Trim()
            };
            _bannerRepository.Add(banner);
            await _unitOfWork.SaveChangesAsync();
            return banner.Id;
        }
        #endregion

        #region Delete Banner
        /// <summary>
        /// This methos is used to delete banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DeleteBanner(int id)
        {
            var bannerToDelete = _bannerRepository.FindById(id);
            _bannerRepository.Remove(bannerToDelete);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region GetWrapperForIndexView
        /// <summary>
        /// This method is used to get index view of the banner
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<BannerWrapperViewModel> GetWrapperForIndexView(BannerFilter filter)
        {
            BannerWrapperViewModel ResponseModel = new BannerWrapperViewModel
            {
                TotalCount = _bannerRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Banner> list = await _bannerRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.BannerList = list.Select((x, index) => new BannerListViewModel
            {
                Id = x.Id,
                Caption = x.Title,
                CreatedDate = x.CreatedDateTime.ToStringDatePattern(),
                ExpireDate = x.ExpiryDate?.ToStringDatePattern(),
                ImagePath = x.Image,
                Number = ResponseModel.PagingData.FromRecord + index,
                IsExpired = filter.showExpired
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }
        #endregion

        #region getForEditAsync
        /// <summary>
        /// This is get method for edit banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AddBannerViewModel> getForEditAsync(int id)
        {
            var databaseModel = await _bannerRepository.FindByIdAsync(id);
            AddBannerViewModel model = new AddBannerViewModel()
            {
                Caption = databaseModel.Title,
                ImageUrl = databaseModel.Image,
                StartDate = databaseModel.StartDate?.Date,
                ExpireDate = databaseModel.ExpiryDate?.Date
            };
            return model;
        }
        #endregion

        #region EditBannerAsync
        /// <summary>
        /// This is post method for edit banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="imageRelativePath"></param>
        /// <returns></returns>
        public async Task<int> EditBannerAsync(int id, AddBannerViewModel model, string imageRelativePath)
        {
            Banner toUpdate = await _bannerRepository.FindByIdAsync(id);
            toUpdate.Title = model.Caption;
            toUpdate.StartDate = model.StartDate;
            toUpdate.ExpiryDate = model.ExpireDate;
            toUpdate.ModifiedDateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(imageRelativePath))
            {
                toUpdate.Image = imageRelativePath;
            }
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
        #endregion

        #region GetBannerDetails
        /// <summary>
        /// This method is used to fetch banner details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BannerDetailsViewModel> GetBannerDetails(int id)
        {
            BannerDetailsViewModel model;
            var banner = await _bannerRepository.FindByIdAsync(id);
            if (banner == null)
                return null;
            model = new BannerDetailsViewModel()
            {
                ImageUrl = banner.Image,
                Id = banner.Id,
                Caption = banner.Title,
                CreatedDate = banner.CreatedDateTime.ToStringDateTimePattern(),
                ModifiedDate = banner.CreatedDateTime.ToStringDateTimePattern() == banner.ModifiedDateTime.ToStringDateTimePattern() ? null : banner.ModifiedDateTime.ToStringDateTimePattern()
            };
            if (banner.StartDate != null)
                model.StartDate = banner.StartDate.Value.ToStringDatePattern();
            if (banner.ExpiryDate != null)
                model.ExpireDate = banner.ExpiryDate.Value.ToStringDatePattern();
            return model;
        }
        #endregion
    }
}
