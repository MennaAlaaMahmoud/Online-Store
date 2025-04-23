using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specificeations
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyInclude();

        }

        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParamters specParams)
            : base(

                  p =>
                  (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search.ToLower()))&&
                  (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
                  (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId)

                  )
        {
            ApplyInclude();

            ApplySorting(specParams.Sort);
            ApplyPagination(specParams.PageIndex,specParams.PageSize);
        }


        private void ApplyInclude()
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

        }

        private void ApplySorting(string? Sort)
        {
            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }

    }

        
}
