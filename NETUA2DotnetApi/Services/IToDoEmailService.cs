using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.Services
{
    public interface IToDoEmailService
    {
        bool TrySendEmail(string to, TodoItem model);
    }
}
