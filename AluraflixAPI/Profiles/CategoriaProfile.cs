using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;

namespace AluraflixAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaViewModel, Categoria>();
            CreateMap<Categoria, ReadCategoriaViewModel>();
            CreateMap<Categoria, ReadVideosPorCategoriaViewModel>()
                .ForMember(categoria => categoria.Videos, options => options
                .MapFrom(categoria => categoria.Videos.Select(video => new { video.Id, video.Titulo, video.Descricao })));

        }
    }
}
