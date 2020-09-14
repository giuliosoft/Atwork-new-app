using System;

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
    }
}
