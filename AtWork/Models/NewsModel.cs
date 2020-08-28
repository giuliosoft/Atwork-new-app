using System;

using Xamarin.Forms;

namespace AtWork.Models
{
    public class NewsModel : ContentPage
    {
        public class NewsResponce
        {
            public bool Flag { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public NewsDetailModel Data { get; set; }
        }
        public class NewsDetailModel
        {
            public int id { get; set; }
            public string coUniqueID { get; set; }
            public string newsUniqueID { get; set; }
            public string volUniqueID { get; set; }
            public string newsTitle { get; set; }
            public string newsContent { get; set; }
            public DateTime? newsDateTime { get; set; }
            public DateTime? newsPostedTime { get; set; }
            public string newsPrivacy { get; set; }
            public string newsStatus { get; set; }
            public string newsOrigin { get; set; }
            public string newsImage { get; set; }
            public string newsFile { get; set; }
            public string newsFileOriginal { get; set; }
        }
        
    }
}

