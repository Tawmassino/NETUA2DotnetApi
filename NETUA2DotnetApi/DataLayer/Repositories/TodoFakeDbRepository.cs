using NETUA2DotnetApi.DataLayer.FakeDatabase;
using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.DataLayer.Repositories
{

    public class TodoFakeDbRepository: ITodoRepository
    {
        private readonly List<TodoItem> _database;
        public TodoFakeDbRepository()
        {
            _database = new TodoFakeDatabase().GetAll();
        }
        public IEnumerable<TodoItem> GetAll()
        {
            return _database;
        }
        public TodoItem GetById(int key)
        {
            return _database.Find(t => t.Id == key);
        }

        public long Add(TodoItem item)
        {
            var newId = _database.Max(t => t.Id) + 1;
            item.Id = newId;
            _database.Add(item);
            return newId;
        }
        public void Update(TodoItem item)
        {
            var index = _database.FindIndex(t => t.Id == item.Id);
            _database[index] = item;
        }
        public void Delete(int key)
        {
            var index = _database.FindIndex(t => t.Id == key);
            _database.RemoveAt(index);
        }







    }

}
