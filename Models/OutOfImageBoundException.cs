namespace Graphics.Models;

public class OutOfImageBoundException : Exception
{
    public OutOfImageBoundException() { }
    public OutOfImageBoundException(string message) 
        : base(message) { }
    public OutOfImageBoundException(string message, Exception inner) 
        : base(message, inner) { }

}