
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;
using Microsoft.Extensions.Options;
//using KeralaMiniMart.Service.Utility;

namespace KeralaMiniMart.Service
{
    public class NotificationService : INotificationService
    {
        private readonly AppSettings _appSettings;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationDeliveryLocationRepository _notificationDeliveryLocationRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IUserDeviceInfoRepository _userDeviceInfoRepository;
        private readonly IDeliveryLocationRepository _deliveryLocationRepository;
        private readonly IFCMPushNotificationService _fCMPushNotificationService;

        public NotificationService(IOptions<AppSettings> options, INotificationRepository notificationRepository, INotificationDeliveryLocationRepository notificationDeliveryLocationRepository, IDeliveryLocationRepository deliveryLocationRepository, IUnitOfWork unitOfWork, IUserDeviceInfoRepository userDeviceInfoRepository, IFCMPushNotificationService fCMPushNotificationService)
        {
            _appSettings = options.Value;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _userDeviceInfoRepository = userDeviceInfoRepository;
            _fCMPushNotificationService = fCMPushNotificationService;
            _deliveryLocationRepository = deliveryLocationRepository;
            _notificationDeliveryLocationRepository = notificationDeliveryLocationRepository;
        }

        #region AddNotification
        /// <summary>
        /// This method is used to add notification
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddNotificationAsync(AddNotificationViewModel model)
        {
            Notification notification = new Notification()
            {
                CreatedDateTime = DateTime.Now,
                Title = model.Title,
                Message = model.Message,
                ImageUrl = model.ImageUrl,
                IsShowOnWeb = true,
                NotificationType = model.Type.ToLower() == StringConstants.TextNotification.ToLower() ? StringConstants.TextNotification : StringConstants.ImageNotification,
                CategoryId = model.CategoryId,
            };
            _notificationRepository.Add(notification);
            var result = await _unitOfWork.SaveChangesAsync();
            int NotiId = notification.Id;
            #region Store Area Data
            foreach (var location in model.SelectedDeliveryLocationIds)
            {
                NotificationDeliveryLocation obj = new NotificationDeliveryLocation();
                obj.NotificationId = notification.Id;
                obj.DeliveryLocationId = location;
                _notificationDeliveryLocationRepository.Add(obj);
            }
            _unitOfWork.SaveChanges();
            #endregion

            // send notiication
            List<UserDeviceInfo> registerIds;
            if (model.SelectedDeliveryLocationIds == null || model.SelectedDeliveryLocationIds.Count <= 0)
                registerIds = await _userDeviceInfoRepository.GetAllAsync();
            else
            {
                registerIds = _userDeviceInfoRepository.GetAllListFromAddress(model.SelectedDeliveryLocationIds);
                #region Store User Specific Notification
                var uniqueId = registerIds.Select(x => x.ApplicationUserId).Distinct();
                foreach (var item in uniqueId)
                {
                    notification = new Notification()
                    {
                        CreatedDateTime = DateTime.Now,
                        Title = model.Title,
                        Message = model.Message,
                        ImageUrl = model.ImageUrl,
                        ApplicationUserId = item,
                        NotificationType = model.Type.ToLower() == StringConstants.TextNotification.ToLower() ? StringConstants.TextNotification : StringConstants.ImageNotification,
                        CategoryId = model.CategoryId
                    };
                    _notificationRepository.Add(notification);
                }
                await _unitOfWork.SaveChangesAsync();
                #endregion
            }


            if (registerIds.Count > 0)
            {
                var deviceIds = registerIds.Select(x => x.DeviceId);
                SendPushNotification(deviceIds, notification);
            }
            return NotiId;
        }
        #endregion

        #region EditNotificationGet
        public AddNotificationViewModel GetForEditAsync(int id)
        {
            AddNotificationViewModel response;
            Notification record =  _notificationRepository.FindNotificationById(id);
            if (record != null)
            {
                
                response = new AddNotificationViewModel()
                {
                    Id = record.Id,
                    Message = record.Message,
                    ImageUrl = record.ImageUrl,
                    Type = record.NotificationType,
                    SelectedDeliveryLocationIds = record.NotificationDeliveryLocations.Select(x => x.DeliveryLocationId.Value).ToList(),
                    Title = record.Title,
                    CategoryId = record.CategoryId,
                };
                return response;
            }
            return null;
        }
        #endregion

        #region AddNotification for User
        /// <summary>
        /// This method is used to add notification for specific users
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<int> AddNotificationAsync(AddNotificationViewModel model, int ApplicationUserId)
        {
            Notification notification = new Notification()
            {
                CreatedDateTime = DateTime.Now,
                ApplicationUserId = ApplicationUserId,
                Title = model.Title,
                Message = model.Message,
                ImageUrl = model.ImageUrl,
                NotificationType = model.Type.ToLower() == StringConstants.TextNotification.ToLower() ? StringConstants.TextNotification : StringConstants.ImageNotification
            };
            _notificationRepository.Add(notification);
            var result = await _unitOfWork.SaveChangesAsync();
            var record = _userDeviceInfoRepository.FindByApplicationUserId(ApplicationUserId);
            if (record.Count > 0)
            {
                var deviceIds = record.Select(x => x.DeviceId);
                SendPushNotification(deviceIds, notification);
            }
            return notification.Id;
        }
        #endregion

        #region DeleteNotification
        /// <summary>
        /// This method is used to delete notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteNotification(int id)
        {
            Notification notification = await _notificationRepository.FindByIdAsync(id);
            if (notification == null)
                return 0;
            else
            {
                var record = _notificationDeliveryLocationRepository.GetNotificationDeliveryLocationList(id);
                if (record != null)
                {
                    foreach (var rec in record)
                    {
                        _notificationDeliveryLocationRepository.Remove(rec);
                        _unitOfWork.SaveChanges();
                    }
                }
                _notificationRepository.Remove(notification);
                var result = await _unitOfWork.SaveChangesAsync();
                return result;
            }
        }
        #endregion

        #region GetNotificationDetails
        /// <summary>
        /// This method is used to fetch notification details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NotificationDetailsViewModel GetNotificationDetails(int id)
        {
            NotificationDetailsViewModel notificationDetails;
            Notification notification = _notificationRepository.FindNotificationById(id);
            if (notification == null)
                return null;
            else
                notificationDetails = new NotificationDetailsViewModel()
                {
                    AddedDate = notification.CreatedDateTime.ToStringDateTimePattern(),
                    Id = notification.Id,
                    ImageUrl = notification.ImageUrl,
                    Message = notification.Message,
                    NotificationType = notification.NotificationType.ToLower() == StringConstants.TextNotification.ToLower() ? StringConstants.TextNotification : StringConstants.ImageNotification,
                    Title = notification.Title,
                    Category = notification.CategoryId == null ? "NA" : notification.Category.Name
                };
            notificationDetails.SelectedAreas = notification.NotificationDeliveryLocations.Select(x => x.DeliveryLocation.Area + " (" + x.DeliveryLocation.Pincode + ")").ToList();
            return notificationDetails;
        }
        #endregion

        #region GetWrapperForIndexView
        /// <summary>
        /// This method is for index view of notification
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<NotificationWrapperViewModel> GetWrapperForIndexView(NotificationFilter filter)
        {
            NotificationWrapperViewModel ResponseModel = new NotificationWrapperViewModel
            {
                TotalCount = _notificationRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Notification> list = await _notificationRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.NotificationList = list.Select((x, index) => new NotificationListViewModel
            {
                Id = x.Id,
                NotificationType = x.NotificationType.ToLower() == StringConstants.TextNotification.ToLower() ? StringConstants.TextNotification : StringConstants.ImageNotification,
                SentDate = x.CreatedDateTime.ToStringDatePattern(),
                Title = x.Title,
                Number = ResponseModel.PagingData.FromRecord + index,
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }
        #endregion

        #region SendPushNotification
        /// <summary>
        /// This method is for sending push notification
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <param name="notification"></param>
        private void SendPushNotification(IEnumerable<string> deviceIds, Notification notification)
        {
            object data;
            if (notification.NotificationType.ToLower() == StringConstants.TextNotification.ToLower())
            {
                data = new
                {
                    registration_ids = deviceIds,
                    data = new
                    {
                        data = new
                        {
                            message = notification.Message,
                            title = notification.Title,
                            url = string.Empty
                        }
                    }
                };
            }
            else
            {
                data = new
                {
                    registration_ids = deviceIds,
                    data = new
                    {
                        data = new
                        {
                            message = string.Empty,
                            title = notification.Title,
                            url = _appSettings.WebBaseUrl + notification.ImageUrl
                        }
                    }
                };
            }
            _fCMPushNotificationService.SendNotification(data);
        }
        #endregion

        #region GetDeliveryLocationListAsync
        public List<IdNameViewModel> GetDeliveryLocationList()
        {
            var list = _deliveryLocationRepository.GetAll();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Area + " (" + x.Pincode + ")" }).ToList();
            return responseList;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
