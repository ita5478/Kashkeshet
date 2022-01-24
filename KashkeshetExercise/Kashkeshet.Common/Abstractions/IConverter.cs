namespace Kashkeshet.Common.Abstractions
{
    public interface IConverter<Tinput, Toutput>
    {
        Toutput ConvertTo(Tinput input);

        Tinput ConvertFrom(Toutput input);
    }
}
