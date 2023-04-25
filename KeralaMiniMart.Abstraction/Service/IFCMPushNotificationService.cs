using System;
using System.Collections.Generic;
using System.Text;


namespace KeralaMiniMart.Abstraction.Service
{
    public interface IFCMPushNotificationService
    {
        void SendNotification(object data);
    }
}
