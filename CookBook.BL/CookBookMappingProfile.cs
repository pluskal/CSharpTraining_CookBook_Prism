using System;
using AutoMapper;
using AutoMapper.Mappers;
using CookBook.Common.Models;
using CookBook.DAL.Entities;

namespace CookBook.BL
{
    public class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile()
        {
            this.CreateMap<Guid, Guid>().ConvertUsing(s => s == Guid.Empty ? Guid.NewGuid() : s);

            this.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");

            this.CreateMap<IngredientAmountEntity, IngredientDetailDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Ingredient.Description))
                .ForMember(d => d.IngredientId, opt => opt.MapFrom(src => src.Ingredient.Id));
        }
    }
}