using System;
using System.Linq.Expressions;
using ClientLibrary.Models.ServicioAhora.ServOffering;

namespace ClientLibrary.Specifications
{
    public class ServiceSearchSpecification : BaseSpecification<GetServiceOffering>
    {
        public ServiceSearchSpecification(string? searchTerm, Guid? categoryId, decimal? minPrice, decimal? maxPrice)
            : base(s => 
                (string.IsNullOrEmpty(searchTerm) || s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                (s.Description != null && s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))) &&
                (!categoryId.HasValue || categoryId == Guid.Empty || s.CategoryId == categoryId) &&
                (!minPrice.HasValue || s.BasePrice >= minPrice) &&
                (!maxPrice.HasValue || s.BasePrice <= maxPrice) &&
                s.Status // Only active services
            )
        {
            AddOrderBy(s => s.Name);
        }
    }
}
