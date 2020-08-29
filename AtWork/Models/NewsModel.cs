using System;
using System.Collections.Generic;
using AtWork.ViewModels;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

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
            public News News { get; set; }
            public int Comments_Likes { get; set; }
            public string Day { get; set; }
            public Volunteers Volunteers { get; set; }
        }
        public class News
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
            public string NewsImage { get; set; }
            public List<CarouselModel> NewsImageCarouselList { get; set; }
        }


        public class NewsDetailModel_Input
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

