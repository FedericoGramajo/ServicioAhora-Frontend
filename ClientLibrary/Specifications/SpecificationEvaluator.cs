using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientLibrary.Specifications
{
    public class SpecificationEvaluator<T>
    {
        public static IEnumerable<T> GetQuery(IEnumerable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery.AsQueryable();

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            return query.AsEnumerable();
        }
    }
}
