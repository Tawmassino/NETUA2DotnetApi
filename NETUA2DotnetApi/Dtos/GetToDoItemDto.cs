using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.Dtos
{
    public class GetToDoItemDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; }
    }
}
