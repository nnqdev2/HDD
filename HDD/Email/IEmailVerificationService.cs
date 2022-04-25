namespace HDD.Email
{
    public interface IEmailVerificationService
    {
        void SendVerificationCodeEmail(string vin, string plate, string email);
    }
}
