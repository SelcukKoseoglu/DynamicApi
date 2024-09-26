using Azure.Core;
using DynamicAPI.Business.DynamicObjectMethods;
using DynamicAPI.Business.Helper;
using DynamicAPI.DAL.Context;
using DynamicAPI.DTO;
using DynamicAPI.Entity.DynamicObjectDb;
using DynamicAPI.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;

namespace DynamicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        private readonly IObjectMethods _objectMethods;
        private readonly ObjectHelper _objectHelper;
        private readonly DynamicApiDbContext _context;

        public ObjectController(IObjectMethods objectMethods, ObjectHelper objectHelper, DynamicApiDbContext context)
        {
            _objectMethods = objectMethods;
            _objectHelper = objectHelper;
            _context = context;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateDynamicObject([FromBody] ObjectsRequestModel request)
        {
            var validator = new ObjectsRequestModelValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var dynamicObject = new Objects
            {
                Type = request.ObjectType,
                ObjectData = JsonConvert.SerializeObject(_objectHelper.ConvertValuesToStrings(request.Data)), 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _objectMethods.AddObject(dynamicObject).ConfigureAwait(false);

            return Ok();
        }
        [HttpGet("{type}/{id}")]
        public async Task<IActionResult> GetDynamicObject(string type, int id)
        {
            var dynamicObject = await _objectMethods.GetObject(type, id).ConfigureAwait(false);

            if (dynamicObject == null)
                return NotFound();

            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dynamicObject.ObjectData);
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDynamicObject(int id, [FromBody] Dictionary<string, object> updatedData)
        {
            var dynamicObject = await  _objectMethods.GetObjectById(id).ConfigureAwait(false);

            if (dynamicObject == null)
                return NotFound();

            dynamicObject.ObjectData = JsonConvert.SerializeObject(_objectHelper.ConvertValuesToStrings(updatedData));
            dynamicObject.UpdatedAt = DateTime.Now;

            await _objectMethods.UpdateObject(dynamicObject).ConfigureAwait(false);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicObject(int id)
        {
            var dynamicObject = await _objectMethods.GetObjectById(id).ConfigureAwait(false);

            if (dynamicObject == null)
                return NotFound();

            await _objectMethods.DeleteObject(dynamicObject).ConfigureAwait(false);

            return Ok();
        }


        [HttpPost("createTransaction")]
        public async Task<IActionResult> CreateOrderWithProducts([FromBody] OrderTransactionRequestModel request)
        {
            var validator = new OrderTransactionRequestModelValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var orderObject = new Objects
                {
                    Type = "order",
                    ObjectData = JsonConvert.SerializeObject(_objectHelper.ConvertValuesToStrings(request.OrderData)),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Objects.Add(orderObject);
                await _context.SaveChangesAsync();

                foreach (var product in request.Products)
                {

                    var productObject = new Objects
                    {
                        Type = "product",
                        ObjectData = JsonConvert.SerializeObject(_objectHelper.ConvertValuesToStrings(product)),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.Objects.Add(productObject);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { OrderId = orderObject.Id });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { Message = "Transaction failed", Error = ex.Message });
            }
        }


    }
}
