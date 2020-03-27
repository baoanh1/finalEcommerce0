using Ecommerce.Data.EF;
using Ecommerce.ViewModels.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Catalog.Products
{
    public class ManageProductService : IManageProductSevice
    {
        protected EcommerceDbContext _context;
        public ManageProductService(EcommerceDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PageViewModel<ProductViewModel> GetAllPaging(string keywork, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
