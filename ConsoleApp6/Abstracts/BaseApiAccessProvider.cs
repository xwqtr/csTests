namespace CurrencyService.Abstracts
{
    using Newtonsoft.Json;
    using CurrencyService.Common.Interfaces;
    public abstract class BaseApiAccessProvider : IApiAccessProvider
    {
        public BaseApiAccessProvider() {
        }
        public abstract string CallUri(string uri);

        public T GetData<T>(string command) {
            return JsonConvert.DeserializeObject<T>(this.CallUri(command));
        }

    }
}
