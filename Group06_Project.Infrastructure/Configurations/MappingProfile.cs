using AutoMapper;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Models;

namespace Group06_Project.Infrastructure.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, SelectOption>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<Genre, SelectOption>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

        CreateMap<Film, FilmItemList>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres.Select(g => new SelectOption { Value = g.Id, Label = g.Name })));

        CreateMap<Film, FilmItemDetail>()
            .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src =>
                    src.Country != null ? new SelectOption { Value = src.Country.Id, Label = src.Country.Name } : null))
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres.Select(g => new SelectOption { Value = g.Id, Label = g.Name })));

        CreateMap<Film, FilmListExport>();
    }
}