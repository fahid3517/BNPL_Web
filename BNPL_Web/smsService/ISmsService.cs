namespace BNPL_Web.smsService
{
    public interface ISmsService
    {
        int GenerateRandomNo();
        bool SendSMS(int OtpCode, string ContactNumber);
    }
}
