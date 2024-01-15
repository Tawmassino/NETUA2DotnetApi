using Microsoft.EntityFrameworkCore;
using NETUA2DotnetApi.DataLayer.FakeDatabase;
using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.DataLayer;

public class TodoContext: DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }
    public DbSet<TodoItem> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .HasData(TodoFakeDatabase.todoList);
    }


}
