namespace CSharpFundamentals
{
    public interface IPrinter
    {
        void Print(string entry);
        string[] entriesSequense { get; }
    }
}
