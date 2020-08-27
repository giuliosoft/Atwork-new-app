using System;
using System.Diagnostics;
using DLToolkit.Forms.Controls;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AtWork.Services
{
    public class LayoutService
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static void Init()
        {
            FlowListView.Init();
            Device.SetFlags(new string[] { "CarouselView_Experimental", "SwipeView_Experimental", "CollectionView_Experimental" });
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (DeviceInfo.Version < Version.Parse("11.0"))
                {
                    App.Current.Resources["SafeAreaPadding"] = new Thickness(0, 20, 0, 0);
                }
                else
                {
                    App.Current.Resources["SafeAreaPadding"] = new Thickness(0, 0, 0, 0);
                }
            }

            DynamicLayoutServiceInit();
        }

        public static void DynamicLayoutServiceInit()
        {
            #region Font Size

            App.Current.Resources["FontSize10"] = sizeConvertAsPerDevice(10);
            App.Current.Resources["FontSize11"] = sizeConvertAsPerDevice(11);
            App.Current.Resources["FontSize12"] = sizeConvertAsPerDevice(12);
            App.Current.Resources["FontSize14"] = sizeConvertAsPerDevice(14);
            App.Current.Resources["FontSize15"] = sizeConvertAsPerDevice(15);
            App.Current.Resources["FontSize16"] = sizeConvertAsPerDevice(16);
            App.Current.Resources["FontSize17"] = sizeConvertAsPerDevice(17);
            App.Current.Resources["FontSize18"] = sizeConvertAsPerDevice(18);
            App.Current.Resources["FontSize20"] = sizeConvertAsPerDevice(20);
            App.Current.Resources["FontSize22"] = sizeConvertAsPerDevice(22);
            App.Current.Resources["FontSize24"] = sizeConvertAsPerDevice(24);
            App.Current.Resources["FontSize25"] = sizeConvertAsPerDevice(25);
            App.Current.Resources["FontSize27"] = sizeConvertAsPerDevice(27);
            App.Current.Resources["FontSize30"] = sizeConvertAsPerDevice(30);
            App.Current.Resources["FontSize32"] = sizeConvertAsPerDevice(32);
            App.Current.Resources["FontSize35"] = sizeConvertAsPerDevice(35);
            App.Current.Resources["FontSize50"] = sizeConvertAsPerDevice(50);
            App.Current.Resources["FontSize70"] = sizeConvertAsPerDevice(70);
            App.Current.Resources["FontSize90"] = sizeConvertAsPerDevice(90);

            #endregion

            #region View Height Width

            App.Current.Resources["HeightWidth01"] = sizeConvertAsPerDevice(01);
            App.Current.Resources["HeightWidth02"] = sizeConvertAsPerDevice(02);
            App.Current.Resources["HeightWidth03"] = sizeConvertAsPerDevice(03);
            App.Current.Resources["HeightWidth05"] = sizeConvertAsPerDevice(05);
            App.Current.Resources["HeightWidth06"] = sizeConvertAsPerDevice(06);
            App.Current.Resources["HeightWidth10"] = sizeConvertAsPerDevice(10);
            App.Current.Resources["HeightWidth12"] = sizeConvertAsPerDevice(12);
            App.Current.Resources["HeightWidth15"] = sizeConvertAsPerDevice(15);
            App.Current.Resources["HeightWidth18"] = sizeConvertAsPerDevice(18);
            App.Current.Resources["HeightWidth20"] = sizeConvertAsPerDevice(20);
            App.Current.Resources["HeightWidth25"] = sizeConvertAsPerDevice(25);
            App.Current.Resources["HeightWidth28"] = sizeConvertAsPerDevice(28);
            App.Current.Resources["HeightWidth30"] = sizeConvertAsPerDevice(30);
            App.Current.Resources["HeightWidth35"] = sizeConvertAsPerDevice(35);
            App.Current.Resources["HeightWidth40"] = sizeConvertAsPerDevice(40);
            App.Current.Resources["HeightWidth50"] = sizeConvertAsPerDevice(50);
            App.Current.Resources["HeightWidth55"] = sizeConvertAsPerDevice(55);
            App.Current.Resources["HeightWidth60"] = sizeConvertAsPerDevice(60);
            App.Current.Resources["HeightWidth64"] = sizeConvertAsPerDevice(64);
            App.Current.Resources["HeightWidth70"] = sizeConvertAsPerDevice(70);
            App.Current.Resources["HeightWidth80"] = sizeConvertAsPerDevice(80);
            App.Current.Resources["HeightWidth90"] = sizeConvertAsPerDevice(90);
            App.Current.Resources["HeightWidth100"] = sizeConvertAsPerDevice(100);
            App.Current.Resources["HeightWidth115"] = sizeConvertAsPerDevice(115);
            App.Current.Resources["HeightWidth155"] = sizeConvertAsPerDevice(155);
            App.Current.Resources["HeightWidth160"] = sizeConvertAsPerDevice(160);
            App.Current.Resources["HeightWidth180"] = sizeConvertAsPerDevice(180);
            App.Current.Resources["HeightWidth218"] = sizeConvertAsPerDevice(218);
            App.Current.Resources["HeightWidth220"] = sizeConvertAsPerDevice(220);
            App.Current.Resources["HeightWidth250"] = sizeConvertAsPerDevice(250);
            App.Current.Resources["HeightWidth325"] = sizeConvertAsPerDevice(325);
            App.Current.Resources["HeightWidth350"] = sizeConvertAsPerDevice(350);

            #endregion

            #region Grid Height Width

            App.Current.Resources["GridHeightWidth10"] = (GridLength)sizeConvertAsPerDevice(10);
            App.Current.Resources["GridHeightWidth20"] = (GridLength)sizeConvertAsPerDevice(20);
            App.Current.Resources["GridHeightWidth25"] = (GridLength)sizeConvertAsPerDevice(25);
            App.Current.Resources["GridHeightWidth30"] = (GridLength)sizeConvertAsPerDevice(30);
            App.Current.Resources["GridHeightWidth40"] = (GridLength)sizeConvertAsPerDevice(40);
            App.Current.Resources["GridHeightWidth60"] = (GridLength)sizeConvertAsPerDevice(60);
            App.Current.Resources["GridHeightWidth70"] = (GridLength)sizeConvertAsPerDevice(70);
            App.Current.Resources["GridHeightWidth100"] = (GridLength)sizeConvertAsPerDevice(100);

            #endregion

            #region View Spacing

            App.Current.Resources["Spacing03"] = sizeConvertAsPerDevice(3);
            App.Current.Resources["Spacing05"] = sizeConvertAsPerDevice(5);
            App.Current.Resources["Spacing10"] = sizeConvertAsPerDevice(10);
            App.Current.Resources["Spacing15"] = sizeConvertAsPerDevice(15);
            App.Current.Resources["Spacing20"] = sizeConvertAsPerDevice(20);
            App.Current.Resources["Spacing25"] = sizeConvertAsPerDevice(25);
            App.Current.Resources["Spacing30"] = sizeConvertAsPerDevice(30);
            App.Current.Resources["Spacing50"] = sizeConvertAsPerDevice(50);

            #endregion

            #region Corner Radius
            App.Current.Resources["CornerRadius03"] = (float)sizeConvertAsPerDevice(03);
            App.Current.Resources["CornerRadius05"] = (float)sizeConvertAsPerDevice(05);
            App.Current.Resources["CornerRadius10"] = (float)sizeConvertAsPerDevice(10);
            App.Current.Resources["CornerRadius15"] = (float)sizeConvertAsPerDevice(15);
            App.Current.Resources["CornerRadius20"] = (float)sizeConvertAsPerDevice(20);
            App.Current.Resources["CornerRadius25"] = (float)sizeConvertAsPerDevice(25);

            App.Current.Resources["ButtonCornerRadius05"] = (int)sizeConvertAsPerDevice(05);
            App.Current.Resources["ButtonCornerRadius10"] = (int)sizeConvertAsPerDevice(10);
            App.Current.Resources["ButtonCornerRadius15"] = (int)sizeConvertAsPerDevice(15);
            App.Current.Resources["ButtonCornerRadius17"] = (int)sizeConvertAsPerDevice(17);
            App.Current.Resources["ButtonCornerRadius20"] = (int)sizeConvertAsPerDevice(20);

            #endregion

            #region View Margin Padding

            App.Current.Resources["MarginPadding00"] = MarginPaddingConvertAsPerDevice(0, 0, 0, 0);
            App.Current.Resources["MarginPadding02"] = MarginPaddingConvertAsPerDevice(02, 02, 02, 02);
            App.Current.Resources["MarginPadding04"] = MarginPaddingConvertAsPerDevice(04, 04, 04, 04);
            App.Current.Resources["MarginPadding05"] = MarginPaddingConvertAsPerDevice(05, 05, 05, 05);
            App.Current.Resources["MarginPadding08"] = MarginPaddingConvertAsPerDevice(08, 08, 08, 08);
            App.Current.Resources["MarginPadding10"] = MarginPaddingConvertAsPerDevice(10, 10, 10, 10);
            App.Current.Resources["MarginPadding12"] = MarginPaddingConvertAsPerDevice(12, 12, 12, 12);
            App.Current.Resources["MarginPadding15"] = MarginPaddingConvertAsPerDevice(15, 15, 15, 15);
            App.Current.Resources["MarginPadding20"] = MarginPaddingConvertAsPerDevice(20, 20, 20, 20);
            App.Current.Resources["MarginPadding25"] = MarginPaddingConvertAsPerDevice(25, 25, 25, 25);
            App.Current.Resources["MarginPadding30"] = MarginPaddingConvertAsPerDevice(30, 30, 30, 30);
            App.Current.Resources["MarginPadding40"] = MarginPaddingConvertAsPerDevice(40, 40, 40, 40);
            App.Current.Resources["MarginPadding30_50_30_50"] = MarginPaddingConvertAsPerDevice(30, 50, 30, 50);
            App.Current.Resources["MarginPadding00_50_00_30"] = MarginPaddingConvertAsPerDevice(00, 50, 00, 30);

            App.Current.Resources["MarginPadding00_05_00_00"] = MarginPaddingConvertAsPerDevice(00, 05, 00, 00);
            App.Current.Resources["MarginPadding00_10_05_10"] = MarginPaddingConvertAsPerDevice(00, 10, 05, 10);
            App.Current.Resources["MarginPadding00_08_00_00"] = MarginPaddingConvertAsPerDevice(00, 08, 00, 00);
            App.Current.Resources["MarginPadding00_00_05_00"] = MarginPaddingConvertAsPerDevice(00, 00, 05, 00);
            App.Current.Resources["MarginPadding00_05_00_05"] = MarginPaddingConvertAsPerDevice(00, 05, 00, 05);
            App.Current.Resources["MarginPadding00_25_00_25"] = MarginPaddingConvertAsPerDevice(00, 25, 00, 25);
            App.Current.Resources["MarginPadding00_10_25_00"] = MarginPaddingConvertAsPerDevice(00, 10, 25, 00);
            App.Current.Resources["MarginPadding05_00_05_00"] = MarginPaddingConvertAsPerDevice(05, 00, 05, 00);
            App.Current.Resources["MarginPadding05_00_10_00"] = MarginPaddingConvertAsPerDevice(05, 00, 10, 00);
            App.Current.Resources["MarginPadding05_10_05_10"] = MarginPaddingConvertAsPerDevice(05, 10, 05, 10);
            App.Current.Resources["MarginPadding05_20_05_20"] = MarginPaddingConvertAsPerDevice(05, 20, 05, 20);
            App.Current.Resources["MarginPadding10_05_10_05"] = MarginPaddingConvertAsPerDevice(10, 05, 10, 05);
            App.Current.Resources["MarginPadding10_00_10_00"] = MarginPaddingConvertAsPerDevice(10, 00, 10, 00);
            App.Current.Resources["MarginPadding10_10_00_10"] = MarginPaddingConvertAsPerDevice(10, 10, 00, 10);
            App.Current.Resources["MarginPadding10_10_10_00"] = MarginPaddingConvertAsPerDevice(10, 10, 10, 00);
            App.Current.Resources["MarginPadding10_00_10_10"] = MarginPaddingConvertAsPerDevice(10, 00, 10, 10);
            App.Current.Resources["MarginPadding00_10_00_00"] = MarginPaddingConvertAsPerDevice(00, 10, 00, 00);
            App.Current.Resources["MarginPadding00_00_10_00"] = MarginPaddingConvertAsPerDevice(00, 00, 10, 00);
            App.Current.Resources["MarginPadding00_10_00_10"] = MarginPaddingConvertAsPerDevice(00, 10, 00, 10);
            App.Current.Resources["MarginPadding10_00_00_00"] = MarginPaddingConvertAsPerDevice(10, 00, 00, 00);
            App.Current.Resources["MarginPadding00_00_00_10"] = MarginPaddingConvertAsPerDevice(00, 00, 00, 10);
            App.Current.Resources["MarginPadding10_10_10_25"] = MarginPaddingConvertAsPerDevice(10, 10, 10, 25);
            App.Current.Resources["MarginPadding10_20_10_20"] = MarginPaddingConvertAsPerDevice(10, 20, 10, 20);

            App.Current.Resources["MarginPadding12_00_00_00"] = MarginPaddingConvertAsPerDevice(12, 00, 00, 00);

            App.Current.Resources["MarginPadding12_00_12_00"] = MarginPaddingConvertAsPerDevice(12, 00, 12, 00);
            App.Current.Resources["MarginPadding15_15_15_10"] = MarginPaddingConvertAsPerDevice(15, 15, 15, 10);
            App.Current.Resources["MarginPadding15_15_15_00"] = MarginPaddingConvertAsPerDevice(15, 15, 15, 00);
            App.Current.Resources["MarginPadding15_00_15_10"] = MarginPaddingConvertAsPerDevice(15, 00, 15, 10);
            App.Current.Resources["MarginPadding15_00_15_15"] = MarginPaddingConvertAsPerDevice(15, 00, 15, 15);
            App.Current.Resources["MarginPadding15_00_15_00"] = MarginPaddingConvertAsPerDevice(15, 00, 15, 00);
            App.Current.Resources["MarginPadding15_00_00_00"] = MarginPaddingConvertAsPerDevice(15, 00, 00, 00);
            App.Current.Resources["MarginPadding00_00_15_00"] = MarginPaddingConvertAsPerDevice(00, 00, 15, 00);
            App.Current.Resources["MarginPadding00_15_00_15"] = MarginPaddingConvertAsPerDevice(00, 15, 00, 15);
            App.Current.Resources["MarginPadding15_05_15_05"] = MarginPaddingConvertAsPerDevice(15, 05, 15, 05);
            App.Current.Resources["MarginPadding15_05_15_15"] = MarginPaddingConvertAsPerDevice(15, 05, 15, 15);
            App.Current.Resources["MarginPadding15_10_00_10"] = MarginPaddingConvertAsPerDevice(15, 10, 00, 10);
            App.Current.Resources["MarginPadding15_10_15_10"] = MarginPaddingConvertAsPerDevice(15, 10, 15, 10);
            App.Current.Resources["MarginPadding15_15_00_15"] = MarginPaddingConvertAsPerDevice(15, 15, 00, 15);
            App.Current.Resources["MarginPadding15_15_15_15"] = MarginPaddingConvertAsPerDevice(15, 15, 15, 15);
            App.Current.Resources["MarginPadding15_00_15_20"] = MarginPaddingConvertAsPerDevice(15, 00, 15, 20);
            App.Current.Resources["MarginPadding15_20_15_20"] = MarginPaddingConvertAsPerDevice(15, 20, 15, 20);
            App.Current.Resources["MarginPadding15_30_15_00"] = MarginPaddingConvertAsPerDevice(15, 30, 15, 00);
            App.Current.Resources["MarginPadding20_00_20_00"] = MarginPaddingConvertAsPerDevice(20, 00, 20, 00);
            App.Current.Resources["MarginPadding00_00_20_00"] = MarginPaddingConvertAsPerDevice(00, 00, 20, 00);
            App.Current.Resources["MarginPadding00_20_00_00"] = MarginPaddingConvertAsPerDevice(00, 20, 00, 00);
            App.Current.Resources["MarginPadding00_20_00_20"] = MarginPaddingConvertAsPerDevice(00, 20, 00, 20);
            App.Current.Resources["MarginPadding20_20_20_10"] = MarginPaddingConvertAsPerDevice(20, 20, 20, 10);
            App.Current.Resources["MarginPadding20_20_20_00"] = MarginPaddingConvertAsPerDevice(20, 20, 20, 00);
            App.Current.Resources["MarginPadding20_00_00_00"] = MarginPaddingConvertAsPerDevice(20, 00, 00, 00);
            App.Current.Resources["MarginPadding00_00_00_20"] = MarginPaddingConvertAsPerDevice(00, 00, 00, 20);
            App.Current.Resources["MarginPadding20_15_20_05"] = MarginPaddingConvertAsPerDevice(20, 15, 20, 05);
            App.Current.Resources["MarginPadding20_30_20_30"] = MarginPaddingConvertAsPerDevice(20, 30, 20, 30);
            App.Current.Resources["MarginPadding20_0_20_0"] = MarginPaddingConvertAsPerDevice(20, 0, 20, 0);
            App.Current.Resources["MarginPadding0_10_0_40"] = MarginPaddingConvertAsPerDevice(0, 10, 0, 40);

            App.Current.Resources["MarginPadding00_20_00_10"] = MarginPaddingConvertAsPerDevice(00, 20, 00, 10);
            App.Current.Resources["MarginPadding20_10_20_20"] = MarginPaddingConvertAsPerDevice(20, 10, 20, 20);
            App.Current.Resources["MarginPadding20_10_20_10"] = MarginPaddingConvertAsPerDevice(20, 10, 20, 10);
            App.Current.Resources["MarginPadding20_10_20_15"] = MarginPaddingConvertAsPerDevice(20, 10, 20, 15);
            App.Current.Resources["MarginPadding20_10_20_00"] = MarginPaddingConvertAsPerDevice(20, 10, 20, 00);
            App.Current.Resources["MarginPadding20_00_20_10"] = MarginPaddingConvertAsPerDevice(20, 00, 20, 10);
            App.Current.Resources["MarginPadding20_05_10_05"] = MarginPaddingConvertAsPerDevice(20, 05, 10, 05);
            App.Current.Resources["MarginPadding20_05_20_15"] = MarginPaddingConvertAsPerDevice(20, 05, 20, 15);
            App.Current.Resources["MarginPadding20_00_00_25"] = MarginPaddingConvertAsPerDevice(20, 00, 00, 25);
            App.Current.Resources["MarginPadding20_00_20_35"] = MarginPaddingConvertAsPerDevice(20, 00, 20, 35);
            App.Current.Resources["MarginPadding20_00_20_55"] = MarginPaddingConvertAsPerDevice(20, 00, 20, 55);
            App.Current.Resources["MarginPadding60_15_60_15"] = MarginPaddingConvertAsPerDevice(60, 15, 60, 15);

            App.Current.Resources["MarginPadding20_07_20_07"] = MarginPaddingConvertAsPerDevice(20, 07, 20, 07);
            App.Current.Resources["MarginPadding30_00_00_00"] = MarginPaddingConvertAsPerDevice(30, 00, 00, 00);
            App.Current.Resources["MarginPadding00_30_00_00"] = MarginPaddingConvertAsPerDevice(00, 30, 00, 00);
            App.Current.Resources["MarginPadding00_00_30_00"] = MarginPaddingConvertAsPerDevice(00, 00, 30, 00);
            App.Current.Resources["MarginPadding00_00_00_30"] = MarginPaddingConvertAsPerDevice(00, 00, 00, 30);
            App.Current.Resources["MarginPadding00_30_00_30"] = MarginPaddingConvertAsPerDevice(00, 30, 00, 30);
            App.Current.Resources["MarginPadding30_00_30_00"] = MarginPaddingConvertAsPerDevice(30, 00, 30, 00);
            App.Current.Resources["MarginPadding30_00_30_20"] = MarginPaddingConvertAsPerDevice(30, 00, 30, 20);
            App.Current.Resources["MarginPadding30_15_30_15"] = MarginPaddingConvertAsPerDevice(30, 15, 30, 15);
            App.Current.Resources["MarginPadding30_20_30_10"] = MarginPaddingConvertAsPerDevice(30, 20, 30, 10);
            App.Current.Resources["MarginPadding00_20_00_40"] = MarginPaddingConvertAsPerDevice(00, 20, 00, 40);

            App.Current.Resources["MarginPadding35_00_35_00"] = MarginPaddingConvertAsPerDevice(35, 00, 35, 00);
            App.Current.Resources["MarginPadding35_00_35_25"] = MarginPaddingConvertAsPerDevice(35, 00, 35, 25);
            App.Current.Resources["MarginPadding35_00_35_60"] = MarginPaddingConvertAsPerDevice(35, 00, 35, 60);
            App.Current.Resources["MarginPadding40_00_40_00"] = MarginPaddingConvertAsPerDevice(40, 00, 40, 00);
            App.Current.Resources["MarginPadding40_05_40_05"] = MarginPaddingConvertAsPerDevice(40, 05, 40, 05);
            App.Current.Resources["MarginPadding00_40_00_40"] = MarginPaddingConvertAsPerDevice(00, 40, 00, 40);

            App.Current.Resources["MarginPadding00_50_00_00"] = MarginPaddingConvertAsPerDevice(00, 50, 00, 00);
            App.Current.Resources["MarginPadding50_10_50_10"] = MarginPaddingConvertAsPerDevice(50, 10, 50, 10);

            App.Current.Resources["MarginPadding50_20_50_20"] = MarginPaddingConvertAsPerDevice(50, 20, 50, 20);
            App.Current.Resources["MarginPadding00_00_00_50"] = MarginPaddingConvertAsPerDevice(00, 00, 00, 50);
            App.Current.Resources["MarginPaddin50_40_50_00"] = MarginPaddingConvertAsPerDevice(50, 40, 50, 00);

            App.Current.Resources["MarginPadding0_0_0_100"] = MarginPaddingConvertAsPerDevice(0, 0, 0, 1000);
            App.Current.Resources["MarginPadding0_100_0_0"] = MarginPaddingConvertAsPerDevice(0, 100, 0, 0);
            App.Current.Resources["MarginPadding0_120_0_0"] = MarginPaddingConvertAsPerDevice(0, 120, 0, 0);
            App.Current.Resources["MarginPadding0_200_0_0"] = MarginPaddingConvertAsPerDevice(0, 200, 0, 0);

            App.Current.Resources["MarginPaddingm10_0_0_0"] = MarginPaddingConvertAsPerDevice(-10, 0, 0, 0);
            App.Current.Resources["MarginPadding0_m20_0_0"] = MarginPaddingConvertAsPerDevice(0, -20, 0, 0);
            App.Current.Resources["MarginPaddingm20_0_0_0"] = MarginPaddingConvertAsPerDevice(-20, 0, 0, 0);
            App.Current.Resources["MarginPadding0_0_m20_0"] = MarginPaddingConvertAsPerDevice(0, 0, -20, 0);
            App.Current.Resources["MarginPadding30_m05_0_0"] = MarginPaddingConvertAsPerDevice(30, -5, 0, 0);

            App.Current.Resources["MarginPadding20_m05_0_m05"] = MarginPaddingConvertAsPerDevice(20, -5, 0, -5);
            App.Current.Resources["MarginPadding20_m07_0_m07"] = MarginPaddingConvertAsPerDevice(20, -7, 0, -7);
            App.Current.Resources["MarginPadding0_0_0_m40"] = MarginPaddingConvertAsPerDevice(0, 0, 0, -40);

            App.Current.Resources["MarginPadding0_m10_m20_m10"] = MarginPaddingConvertAsPerDevice(0, -10, -20, -10);
            App.Current.Resources["MarginPaddingm20_m10_0_m10"] = MarginPaddingConvertAsPerDevice(-20, -10, 0, -10);

            App.Current.Resources["MarginPadding15_0_0_0"] = MarginPaddingConvertAsPerDevice(15, 0, 0, 0);
            App.Current.Resources["MarginPadding15_0_0_m100"] = MarginPaddingConvertAsPerDevice(15, 0, 0, -100);
            App.Current.Resources["MarginPadding00_m20_00_00"] = MarginPaddingConvertAsPerDevice(0, -20, 0, 0);
            App.Current.Resources["MarginPadding00_00_00_m10"] = MarginPaddingConvertAsPerDevice(0, 0, 0, -10);
            App.Current.Resources["MarginPaddingm20_00_m20_00"] = MarginPaddingConvertAsPerDevice(-20, 0, -20, 0);
            App.Current.Resources["MarginPaddingm30_00_00_00"] = MarginPaddingConvertAsPerDevice(-30, 0, 0, 0);
            App.Current.Resources["MarginPadding00_00_m30_00"] = MarginPaddingConvertAsPerDevice(0, 0, -30, 0);
            #endregion

            #region PancakeView CornerRadius

            App.Current.Resources["PancakeViewCornerRadius10"] = PancakeViewSizeConvertAsPerDevice(10);
            App.Current.Resources["PancakeViewCornerRadius15"] = PancakeViewSizeConvertAsPerDevice(15);
            App.Current.Resources["PancakeViewCornerRadius30"] = PancakeViewSizeConvertAsPerDevice(30);

            #endregion
        }

        static double SmallDeviceSize = 700;
        public static double sizeConvertAsPerDevice(double size)
        {
            try
            {
                if (size <= 0) { return 0; }
                int d = 0;
                if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                {
                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                    var Height = mainDisplayInfo.Height / mainDisplayInfo.Density;

                    if (Height <= SmallDeviceSize)
                    {
                        //For Small Size Devices
                        d = 4;
                    }
                }
                else
                {
                    d = 2;
                }

                size = CalculateSize(size, d);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return size;
        }

        static double CalculateSize(double val, double d)
        {
            if (d == 0)
            {
                return val;
            }
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                if (val > 0)
                {
                    var half = val / d;
                    val = val - half;
                }
                else if (val < 0)
                {
                    var half = Math.Abs(val) / d;
                    val = -(Math.Abs(val) - half);
                }
            }
            else
            {
                if (val > 0)
                {
                    var half = val / d;
                    val = val + half;
                }
                else if (val < 0)
                {
                    var half = Math.Abs(val) / d;
                    val = -(Math.Abs(val) + half);
                }
            }
            return val;
        }

        public static Thickness MarginPaddingConvertAsPerDevice(double left, double top, double right, double bottom)
        {
            try
            {
                int d = 0;
                if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                {
                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                    var Height = mainDisplayInfo.Height / mainDisplayInfo.Density;

                    if (Height <= SmallDeviceSize)
                    {
                        //For Small Size Devices
                        d = 4;
                    }
                }
                else
                {
                    d = 2;
                }
                left = CalculateSize(left, d);
                top = CalculateSize(top, d);
                right = CalculateSize(right, d);
                bottom = CalculateSize(bottom, d);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return new Thickness(left, top, right, bottom);
        }

        public static CornerRadius PancakeViewSizeConvertAsPerDevice(double size)
        {
            try
            {
                if (size <= 0) { return 0; }
                if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                {
                    var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        int d = 0;
                        if (mainDisplayInfo.Height <= 1136)
                        {
                            d = 5;
                        }
                        else if (mainDisplayInfo.Height <= 1334)
                        {
                            d = 7;
                        }
                        else
                        {
                            return new CornerRadius(size, size, size, size);
                        }

                        var half = size / d;
                        size = size - half;
                    }
                    else
                    {
                        return new CornerRadius(size, size, size, size);
                    }
                }
                else
                {
                    var half = size / 2;
                    size = size + half;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return new CornerRadius(size, size, size, size);
        }
    }
}
