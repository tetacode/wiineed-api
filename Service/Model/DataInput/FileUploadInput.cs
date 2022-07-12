namespace Service.Model.DataInput;

public class FileUploadInput
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
}