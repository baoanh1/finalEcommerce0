using Ecommerce.ViewModels.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public PageViewModel<ProductViewModel> GetAllByCategoryId(int productcategoryId, int pageIndex, int pageSize);
    }
}
