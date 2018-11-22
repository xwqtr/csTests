namespace ConsoleApp6
{
    public interface IBaseWebService
    {
        T GetDataWebClient<T>(string uri);
    }
}