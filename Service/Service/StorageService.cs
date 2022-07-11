using System.Net.Mime;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Core.Service;
using Core.Service.Exception;
using Core.Service.Result;
using Data.Entity;
using Data.StaticRepository;
using Microsoft.Extensions.Options;
using Service.Service.Abstract;

namespace Service.Service;

public class StorageService : IStorageService
{
    private ServiceConfiguration.S3 _s3;

    public StorageService(IOptions<ServiceConfiguration.S3> s3)
    {
        _s3 = s3.Value;
    }

    private IAmazonS3? _amazonS3;
    private readonly List<string> _extensions = new List<string>{".jpeg", ".jpg", ".png", ".gif", ".mp4"};

    private IAmazonS3 S3Client()
    {
        return _amazonS3 ??= new AmazonS3Client(_s3.AccessKey, _s3.SecretKey, new AmazonS3Config()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_s3.Region),
            ServiceURL = _s3.ServiceUrl
        });
    }

    public MediaTypeEnum GetMediaType(string extension)
    {
        return extension switch
        {
            ".mp4" => MediaTypeEnum.VIDEO,
            ".jpeg" => MediaTypeEnum.IMAGE,
            ".jpg" => MediaTypeEnum.IMAGE,
            ".png" => MediaTypeEnum.IMAGE,
            ".gif" => MediaTypeEnum.GIF,
            _ => MediaTypeEnum.IMAGE
        };
    }

    public DataResult<Media> UploadMedia(Stream fileStream, string filePath, string fileName)
    {
        if (fileStream is null || fileStream.Length <= 0)
            throw new ServiceException("File Error!");

        var extension = Path.GetExtension(fileName);
        if(!_extensions.Contains(extension))
            throw new ServiceException("File Type Error!");

        var media = new Media();
        media.Name = $"{media.Id}{extension}";
        media.Path = $"{filePath ?? ""}/{media.Name}";
        media.OriginalName = fileName;
        media.MediaType = GetMediaType(extension);
        media.ThumbnailSrc = $"https://{_s3.BucketName}.{_s3.PublicDomain}/{media.Path}";
        media.Src = $"https://{_s3.BucketName}.{_s3.PublicDomain}/{media.Path}";

        var res = S3Client().PutObjectAsync(new PutObjectRequest()
        {
            BucketName = _s3.BucketName,
            InputStream = fileStream,
            Key = $"{filePath ?? ""}/{media.Name}",
            CannedACL = S3CannedACL.PublicRead,
        }).GetAwaiter().GetResult();

        return media.DataResult();
    }
}