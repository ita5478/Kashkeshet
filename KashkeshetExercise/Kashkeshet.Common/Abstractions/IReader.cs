namespace Common.Abstractions
{
    public interface IReader<out T>
    {
        T Read();
    }
}
