using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Balta.Models;
using Balta.View.ProductView;
using Balta.Repositories;

namespace Balta.Controllers
{
    public class ProductController
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;            
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductView> Get()
        {
            return _repository.Get();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public Product Get(int id)
        {
            return _repository.Get(id);
        }
    
        [Route("v1/products")]
        [HttpPost]
        public ResultView Post([FromBody]EditorProductView model)
        {
            model.Validate();
            if(model.Invalid)
            {
                return new ResultView
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto!",
                    Data = model.Notifications
                };
            }

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            //products.CreateDate = DateTime.Now;         //nunca recebe esta informação
            product.Description = model.Description;
            product.Image = model.Image;
            //products.LastUpdateDate = DateTime.Now;     //nunca recebe esta informação
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);

            return new ResultView
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };

        }

        [Route("v1/products")]
        [HttpPut]
        public ResultView Put([FromBody]EditorProductView model)
        {
            model.Validate();
            if(model.Invalid)
            {
                return new ResultView
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };
            }

            var product = _repository.Find(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            //products.CreateDate = DateTime.Now; // nunca altera a data de criação
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now; // nunca recebe esta informação
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Update(product);

            return new ResultView
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }
    }
}