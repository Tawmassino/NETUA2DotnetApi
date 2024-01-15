using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.Services
{
    public class ToDoEmailService: IToDoEmailService
    {
        public bool TrySendEmail(string emailAdress, TodoItem model)
        {
            Console.WriteLine($"El. pastas issiustas {emailAdress}");
            //kazkokia email siuntimo logika....
            return true;
        }
    }
}
