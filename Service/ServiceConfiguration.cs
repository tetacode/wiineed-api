namespace Service;

public static class ServiceConfiguration
{
    public class App
    {
        public string Disk { get; set; }
        
        public string DefaultReportOutputTemplateFile { get; set; }
        public string ReportGenerateApi { get; set; }
        public string WeasisName { get; set; }
    }
    
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
    
    public class S3
    {
        public string ServiceUrl { get; set; }
        public string PublicDomain { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }
    }
}