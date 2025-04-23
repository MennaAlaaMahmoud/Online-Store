using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specificeations
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParamters specParams) 
            : base(
                   p =>
                  (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search.ToLower())) &&
                  (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
                  (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId)


                  )
        {
            
        }

    }
}
