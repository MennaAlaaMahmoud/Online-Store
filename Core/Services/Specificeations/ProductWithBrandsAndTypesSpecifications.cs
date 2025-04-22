using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specificeations
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyInclude();

        }

        public ProductWithBrandsAndTypesSpecifications(int? brandId, int? typeId, string? sort, int pageIndex , int pageSize )
            : base(

                  p =>
                  (!brandId.HasValue || p.BrandId == brandId) &&
                  (!typeId.HasValue || p.TypeId == typeId)

                  )
        {
            ApplyInclude();

            ApplySorting(sort);
            ApplyPagination(pageIndex, pageSize);
        }


        private void ApplyInclude()
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

        }

        private void ApplySorting(string? sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
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
