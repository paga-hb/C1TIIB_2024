using AutoMapper;
using Todo.Dto;
using Todo.Data.Entities;

namespace Todo.Api.Profiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        // Map Entity to DTO
        // Note that "dest.Done" gets its value from "src.Completed"
        // Note that there is no "dest.Comments" to match "src.Comments"
        CreateMap<TodoItem, TodoItemResponseDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Completed));

        // Map DTO to Entity
        // Note that "dest.Id" has its value set to 0
        // Note that "dest.Completed" gets its value from "src.Done"
        // Note that "dest.Comments" has its value set to string.Empty
        CreateMap<TodoItemRequestDto, TodoItem>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Done))
        .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => string.Empty));

        //CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}