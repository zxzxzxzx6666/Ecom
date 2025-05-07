using Ardalis.Specification;
using ApplicationCore.Entities;

namespace ApplicationCore.Specifications 
{
    public class CatalogItemNameSpecification : Specification<CatalogItem>
    {
        public CatalogItemNameSpecification(string catalogItemName)
        {
            Query.Where(item => catalogItemName == item.Name);
        }
    }

}

