using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;

namespace AluraflixAPI.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<CreateVideoViewModel, Video>();
            CreateMap<Video, ReadVideoViewModel>();
        }
    }
}
