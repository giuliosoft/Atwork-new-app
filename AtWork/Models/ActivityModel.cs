using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AtWork.Helpers;
using AtWork.Services;
using Prism.Mvvm;
using Xamarin.Forms;
using static AtWork.Models.LoginModel;

namespace AtWork.Models
{

    public class ActivityModel
    {
        public class ActivityListModel : BindableBase
        {
            public int id { get; set; }
            public string proUniqueID { get; set; }
            public string proTitle { get; set; }
            public string coUniqueID { get; set; }
            public int JoinActivityId { get; set; }

            public string proDescription { get; set; }
            public string proLocation { get; set; }
            public DateTime? proAddActivityDate { get; set; }
            public string proAddActivity_StartTime { get; set; }
            public string proAddActivity_EndTime { get; set; }
            public string proAddress1 { get; set; }
            public object proAddress2 { get; set; }
            public string proPostalCode { get; set; }
            public string proCity { get; set; }
            public string proCategoryName { get; set; }
            public string proSubCategoryName { get; set; }
            public string proStatus { get; set; }
            public string proPartner { get; set; }
            public object proAddActivity_HoursCommitted { get; set; }
            public string proAddActivity_ParticipantsMinNumber { get; set; }
            public string proAddActivity_ParticipantsMaxNumber { get; set; }
            public string proBackgroundImage { get; set; }
            public string proDeliveryMethod { get; set; }
            public string proCompany { get; set; }
            public string proCountry { get; set; }
            public string proContinent { get; set; }
            public string proTimeCommitment { get; set; }
            public string proTimeCommitmentDecimal { get; set; }
            public string proDatesConfirmed { get; set; }
            public string proType { get; set; }
            public string proCategory { get; set; }
            public string proSubCategory { get; set; }
            public string proPartnerEmail { get; set; }
            public string proActivityLanguage { get; set; }
            public string proActivityLanguageID { get; set; }
            public string proAudience { get; set; }
            public string proSpecialRequirements { get; set; }
            public string proCostCoveredEmployee { get; set; }
            public string proCostCoveredCompany { get; set; }
            public string proAddActivity_OrgName { get; set; }
            public string proAddActivity_Website { get; set; }
            public string proAddActivity_AdditionalInfo { get; set; }
            public string proAddActivity_CoordinatorEmail { get; set; }
            public DateTime? proPublishedDate { get; set; }
            public string volUniqueID { get; set; }
            public string Member { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string DataType { get; set; }
            public string proVolHourDates { get; set; }
            public string Companie_Name { get; set; }
            public string Companie_Address1 { get; set; }
            public string Companie_Address2 { get; set; }
            public string Keyword { get; set; }
            public string Emoji { get; set; }
            public string ImageName { get; set; }
            public string Goal { get; set; }
            public string skills { get; set; }
            public string Comments { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }

            //New Fields:
            public ObservableCollection<ActivityCarouselListModel> ActivityCarouselList { get; set; }

            public string activityLocation
            {
                get
                {
                    if (string.IsNullOrEmpty(proAddress1))
                        return string.Empty;
                    return proAddress1;
                }
            }

            public string activityDate
            {
                get
                {
                    var retDate = string.Empty;
                    try
                    {
                        if (proAddActivityDate == null)
                            retDate = string.Empty;
                        var date = proAddActivityDate?.Date;
                        retDate = date?.ToString("d MMM yyyy");
                    }
                    catch (Exception ex)
                    {
                        ExceptionHelper.CommanException(ex);
                    }
                    return retDate;
                }
            }

            public string activityDuration
            {
                get
                {
                    if (string.IsNullOrEmpty(proAddActivity_StartTime) && string.IsNullOrEmpty(proAddActivity_EndTime))
                        return string.Empty;
                    else if (!string.IsNullOrEmpty(proAddActivity_StartTime) && !string.IsNullOrEmpty(proAddActivity_EndTime))
                        return proAddActivity_StartTime + " - " + proAddActivity_EndTime;
                    else if (proAddActivity_StartTime != null && proAddActivity_StartTime != string.Empty)
                        return proAddActivity_StartTime;
                    else if (proAddActivity_EndTime != null && proAddActivity_EndTime != string.Empty)
                        return proAddActivity_EndTime;
                    else
                        return string.Empty;


                }
            }
            public bool _ShowPastActivity;
            public bool ShowPastActivity
            {
                get { return _ShowPastActivity; }
                set { SetProperty(ref _ShowPastActivity, value); }
            }

            public bool IsPastActivity { get; set; }
            public bool ShowLink
            {
                get
                {
                    return !string.IsNullOrEmpty(proAddActivity_Website);
                }
            }

            public ImageSource activityPostPublishType
            {
                get
                {
                    if (string.IsNullOrEmpty(proAudience))
                        return string.Empty;
                    if (proAudience.Equals("Post to everybody", StringComparison.InvariantCultureIgnoreCase))
                        return "earth";
                    else
                        return "ActivityPeopleIcon";
                }
            }
            public ObservableCollection<EmojiDisplayModel> _EmojiList;
            public ObservableCollection<EmojiDisplayModel> EmojiList
            {
                get { return _EmojiList; }
                set { SetProperty(ref _EmojiList, value); }
            }
            public bool ActivityCreatedByLoggedInUser
            {
                get
                {
                    if (!string.IsNullOrEmpty(volUniqueID) && SettingsService.VolunteersUserData != null)
                        return volUniqueID == SettingsService.VolunteersUserData.volUniqueID;
                    return false;
                }
            }
        }

        public class ActivityResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<ActivityListModel> Data { get; set; }
            public List<ActivityListModel> Data1 { get; set; }
        }

        public class ActivityCarouselListModel
        {
            public ImageSource ActivityImage { get; set; }
            public string ActivityImageUrl { get; set; }
        }

        public class JoinActivityInputModel
        {
            public int Id { get; set; }
            public string coUniqueID { get; set; }
            public string proUniqueID { get; set; }
            public string volUniqueID { get; set; }
            //public int ActivityID { get; set; }
            public string volTransport { get; set; }

            public string volDiet { get; set; }

            public string proStatus { get; set; }

            public DateTime? proChosenDate { get; set; }

            public DateTime? proVolHourDates { get; set; }

            public string RecurringDates { get; set; }
        }

        public class ActivityJoinedMemberListResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<Volunteers> Data { get; set; }
        }

        //Activity History

        public class ActivityHistoryResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public List<ActivitiesDisplay> Data { get; set; }
            public ActivitiesHistory Data1 { get; set; }
        }
        public class ActivitiesDisplay :BindableBase
        {
            public string proUniqueID { get; set; }
            public string proTitle { get; set; }
            public string proCategoryName { get; set; }
            public DateTime? proAddActivityDate { get; set; }

            public string ActivityDate 
            {
                get
                {
                    if (proAddActivityDate != null)
                    {
                        if (proAddActivityDate?.Year == DateTime.Now.Year)
                        {
                            return proAddActivityDate?.ToString("MMMM dd");
                        }
                        else
                        {
                            return proAddActivityDate?.ToString("MMMM dd yyyy");
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
        public class ActivitiesHistory
        {
            public ActivitiesDisplay Activities { get; set; }
            public string TotalActivitieHour { get; set; }
            public string TotalActivitieCount { get; set; }
            public string CategorywiseHourCount { get; set; }
            public string CategoryActivityCount { get; set; }

            //public string TotalActivitieHourCount
            //{
            //    get
            //    {
            //        if (string.IsNullOrEmpty(TotalActivitieHour))
            //        {
            //            return "0";
            //        }
            //        else
            //        {
            //            return TotalActivitieHour;
            //        }
            //    }
            //}
        }



    }
}
