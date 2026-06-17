namespace Domain.Exceptions
{
    public class RetrievalException<T> : Exception
    {
        public RetrievalException()
            : base(FormatMessage()) { }

        public RetrievalException(long id)
            : base(FormatMessage(id)) { }

        private static string FormatMessage() => $"Failed to fetch entities of type {typeof(T).Name}.";

        private static string FormatMessage(long id) => $"Failed to fetch entity of type {typeof(T).Name} with id {id}.";
    }
}