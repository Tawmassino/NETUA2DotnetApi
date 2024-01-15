using NETUA2DotnetApi.Dtos;

namespace NETUA2DotnetApi.Services
{
    public interface ITodoValidationService
    {
        bool IsValid(CreateToDoItemDto dto);
        bool IsValid(UpdateToDoItemDto dto);
    }
    public class TodoValidationService : ITodoValidationService
    {
        /*
        type can be only: Work, Home, Hobby
        end date: cannot be in the past, cannot be more than 2 months in the future
        content: cannot be more than 200 characters long
        */
        private readonly string[] validTypes = new string[] { "Work", "Home", "Hobby" };

        public virtual bool IsValid(CreateToDoItemDto dto)
        {
            if (!validTypes.Contains(dto.Type))
            {
                return false;
            }

            if ( dto.EndDate != null && (((DateTime)dto.EndDate).Date < DateTime.Now.Date || ((DateTime)dto.EndDate).Date >= DateTime.Now.AddMonths(2).Date ))
            {
                return false;
            }

            if (dto.Content.Length > 200)
            {
                return false;
            }


            return true;
        }




        public virtual bool IsValid(UpdateToDoItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
