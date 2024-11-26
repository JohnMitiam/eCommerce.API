using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Interfaces.Validator
{
    public interface IValidate<T> where T : class
    {
        Task<(bool isSuccess, List<string>? errorMessages)> IsValidAsync(T value);
        (bool isSuccess, List<string>? errorMessages) IsValid(T value);
    }
}
