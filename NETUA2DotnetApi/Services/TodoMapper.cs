using NETUA2DotnetApi.DataLayer.Models;
using NETUA2DotnetApi.Dtos;

namespace NETUA2DotnetApi.Services
{
    public interface ITodoMapper
    {
        GetToDoItemDto Map(TodoItem model);
        IEnumerable<GetToDoItemDto> Map(IEnumerable<TodoItem> models);
        TodoItem Map(CreateToDoItemDto dto);
        TodoItem Map(UpdateToDoItemDto dto);
    }
    public class TodoMapper: ITodoMapper
    {
        public GetToDoItemDto Map(TodoItem model)
        {
            return new GetToDoItemDto
            {
                Id = model.Id,
                Type = model.Type,
                Content = model.Content,
                EndDate = model.EndDate,
                UserId = model.UserId
            };
        }
        public IEnumerable<GetToDoItemDto> Map(IEnumerable<TodoItem> models)
        {
            return models.Select(Map);
        }
        public TodoItem Map(CreateToDoItemDto dto)
        {
            return new TodoItem
            {
                Type = dto.Type,
                Content = dto.Content,
                EndDate = dto.EndDate,
                UserId = dto.UserId
            };
        }
        public TodoItem Map(UpdateToDoItemDto dto)
        {
            return new TodoItem
            {
                Id = dto.Id,
                Type = dto.Type,
                Content = dto.Content,
                EndDate = dto.EndDate,
                UserId = dto.UserId
            };
        }
    }
}
