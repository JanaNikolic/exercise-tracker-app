namespace Domain.Exceptions
{
    public class CreationException<T> : Exception
    {

        public CreationException()
            : base(FormatMessage())
        {
        }

        private static string FormatMessage() => $"Failed to create entity of type {typeof(T).Name}.";
    }
}