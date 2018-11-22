using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class WebServiceAccess<T> where T : BaseWebService
    {

        private readonly T _webService;
        public WebServiceAccess(T webService) {
            _webService = webService;

        }

        public TResult FetchData<TResult>(string uri) {
   
            return _webService.GetDataWebClient<TResult>(uri);
        }
    }
}
