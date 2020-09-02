using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AtWork.Services;
using Prism.Commands;
using Prism.Navigation;

namespace AtWork.ViewModels
{
    public class MemberListPageViewModel : ViewModelBase
    {
        public MemberListPageViewModel(INavigationService navigationService, FacadeService facadeService) : base(navigationService, facadeService)
        {
            DetailHeaderOptionIsVisible = false;
        }
        private ObservableCollection<TempMember> _members = new ObservableCollection<TempMember>();
        public ObservableCollection<TempMember> Members
        {
            get { return _members; }
            set
            {
                if (SetProperty(ref _members, value))
                    HeaderDetailsTitle = $"{Members?.Count} joined";
            }
        }
        public DelegateCommand<TempMember> SelectionChangedCommand { get { return new DelegateCommand<TempMember>(async (obj) => await OnSelectionChanged(obj)); } }

        private async Task OnSelectionChanged(TempMember member)
        {
            await DisplayAlertAsync(member.Name);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            List<TempMember> Member = new List<TempMember>();
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Member.Add(new TempMember() { ProfileImage = "http://app.atwork.ai/images/Profiles/defaultpic.png", Name = "John Claar" });
            Members = new ObservableCollection<TempMember>(Member);
        }
        //Remove this class when Api is implemented
        public class TempMember
        {
            string name;
            public string ProfileImage { get; set; }
            public string Name { get; set; }
        }
    }
}
