using Ecommerce.Application.Services;
using Ecommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.Areas.Admin.ProductModel
{
    public class ProductEditModel
    {

        public Product Product { get; set; }
        public IList<ProductCategory> ProductCategorys { get; set; } = new List<ProductCategory>();
        public IList<ProductImage> ProductImages {get;set;} = new List<ProductImage>();
        public List<string> ProductImagePath { get; set; } = new List<string>();
        public string SelectedProductCategory { get; set; }
        public static ProductEditModel Create(IRepository<Product> ProductRepository, IRepository<ProductCategory> ProductCategoryRepository, IRepository<ProductImage> ProductImageRepository)
        {
            return new ProductEditModel
            {
                Product = new Product(),
                ProductCategorys = ProductCategoryRepository.GetAll().OrderBy(r => r.Name).ToList(),
                
            };
        }

        public static ProductEditModel GetById(IRepository<Product> ProductRepository, IRepository<ProductCategory> ProductCategoryRepository, IRepository<ProductImage> ProductImageRepository, int id)
        {
            var product = ProductRepository.GetByID(id);
            var getproductImages = ProductImageRepository.GetAll().Where(x=>x.ProductID == product.ID).ToList();
            var getProductImagePath = ProductImageRepository.GetAll().Where(x => x.ProductID == product.ID).Select(x=>x.ImagePath).ToList();
            if (product != null)
            {
                var model = new ProductEditModel
                {
                    Product = product,
                    ProductCategorys = ProductCategoryRepository.GetAll().OrderBy(r => r.ID).ToList(),
                    ProductImages = getproductImages,
                    ProductImagePath = getProductImagePath
                };
                
                var Productcate = ProductCategoryRepository.GetByID(product.categoryID).Name;
                model.SelectedProductCategory = Productcate;
                return model;
            }

            return null;
        }

        public bool Save(IRepository<Product> ProductRepository, IRepository<ProductCategory> ProductCategoryRepository, IRepository<ProductImage> ProductImageRepository)
        {
            bool status = false;
            var product = ProductRepository.GetByID(Product.ID);

            if (product == null)
            {
                //foreach(var image in product.ProductImages)
                //{
                //    ProductImageRepository.Add(image);
                //}
                
                ProductRepository.Add(Product);

                status = true;

            }
            else
            {
                product = Product;
                ProductRepository.Update(product);

                status = true;
            }

            return status;
        }
    }
}
