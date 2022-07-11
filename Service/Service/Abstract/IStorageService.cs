using Core.Service.Result;
using Data.Entity;

namespace Service.Service.Abstract;

public interface IStorageService
{
    public DataResult<Media> UploadMedia(Stream fileStream, string filePath, string fileName);
}