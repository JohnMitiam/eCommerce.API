using eCommerce.Application.Interfaces.Validator;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Validators.ProductValidators
{
    public class ProductValidator : IProductValidator
    {
        public async Task<(bool isSuccess, List<string>? errorMessages)> IsValidAsync(Product value)
        {
            return await Task.FromResult(IsValid(value));
        }

        public (bool isSuccess, List<string>? errorMessages) IsValid(Product value)
        {
            var nameLengthValidator = new ProductNameLengthValidator();
            var descriptionLenghtValidator = new ProductDescriptionLengthValidator();

            var productValidator = nameLengthValidator.And(descriptionLenghtValidator);

            var result = productValidator.IsValid(value);
            return (result.isSuccess, result.errorMessages);
        }
    }
}
