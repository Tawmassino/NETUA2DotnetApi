using NETUA2DotnetApi.Dtos;

namespace NETUA2DotnetApi.Services
{
    public class TodoValidationService2: TodoValidationService
    {
        private readonly string[] validTypes = new string[] { "Work", "Home", "Hobby", "Other" };
        public override bool IsValid(CreateToDoItemDto dto)
        {
            if (!validTypes.Contains(dto.Type))
            {
                return false;
            }

            if (dto.EndDate != null && (((DateTime)dto.EndDate).Date < DateTime.Now.Date || ((DateTime)dto.EndDate).Date >= DateTime.Now.AddMonths(2).Date))
            {
                return false;
            }

            if (dto.Content.Length > 200)
            {
                return false;
            }
            //nujas funkcionalumas

            return true;
        }
    }

}
