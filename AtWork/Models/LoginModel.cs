using System;

using Xamarin.Forms;

namespace AtWork.Models
{
    public class LoginModel
    {
        public class LoginInputModel
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public class LoginResponce
        {
            public bool Flag { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public LoginOutputModel Data { get; set; }
            public Volunteers Data1 { get; set; }
        }

        public class LoginOutputModel
        {
            private string _coLogo = string.Empty;
            
            public int id { get; set; }

            public string coUniqueID { get; set; }

            public string parentCoUniqueID { get; set; }

            public string coName { get; set; }

            public string coContactName { get; set; }

            public string coEmail { get; set; }

            public string coUserName { get; set; }

            public string coPassword { get; set; }

            public string coInvAddress1 { get; set; }

            public string coInvAddress2 { get; set; }

            public string coInvCity { get; set; }

            public string coInvPostalCode { get; set; }

            public string coInvCountry { get; set; }

            public string coLocation { get; set; }

            public string coAddress1 { get; set; }

            public string coAddress2 { get; set; }

            public string coCity { get; set; }

            public string coPostalCode { get; set; }

            public string coCountry { get; set; }

            public string coContinent { get; set; }

            public string coPhone { get; set; }

            public string coURL { get; set; }

            public string coDescription { get; set; }

            public string coIndustry { get; set; }

            public int? coMaxEmpl { get; set; }

            public string coPackage { get; set; }

            public string coTermsAccepted { get; set; }

            public DateTime? dateNewCompanySent { get; set; }

            public string onBoardStatus { get; set; }

            public int? coEmplOnBoardCompleted { get; set; }

            public int? coEmplOnboardSent { get; set; }

            public string coCompetenciesStatus { get; set; }

            public string coWhiteLabel { get; set; }

            public string coAllowPosts { get; set; }

            public string coWhiteLabelPicStatus { get; set; }

            public string coWhiteLabelGTPicStatus { get; set; }

            public string Accent { get; set; }
            public string Dark { get; set; }
            public string Secondary_Dark { get; set; }
            public string Light { get; set; }
            public string Secondary_Light { get; set; }


            public string coLogo
            {
                get
                {
                    if (_coLogo == null)
                        return string.Empty;
                    return _coLogo;
                }
                set
                {
                    _coLogo = value;
                }
            }
        }
        public partial class Volunteers
        {
            private string _volPicture = string.Empty;

            public int id { get; set; }

            public string coUniqueID { get; set; }

            public string volUniqueID { get; set; }

            public string volFirstName { get; set; }

            public string volLastName { get; set; }

            public string volGender { get; set; }

            public string volUserName { get; set; }

            public string VolUserPassword { get; set; }

            public string volEmail { get; set; }

            public string volOnBoardStatus { get; set; }

            public DateTime? volOnBoardDateSent { get; set; }

            public string volEducation { get; set; }

            public string volCompetencies { get; set; }

            public string volCategories { get; set; }

            public string volPhone { get; set; }

            public string volStatus { get; set; }

            public string restricted { get; set; }

            public string reviewStatus { get; set; }

            public DateTime? reviewDate { get; set; }

            public string volDepartment { get; set; }

            public string volLanguage { get; set; }

            public string volAbout { get; set; }

            public string volInterests { get; set; }
            public string volPicture
            {
                get
                {
                    if (_volPicture == null)
                        return string.Empty;
                    return _volPicture;
                }
                set
                {
                    _volPicture = value;
                }
            }
        }
    }
}

