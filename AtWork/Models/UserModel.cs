using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

namespace AtWork.Models
{
    public class UserModel
    {
        public class ProfileResponce
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public Volunteers Data { get; set; }
            public object Data1 { get; set; }
        }

        public class LanguageResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<string> Data { get; set; }
            public string Data1 { get; set; }
        }
        public class BirthDateResponce
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public VolunteerBirthday Data { get; set; }
            public string Data1 { get; set; }
        }
        public class VolunteerBirthday
        {
            public int volBirthMonth { get; set; }
            public int volBirthDay { get; set; }
            public int volBirthYear { get; set; }
            public bool volShowBirthday { get; set; }
        }
        public class GetUsersByGroupWise
        {
            public string coUniqueID { get; set; }
            public string classUniqueID { get; set; }
            public string classValue { get; set; }
            public int pageNumber { get; set; }
        }
    }
}
