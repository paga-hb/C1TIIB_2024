using AutoMapper;
using Todo.Dto;
using Todo.Blazor.Models;

namespace Todo.Blazor.Profiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        // Map Model to DTO
        // Note that "src.Id" isn't mapped to any "Id" property in the DTO
        // Note that "dest.Name" gets its value from "src.TaskName"
        CreateMap<TodoItem, TodoItemRequestDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
        .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));

        // Map DTO to Model
        // Note that "dest.TaskName" gets its value from "src.Name"
        CreateMap<TodoItemResponseDto, TodoItem>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));

        //CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}