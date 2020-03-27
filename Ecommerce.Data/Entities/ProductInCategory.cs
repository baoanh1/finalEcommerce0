using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Data.Entities
{
    public class ProductInCategory
    {
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
