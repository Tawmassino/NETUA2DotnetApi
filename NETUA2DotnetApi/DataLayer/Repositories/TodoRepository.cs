using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.DataLayer.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        public long Add(TodoItem item)
        {
            _context.Todos.Add(item);
            _context.SaveChanges();
            return item.Id;
           
        }

        public void Delete(int key)
        {
            var item = _context.Todos.Find((long)key);
            _context.Todos.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.Todos.ToList();
        }

        public TodoItem GetById(int key)
        {
            return _context.Todos.Find((long)key);
        }

        public void Update(TodoItem item)
        {
            _context.Todos.Update(item);
            _context.SaveChanges();
        }
    }
}
