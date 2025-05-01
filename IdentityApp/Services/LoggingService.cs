using System.Text.Json;
using System.Threading.Tasks;
using IdentityApp.Database;
using IdentityApp.Models;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
//using Newtonsoft.Json;

namespace IdentityApp.Services
{
    public class LoggingService
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection of the ApplicationDbContext
        public LoggingService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Flat Dynamic Masking
        // Method to mask sensitive fields in the request data
        private string MaskSensitiveData(string requestData)
        {
            var sensitiveFields = new[] { "Password", "ConfirmPassword", "NewPassword", "OldPassword","Pin" };

            if (string.IsNullOrWhiteSpace(requestData))
                return requestData;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(requestData, options);

                if (data != null)
                {
                    foreach (var key in sensitiveFields)
                    {
                        if (data.ContainsKey(key))
                        {
                            data[key] = "****";
                        }
                    }
                    return JsonSerializer.Serialize(data);
                }
            }
            catch
            {
                // If not a valid JSON, just return the original string
                return requestData;
            }

            return requestData;
        }


        // Central method to handle logging
        public void LogToDatabase<T>(T logEntry) where T : class
        {
            try
            {
                _context.Set<T>().Add(logEntry);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // This will help you understand what exactly went wrong
                Console.WriteLine("Logging failed: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                throw; // Optional: rethrow or handle accordingly
            }
        }


        // Log for Application Logs (general system logs)
        public void LogApplication(
           string userId,
            string logLevel,
            string module,
            string action,
            string message,
            string exception,
            string ipAddress ,
            string requestParameters ,
            string responseParameters )

        {


            // Mask sensitive data in request parameters
            var maskedRequestParams = string.IsNullOrEmpty(requestParameters) ? null : MaskSensitiveData(requestParameters);

            var appLog = new TApplicationLogs
            {
                ID = Guid.NewGuid(),
                UserID = userId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                LogLevel = logLevel,
                Module = module,
                Action = action,
                Message = message,
                Exception = exception,
                IPAddress = ipAddress,
                RequestParameters = maskedRequestParams,
                ResponseParameters = responseParameters 
            };

            LogToDatabase(appLog);  // Use the generic method to log
        }

        // Similarly, other logging methods can be added here
    }
}
