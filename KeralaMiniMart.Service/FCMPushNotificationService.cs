using System;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Microsoft.Extensions.Options;
using KeralaMiniMart.Entities;
using KeralaMiniMart.Abstraction.Service;

namespace KeralaMiniMart.Service
{
    public class FCMPushNotificationService : IFCMPushNotificationService
    {
        private readonly AppSettings appsettings;

        public FCMPushNotificationService(IOptions<AppSettings> appsettings)
        {
            this.appsettings = appsettings.Value;
        }
        public void SendNotification(object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                string server_api_key = appsettings.FCMServerApiKey;
                string sender_id = appsettings.FCMSenderId;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key={server_api_key}");
                tRequest.Headers.Add($"sender: id={sender_id}");

                tRequest.ContentLength = byteArray.Length;
                Stream datastream = tRequest.GetRequestStream();
                datastream.Write(byteArray, 0, byteArray.Length);
                datastream.Close();

                WebResponse tresponse = tRequest.GetResponse();
                datastream = tresponse.GetResponseStream();
                StreamReader tReader = new StreamReader(datastream);

                string sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                datastream.Close();
                tresponse.Close();
            }
            catch
            {

            }
        }
    }
}


