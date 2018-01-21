using System;
using AutoMapper;
using AutoMapper.Mappers;
using CookBook.BL.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    internal class CookBookMapperConfiguration : MapperConfiguration
    {
        public CookBookMapperConfiguration() : base(
            cfg =>
            {
                cfg.CreateMap<Guid, Guid>().ConvertUsing(s => s == Guid.Empty ? Guid.NewGuid() : s);

                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");

                cfg.CreateMap<IngredientAmountEntity, IngredientDetailDto>()
                    .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Ingredient.Description))
                    .ForMember(d => d.IngredientId, opt => opt.MapFrom(src => src.Ingredient.Id));
            })
        {
            this.AssertConfigurationIsValid();
        }
    }
}