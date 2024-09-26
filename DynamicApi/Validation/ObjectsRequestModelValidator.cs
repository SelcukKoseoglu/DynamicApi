using DynamicAPI.DTO;
using FluentValidation;

namespace DynamicAPI.Validation
{
    public class ObjectsRequestModelValidator : AbstractValidator<ObjectsRequestModel>
    {
        public ObjectsRequestModelValidator()
        {
            RuleFor(x => x.ObjectType)
                .NotEmpty().WithMessage("ObjectType is required.")
                .Must(BeAValidObjectType).WithMessage("Invalid ObjectType.");

            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("Data is required.")
                .Must((request, data) => HaveValidFields(request, data))
                .WithMessage("Data contains invalid or missing fields.");
        }


        private bool BeAValidObjectType(string objectType)
        {
            var validObjectTypes = new List<string> { "product", "order" };
            return validObjectTypes.Contains(objectType);
        }

        private bool HaveValidFields(ObjectsRequestModel request, Dictionary<string, object> data)
        {
            if (request.ObjectType == "product")
            {
                return ValidateProductFields(data);
            }
            else if (request.ObjectType == "order")
            {
                return ValidateOrderFields(data);
            }

            return false;
        }

        private bool ValidateProductFields(Dictionary<string, object> data)
        {
            if (!data.ContainsKey("name") || string.IsNullOrWhiteSpace(data["name"].ToString()))
                return false;
            if (!data.ContainsKey("price") || !IsNumeric(data["price"]))
                return false;
            if (!data.ContainsKey("quantity") || !IsNumeric(data["quantity"]))
                return false;

            return true;
        }

        private bool ValidateOrderFields(Dictionary<string, object> data)
        {
            if (!data.ContainsKey("customerId") || !IsNumeric(data["customerId"]))
                return false;

            if (!data.ContainsKey("orderDate") || !IsValidDateTime(data["orderDate"]))
                return false;

            if (!data.ContainsKey("totalAmount") || !IsNumeric(data["totalAmount"]))
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
