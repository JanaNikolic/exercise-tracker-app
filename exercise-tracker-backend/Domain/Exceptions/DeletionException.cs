namespace Domain.Exceptions
{
    public class DeletionException<T> : Exception
    {
        public DeletionException(long id)
            : base(FormatMessage(id))
        {
        }
        private static string FormatMessage(long id) => $"Failed to delete entity of type {typeof(T).Name} with id {id}.";
    }
}