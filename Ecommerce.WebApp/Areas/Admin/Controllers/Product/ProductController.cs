using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Services;
using Ecommerce.Data.EF;
using Ecommerce.Data.Entities;
using Ecommerce.WebApp.Areas.Admin.ProductModel;
using Ecommerce.WebApp.Areas.Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApp.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("admin")]
    //[Route("admin/[controller]")]

    public class ProductController : MessageController
    {
        IUnitOfWork UOW;
        IRepository<Product> _productRepository;
        IRepository<ProductCategory> _productCategoryRepository;
        IRepository<ProductImage> _productImageRepository;

        public ProductController(IUnitOfWork uow)
        {
            UOW = uow;
            _productRepository = uow.GetRepository<Product>();
            _productCategoryRepository = uow.GetRepository<ProductCategory>();
            _productImageRepository = uow.GetRepository<ProductImage>();
        }
        [Route("admin/products")]
        public IActionResult List()
        {
            return View();
        }
        [Route("admin/product/list")]
        public ProductListModel Get()
        {
          
            var productList = ProductListModel.Get(_productRepository, _productCategoryRepository);
         
            return productList;
        }
        [Route("/admin/product/add")]
        public ProductEditModel Add()
        {
            return ProductEditModel.Create(_productRepository, _productCategoryRepository, _productImageRepository);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
       
        [Route("/admin/product/{id?}")]
        public IActionResult Edit(int id)
        {
            ViewBag.productID = id;
            return View();
        }
        [Route("/admin/product/edit/{id}")]
        public ProductEditModel Get(int id)
        {
            return ProductEditModel.GetById(_productRepository, _productCategoryRepository, _productImageRepository, id);
        }
        [HttpPost]
        [Route("/admin/product/saves")]
        public IActionResult Saves(IFormCollection files)
        {
            var g = new ProductCategory();
            //IFormCollection filess = files;
            return Ok(Get(g.ID));
        }
        [HttpPost]
        [Route("/admin/product/save")]
        public async Task<IActionResult> Save([FromBody] ProductEditModel model, IList<IFormFile> files)
        {
            // Refresh roles in the model if validation fails
            //var temp = UserEditModel.Create(_db);
            //model.Roles = temp.Roles;

            if (model.Product == null)
            {
                return BadRequest("The user could not be found.");
            }



            try
            {
                var productId = model.Product.ID;
                var isNew = productId == 0;

                if (string.IsNullOrWhiteSpace(model.Product.Name))
                {
                    return BadRequest("Name is mandatory.");
                }

                //if (string.IsNullOrWhiteSpace(model.SelectedProductCategory))
                //{
                //    return BadRequest("product category is mandatory.");
                //}

                var result = model.Save(_productRepository, _productCategoryRepository, _productImageRepository);
                if (result)
                {
                    var res = UOW.SaveChanges();
                    if (res > 0)
                    {
                        return Ok(Get(model.Product.ID));
                    }
                       
                }

                var errorMessages = new List<string>();
                return BadRequest("The user could not be saved." + "<br/><br/>" + string.Join("<br />", errorMessages));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/admin/product/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.Delete(id);
            var res = UOW.SaveChanges();
            if (res > 0)
            {
                var str = Ok(GetSuccessMessage("The product has been deleted."));
                return str;
            }
            else
            {
                return Ok(GetErrorMessage("could not be save change."));
            }
            
            return Ok(GetErrorMessage("The product could not be found."));
        }
        //public ProductEditModel Get(int id)
        //{
        //    var product = _productRepository.GetByID(id);
        //    var productcategories = _productCategoryRepository.GetAll().ToList();
        //    var productcategory = product.Name;
        //    var model = new ProductEditModel
        //    {
        //        Product = product,
        //        ProductCategorys = productcategories,
        //        SelectedProductCategory = productcategory
        //    };

        //    return model;
        //}
        
    }
}