namespace Pizza.Exceptions
{
    public class ToppingNotFound : Exception
    {
        public ToppingNotFound(string message):base(message) { }
    }
}
