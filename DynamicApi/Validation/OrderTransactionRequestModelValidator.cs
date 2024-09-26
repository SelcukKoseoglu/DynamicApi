using System.Collections.Generic;
using DynamicAPI.DTO;
using FluentValidation;

namespace DynamicAPI.Validation
{
    public class OrderTransactionRequestModelValidator : AbstractValidator<OrderTransactionRequestModel>
    {
        public OrderTransactionRequestModelValidator()
        {
            RuleFor(x => x.OrderData)
                .NotEmpty().WithMessage("OrderData is required.")
                .Must(HaveValidOrderFields).WithMessage("OrderData contains invalid or missing fields.");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("Products are required.")
                .Must(ContainValidProducts).WithMessage("At least one valid product is required.");
        }

        private bool HaveValidOrderFields(Dictionary<string, object> orderData)
        {
            // OrderData için gerekli alanların validasyonu
            if (!orderData.ContainsKey("customerId") || !IsNumeric(orderData["customerId"]))
                return false;
            if (!orderData.ContainsKey("orderDate") || !IsValidDateTime(orderData["orderDate"]))
                return false;
            if (!orderData.ContainsKey("totalAmount") || !IsNumeric(orderData["totalAmount"]))
                return false;

            return true;
        }

        private bool ContainValidProducts(List<Dictionary<string, object>> products)
        {
            // En az bir geçerli ürün olup olmadığını kontrol et
            foreach (var product in products)
            {
                if (!ValidateProductFields(product))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateProductFields(Dictionary<string, object> product)
        {
            // Ürün verileri için validasyon
            if (!product.ContainsKey("name") || string.IsNullOrWhiteSpace(product["name"].ToString()))
                return false;
            if (!product.ContainsKey("price") || !IsNumeric(product["price"]))
                return false;
            if (!product.ContainsKey("quantity") || !IsNumeric(product["quantity"]))
                return false;

            return true;
        }

        private bool IsNumeric(object value)
        {
            if (value == null)
                return false;

            return decimal.TryParse(value.ToString(), out _) ||
                   double.TryParse(value.ToString(), out _);
        }

        private bool IsValidDateTime(object value)
        {
            if (value == null)
                return false;

            return DateTime.TryParse(value.ToString(), out _);
        }
    }
}
