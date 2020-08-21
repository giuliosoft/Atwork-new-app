using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AtWork.Services
{
    public static class AnimationService
    {
        public static bool IsAnimationEnable { get; set; } = true;

        #region AnimationProperty

        public static uint AnimationSpeed = 150;
        public static int DelaySpeed = 100;

        #endregion

        public static async Task ScaleAllToOne(List<VisualElement> elements, int? delay = null)
        {
            if (IsAnimationEnable)
            {
                if (delay != null)
                {
                    await Task.Delay(delay.Value);
                }
                foreach (var element in elements)
                {
                    await element.ScaleTo(1, AnimationService.AnimationSpeed, Easing.SinIn);
                }
            }
        }

        public static void ScaleAllToZero(List<VisualElement> elements)
        {
            if (IsAnimationEnable)
            {
                foreach (var element in elements)
                {
                    element.Scale = 0;
                }
            }
        }
    }
}
