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
                .ConvertUsing(entity => Convert(entity));
            CreateMap<ZettelEntity, ZettelData>()
                .ConvertUsing((entity, data) =>
                {
                    data.Id = entity.Id;
                    data.Title = entity.Title;
                    data.Content = entity.Content;
                    return data;
                });
            CreateMap<ZettelData, ZettelEntity>()
                .ConvertUsing(data => Convert(data));
        }
        private static ZettelData Convert(ZettelEntity entity)
        {
            Console.WriteLine(entity.Id.ToString());
            Console.WriteLine(entity.Title);
            Console.WriteLine(entity.Content);
            return new ZettelData()
            {
                Id = entity.Id,
                Title = (string)entity.Title,
                Content = (string)entity.Content
            };
        }
        private static ZettelEntity Convert(ZettelData data)
        {
            Console.WriteLine(data.Id.ToString());
            Console.WriteLine(data.Title);
            Console.WriteLine(data.Content);
            return new ZettelEntity(
            new ZettelId(data.Id),
                    new Title(data.Title),
                    new ZettelContent(data.Content));

        }
    }
}