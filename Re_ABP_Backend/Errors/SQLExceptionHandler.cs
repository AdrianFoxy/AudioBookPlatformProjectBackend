using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Re_ABP_Backend.Errors
{
    public static class SQLExceptionHandler
    {
        public static bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   (sqlException.Number == 2601 || sqlException.Number == 2627);
        }
    }
}
