using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Constant
{
    public static class EnvironmentConstants
    {
        ////TESTING
        public static string DatabaseName { get; } = "KeralaMiniMartDB_test";
        public static string KMMImageUrl { get; } = "http://178.128.111.8:9012";
        public static string KMMAPIImageUrl { get; } = "http://178.128.111.8:9011";
        public static string WebInternalURL { get; } = "http://127.0.0.1:9112";
        public static string WebAPIInternalURL { get; } = "http://127.0.0.1:9111";
        public static string CookiesName { get; } = "KeralaMiniMartTest";
        public static string AdminMail { get; } = "rohan.bhokare@omni-bridge.net";


        ////STAGING
        //public static string DatabaseName { get; } = "KeralaMiniMartDB_staging";
        //public static string KMMImageUrl { get; } = "http://178.128.111.8:9014";
        //public static string KMMAPIImageUrl { get; } = "http://178.128.111.8:9013";
        //public static string WebInternalURL { get; } = "http://127.0.0.1:9114";
        //public static string WebAPIInternalURL { get; } = "http://127.0.0.1:9113";
        //public static string CookiesName { get; } = "KeralaMiniMartStaging";
        //public static string AdminMail { get; } = "rohanbhokare92@gmail.com";


        ////PRODUCTION
        //public static string DatabaseName { get; } = "KeralaMiniMartDB_live";
        //public static string KMMImageUrl { get; } = "http://165.22.60.17:80";
        //public static string KMMAPIImageUrl { get; } = "http://165.22.60.17:8080";
        //public static string WebInternalURL { get; } = "http://127.0.0.1:50080";
        //public static string WebAPIInternalURL { get; } = "http://127.0.0.1:58080";
        //public static string CookiesName { get; } = "KeralaMiniMartLive";
        //public static string AdminMail { get; } = "keralaminimart@gmail.com";


    }
}
