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
                  (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
                  (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId)


                  )
        {
            
        }

    }
}
