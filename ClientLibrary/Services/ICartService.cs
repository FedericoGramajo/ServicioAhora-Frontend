using ClientLibrary.Models;
using ClientLibrary.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services
{
    public interface ICartService
    {
        Task<ServiceResponse> Checkout(Checkout checkout);
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
        Task<IEnumerable<GetAchieve>> GetAchieves();
    }
}
