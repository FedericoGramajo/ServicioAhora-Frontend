using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helper
{
    public interface IHttpClientHelper
    {
        HttpClient GetPublicClient();
        Task<HttpClient> GetPrivateClientAsync();

    }
}
