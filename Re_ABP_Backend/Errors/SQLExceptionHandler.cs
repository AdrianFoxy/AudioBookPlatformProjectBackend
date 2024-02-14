using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;

namespace Re_ABP_Backend.Errors
{
    public static class SQLExceptionHandler
    {
        public static bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            return ex?.InnerException is SqlException sqlException &&
                   (sqlException.Number == 2601 || sqlException.Number == 2627);
        }

        public static ActionResult HandleDbUpdateException(DbUpdateException ex, IStringLocalizer sharedResourceLocalizer, string uniqExceptionText)
        {
            if (IsUniqueConstraintViolationException(ex))
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    if (innerException is SqlException sqlException)
                    {
                        if (sqlException.Number == 2601 || sqlException.Number == 2627)
                        {
                            Log.Error($"Unique constraint violation occurred. Details: {sqlException.Message}");
                            return new BadRequestObjectResult(new ApiResponse(400, sharedResourceLocalizer.GetString(uniqExceptionText)));
                        }
                    }
                    innerException = innerException.InnerException;
                }
            }
            Log.Error("Error updating: " + ex.Message);
            return new BadRequestObjectResult(new ApiResponse(400, sharedResourceLocalizer.GetString("ProblemDuringUpdating")));
        }
    }
}
