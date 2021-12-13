namespace VerstaTask.Interfaces
{
    using VerstaTask.Models;

    public interface IErrorService
    {
        public ErrorViewModel GenerateError<T>(long id)
            where T : class;
    }
}
