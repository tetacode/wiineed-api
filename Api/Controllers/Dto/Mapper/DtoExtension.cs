using AutoMapper;
using Core.Service.Result;

namespace Api.Controllers.Dto.Mapper;

public static class DtoExtension
{
    private static readonly MapperConfiguration MapperConfiguration = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });

    private static readonly IMapper Mapper = MapperConfiguration.CreateMapper();
    
    public static DataResult<TDto> As<TEntity, TDto>(this DataResult<TEntity> singleResult)
    {
        var data = Mapper.Map<TDto>(singleResult.Data);

        return new DataResult<TDto>(data);
    }
    
    public static DataListResult<TDto> As<TEntity, TDto>(this DataListResult<TEntity> result)
    {
        var data = Mapper.ProjectTo<TDto>(result.Data.AsQueryable());
        
        return new DataListResult<TDto>(data);
    }
    
    public static DataGridResult<TDto> As<TEntity, TDto>(this DataGridResult<TEntity> result)
    {
        var data = Mapper.ProjectTo<TDto>(result.Data.AsQueryable());
        
        return new DataGridResult<TDto>(data, result.Pagination.PageNumber, result.Pagination.PageSize, result.Pagination.TotalRecords);
    }
}