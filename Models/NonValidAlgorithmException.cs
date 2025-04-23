namespace Graphics.Models;

public class NonValidAlgorithmException :Exception
{
    public NonValidAlgorithmException() { }
    public NonValidAlgorithmException(string message) 
        : base(message) { }
    public NonValidAlgorithmException(string message, Exception inner) 
        : base(message, inner) { }
}