namespace VerstaTask.Services
{
    using VerstaTask.Interfaces;
    using VerstaTask.Models;

    public class ErrorService : IErrorService
    {
        public ErrorViewModel GenerateError<T>(long id)
            where T : class
        {
            var error = new ErrorViewModel
            {
                EntityName = nameof(T),
                EntityId = id.ToString(),
                ErrorMessage = $"Не найдена сущность {nameof(T)} с id = {id}."
            };
            return error;
        }
    }
}
