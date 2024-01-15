using NETUA2DotnetApi.DataLayer.Models;

namespace NETUA2DotnetApi.Dtos
{
    public class UpdateToDoItemDto
    {
        public long Id { get; set; }

        /// <summary>
        ///   type can be only: Work, Home, Hobby
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// content: cannot be more than 200 characters long
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// end date: cannot be in the past, cannot be more than 2 months in the future
        /// </summary>
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; }
    }
}
