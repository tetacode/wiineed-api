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
}