using System;
using AutoMapper;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<ZettelEntity, ZettelData>()
                .ConvertUsing(entity => new ZettelData()
                {
                    Id = Guid.Parse(entity.Id),
                    Title = entity.Title,
                    Content = entity.Content
                });
            CreateMap<ZettelData, ZettelEntity>()
                .ConvertUsing(data => new ZettelEntity(
                    new ZettelId(data.Id.ToString()),
                    new Title(data.Title),
                    new ZettelContent(data.Content)
                ));
        }
    }
}