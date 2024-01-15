using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.DataLayer.Repositories
{
    public interface ITodoRepository
    {
        long Add(TodoItem item);
        void Delete(int key);
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(int key);
        void Update(TodoItem item);
    }

}
