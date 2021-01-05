namespace coreapi.MongoModel.Settings
{
    public class AppSettings
    {
        public SendMailInfo SendMailInfo { get; set; }
        public RequestAuth RequestAuth { get; set; }
    }
    public class SendMailInfo
    {
        public string SMTPServerName { get; set; }
        public string SendEmailAdress { get; set; }
        public string SendEmailPwd { get; set; }
        public string SiteName { get; set; }
        public int SendEmailPort { get; set; }
    }
    public class RequestAuth
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
