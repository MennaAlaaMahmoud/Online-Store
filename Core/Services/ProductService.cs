using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specificeations;
using ServicesAbstractions;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
       // private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandId, int? typeId)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(brandId , typeId);
            
            // Get All Products Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);



            // Mapping to IEnumerable<Product> To <IEnumerable<ProductResultDto>> : Automapper
           var result =  mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;


        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);

            var product = await  unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if(product is null)  return null;

           var result =  mapper.Map<ProductResultDto>(product);
            return result;

        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;

        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
           var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
           var result =  mapper.Map<IEnumerable<BrandResultDto>>(brands);

            return result ;


        }


    }
}
