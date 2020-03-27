using Ecommerce.ViewModels.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Catalog.Products
{
    public interface IManageProductSevice
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int ProductId);
        Task<ProductViewModel> GetAll();
        PageViewModel<ProductViewModel> GetAllPaging(string keywork, int pageIndex, int pageSize);
    }
}
