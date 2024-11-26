using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Application.ResultModels;

namespace eCommerce.Application.Services.Base
{
    public abstract class BaseService
    {
        protected ServiceResult Result(bool isSuccess, string? message = null)
        {
            return new ServiceResult { IsSuccess = isSuccess, ErrorMessage = message };
        }

        protected ServiceResult<T> Result<T>(T data, bool isSuccess, string? message = null)
        {
            return new ServiceResult<T> { IsSuccess = isSuccess, ErrorMessage = message, Data = data };
        }

        protected ServiceResult FailedResult(string message)
        {
            return Result(false, message);
        }

        protected ServiceResult SuccessResult()
        {
            return Result(true);
        }

        protected ServiceResult<T> SuccessResult<T>(T data)
        {
            return Result<T>(data, true);
        }
    }
}
