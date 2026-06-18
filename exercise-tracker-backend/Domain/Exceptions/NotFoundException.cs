namespace Domain.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(long id)
            : base(FormatMessage(id)) { }

        public NotFoundException(string? email)
            : base(FormatMessage(email)) { }

        private static string FormatMessage(long id) => $"{typeof(T).Name} with the Id {id} was not found.";

        private static string FormatMessage(string? email) => $"{typeof(T).Name} with the email {email} was not found.";
    }
}