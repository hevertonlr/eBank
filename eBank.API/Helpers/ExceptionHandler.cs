using GraphQL;
using System.Threading.Tasks;

namespace eBank.API.Helpers
{
    public static class ExceptionHandler
    {
        public static void HandleErrors(this Task task)
        {
            if (task.Exception != null)
                throw new ExecutionError(task.Exception.InnerException?.Message ?? task.Exception.Message);
        }
    }
}
