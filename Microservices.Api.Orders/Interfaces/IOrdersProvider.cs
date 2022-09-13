using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
