using System;
using CollegeIdManagement.Data;
using CollegeIdManagement.Models;

namespace CollegeIdManagement.Services
{
    public static class AuditService
    {
        public static void Log(string username, string action, string entity, string details)
        {
            try
            {
                using var context = new AppDbContext();
                context.AuditLogs.Add(new AuditLog
                {
                    Username = string.IsNullOrEmpty(username) ? (AuthenticationService.CurrentUser?.Username ?? "System") : username,
                    Action = action,
                    Entity = entity,
                    Details = details,
                    Timestamp = DateTime.Now
                });
                context.SaveChanges();
            }
            catch
            {
                // Non-blocking log failure
            }
        }
    }
}
