using System;
using Prism.Mvvm;

namespace AtWork.Models
{
    public class EmojiDisplayModel : BindableBase
    {
        private string _EmojiName;
        public string EmojiName
        {
            get { return _EmojiName; }
            set { SetProperty(ref _EmojiName, value); }
        }
        private string _EmojiCode;
        public string EmojiCode
        {
            get { return _EmojiCode; }
            set { SetProperty(ref _EmojiCode, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
