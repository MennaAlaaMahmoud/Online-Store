using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specificeations
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product,int> 
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyInclude();

        }

        public ProductWithBrandsAndTypesSpecifications() : base(null)
        {
            ApplyInclude();

        }

        private void ApplyInclude()
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }

    }
}
