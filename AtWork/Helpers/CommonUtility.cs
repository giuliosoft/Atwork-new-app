using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using AtWork.Models;
using AtWork.ViewModels;
using Xamarin.Forms;

namespace AtWork.Helpers
{
    public class CommonUtility
    {
        public static ImageSource getImageFromEmbeddedResource(string ImageName)
        {
            ImageSource imageSource = null;
            try
            {
                string Source = "PowerHold.Images." + ImageName;
                imageSource = ImageSource.FromResource(Source, typeof(CommonUtility).GetTypeInfo().Assembly);
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
            return imageSource;
        }

        public static bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool PasswordIsValid(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (hasLowerChar.IsMatch(password) && hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && hasMinMaxChars.IsMatch(password) && hasSymbols.IsMatch(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static ObservableCollection<EmojiDisplayModel> EmojisList()
        {
            ObservableCollection<EmojiDisplayModel> lstEmojis = new ObservableCollection<EmojiDisplayModel>();
            try
            { 
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-rolling_on_the_floor_laughing", EmojiCode = new Emoji(0x1F923).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-slightly_smiling_face", EmojiCode = new Emoji(0x1F642).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-two_hearts", EmojiCode = new Emoji(0x1F495).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em---1", EmojiCode = new Emoji(0x1F44D).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-monkey_face", EmojiCode = new Emoji(0x1F435).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-sleeping", EmojiCode = new Emoji(0x1F634).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-hearts", EmojiCode = new Emoji(0x2764).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-smiley", EmojiCode = new Emoji(0x1F603).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-slightly_frowning_face", EmojiCode = new Emoji(0x1F641).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() {EmojiName = "em em-peace_symbol", EmojiCode = new Emoji(0x262E).ToString()});
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-sunglasses", EmojiCode = new Emoji(0x1F60E).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-laughing", EmojiCode = new Emoji(0x1F606).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-heart_eyes", EmojiCode = new Emoji(0x1F60D).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-kissing_heart", EmojiCode = new Emoji(0x1F618).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-smirk", EmojiCode = new Emoji(0x1F60F).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-wink", EmojiCode = new Emoji(0x1F609).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-notes", EmojiCode = new Emoji(0x1F3B6).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-stuck_out_tongue_winking_eye", EmojiCode = new Emoji(0x1F61C).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-pensive", EmojiCode = new Emoji(0x1F614).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-blush", EmojiCode = new Emoji(0x1F60A).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-sweat_smile", EmojiCode = new Emoji(0x1F605).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-upside_down_face", EmojiCode = new Emoji(0x1F643).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-eyes", EmojiCode = new Emoji(0x1F440).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-stuck_out_tongue_closed_eyes", EmojiCode = new Emoji(0x1F61D).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-ok_hand", EmojiCode = new Emoji(0x1F44C).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-tired_face", EmojiCode = new Emoji(0x1F62B).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-astonished", EmojiCode = new Emoji(0x1F632).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-woman-tipping-hand", EmojiCode = new Emoji(new int[] { 0x1F481, 0x200D, 0x2640, 0xFE0F }).ToString() }); //U+1F481 U+200D U+2640 U+FE0F 
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-neutral_face", EmojiCode = new Emoji(0x1F610).ToString() });
                lstEmojis.Add(new EmojiDisplayModel() { EmojiName = "em em-clap", EmojiCode = new Emoji(0x1F44F).ToString() });
            }
            catch (Exception ex)
            {
                ExceptionHelper.CommanException(ex);
            }
            return lstEmojis;
        }
    }
}
