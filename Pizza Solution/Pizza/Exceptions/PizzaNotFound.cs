namespace Pizza.Exceptions
{
    public class PizzaNotFound : Exception
    {
        public PizzaNotFound(string message): base(message) { }
    }
}
