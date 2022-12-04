using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public partial class EFxceptionService<TDbException>
    {
        private void ValidateInnerException(DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException == null)
            {
                throw dbUpdateException;
            }
        }
    }
}
