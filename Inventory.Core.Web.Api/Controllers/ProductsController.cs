// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Core.Web.Api.Models.Products;
using Inventory.Core.Web.Api.Models.Students.Exceptions;
using Inventory.Core.Web.Api.Services.Foundations.Products;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace OtripleS.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : RESTFulController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService) =>
            this.productService = productService;

        [HttpPost]
        public async ValueTask<ActionResult<Product>> PostProductAsync(Product product)
        {
            try
            {
                Product addedProduct =
                    await this.productService.AddProductAsync(product);

                return Created(addedProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is AlreadyExistsProductException)
            {
                return Conflict(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Product>> GetAllProducts()
        {
            try
            {
                IQueryable<Product> storageProducts =
                    this.productService.RetrieveAllProducts();

                return Ok(storageProducts);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return Problem(productDependencyException.Message);
            }
            catch (ProductServiceException productServiceException)
            {
                return Problem(productServiceException.Message);
            }
        }

        [HttpGet("{productId}")]
        public async ValueTask<ActionResult<Product>> GetProductAsync(Guid productId)
        {
            try
            {
                Product storageProduct =
                    await this.productService.RetrieveProductByIdAsync(productId);

                return Ok(storageProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is NotFoundProductException)
            {
                string innerMessage = GetInnerMessage(productValidationException);

                return NotFound(innerMessage);
            }
            catch (ProductValidationException productValidationException)
            {
                string innerMessage = GetInnerMessage(productValidationException);

                return BadRequest(productValidationException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return Problem(productDependencyException.Message);
            }
            catch (ProductServiceException productServiceException)
            {
                return Problem(productServiceException.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Product>> PutProductAsync(Product product)
        {
            try
            {
                Product modifiedProduct =
                    await this.productService.ModifyProductAsync(product);

                return Created(modifiedProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is AlreadyExistsProductException)
            {
                return Conflict(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException);
            }
        }

        [HttpDelete("{productId}")]
        public async ValueTask<ActionResult<Product>> DeleteProductAsync(Guid productId)
        {
            try
            {
                Product storageProduct =
                    await this.productService.RemoveProductByIdAsync(productId);

                return Created(storageProduct);
            }
            catch (ProductValidationException productValidationException)
                when (productValidationException.InnerException is AlreadyExistsProductException)
            {
                return Conflict(productValidationException.InnerException);
            }
            catch (ProductValidationException productValidationException)
            {
                return BadRequest(productValidationException.InnerException);
            }
            catch (ProductDependencyException productDependencyException)
            {
                return InternalServerError(productDependencyException);
            }
            catch (ProductServiceException productServiceException)
            {
                return InternalServerError(productServiceException);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
            exception.InnerException.Message;
    }
}