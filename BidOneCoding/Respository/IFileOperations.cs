namespace BidOneCoding.Respository
{
    public interface IFileOperations
    {
        string Read();
        void Write(string jsonString);
    }
}