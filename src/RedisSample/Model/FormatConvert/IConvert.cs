namespace RedisSample.Model.FormatConvert
{
    public interface IConvert
    {
        T FromJson<T>(string csvStr);
    }
}