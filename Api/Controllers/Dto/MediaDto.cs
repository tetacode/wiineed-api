using Data.StaticRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Controllers.Dto;

public class MediaDto
{
    public Guid Key { get; set; }
    
    public string OriginalName { get; set; }
    public string Src { get; set; }
    public string ThumbnailSrc { get; set; }
    public int Order { get; set; }
    
    public MediaTypeEnum MediaType { get; set; }
    public DateTime CreatedDate { get; set; }
    public Boolean Enabled { get; set; }
}