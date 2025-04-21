using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        // Generate Query
        public static IQueryable<TEntity> GetQuery<TEntity,TKey>(
            IQueryable<TEntity> inputQuery, 
            ISpecifications<TEntity, TKey> spec
            )
            where TEntity : BaseEntity<TKey>
        {
          
            var query = inputQuery;
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

          query = spec.IncludeExpressions.Aggregate(query,(currentQuery , includeExpressions) => currentQuery.Include(includeExpressions));
                

            return query;
        }

    }
}


// _context.Products.Include(p => p.productBrand).Include(p =>p.productType).ToListAsync() as IEnumerable<TEntity>
