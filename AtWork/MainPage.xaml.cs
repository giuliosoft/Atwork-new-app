using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Forms;

namespace AtWork
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //var calendars = await CrossCalendars.Current.GetCalendarsAsync();
            //var defaultCalendar = calendars.Where((x) => x.AccountName == "Default" && x.CanEditEvents).FirstOrDefault();
            //var calendarEvent = new CalendarEvent
            //{
            //    Name = "AtWork Activity Event",
            //    Start = DateTime.Now,
            //    End = DateTime.Now.AddHours(1),
            //    Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
            //};
            //await CrossCalendars.Current.AddOrUpdateEventAsync(defaultCalendar, calendarEvent);
        }
    }
}
