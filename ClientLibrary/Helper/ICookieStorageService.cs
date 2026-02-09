using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helper
{

    public interface ICookieStorageService
    {
        Task SetCookieAsync(string key, string value, int? days = null, string path = "/");
        Task<string?> GetCookieAsync(string key);
        Task RemoveCookieAsync(string key, string path = "/");
    }

}
