using System;
namespace AtWork.Helpers
{
    public class ExceptionHelper
    {
        public ExceptionHelper()
        {
        }
		public static bool CommanException(Exception exception)
		{
			try
			{
				System.Diagnostics.Debug.WriteLine(exception.Message + exception.StackTrace);
				return true;
			}
			catch (Exception )
			{
				return false;
			}
		}
	}
}
