using Microsoft.Data.SqlClient;

namespace BNPL_Web.smsService
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        public SmsService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public bool SendSMS(int OtpCode, string ContactNumber)
        {

            var To = ContactNumber;
            var text = "Hye Verification Code is" + OtpCode;

            var commandText = "INSERT INTO InsertSms (Body, ToAddress, FromAddress, ChannelID,StatusID, DataCoding, CustomField1, CustomField2) VALUES (@body, @to, @FromAddress, @ChannelID,'SCHEDULED',0,@CustomField1, @CustomField2);";
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMSConnection")))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@to", To);

                command.Parameters.AddWithValue("@body", text);

                command.Parameters.AddWithValue("@FromAddress", this._configuration.GetValue<String>("AppSettings:SMS_DevSmsService_FromAddress"));



                command.Parameters.AddWithValue("@ChannelId", this._configuration.GetValue<String>("AppSettings:SMS_DevSmsService_ChannelId"));
                var configvalue1 = this._configuration.GetValue<String>("AppSettings:SMS_DevSmsService_CustomField2_en");
                command.Parameters.AddWithValue("@CustomField1", this._configuration.GetValue<String>("AppSettings:SMS_DevSmsService_CustomField1"));

                command.Parameters.AddWithValue("@CustomField2", this._configuration.GetValue<String>("AppSettings:'SMS_DevSmsService_CustomField2_en'"));
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
