
namespace Common.Abstractions
{
    public interface IParser<out T>
    {
        T Parse(string data);
    }
}
