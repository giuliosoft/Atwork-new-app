using System;
namespace AtWork.Helpers
{
    public interface PhoneCallEmail
    {
        void MakeQuickCall(string PhoneNumber);
        void ComposerEmail(string email, string AlertMessage);
    }
}
