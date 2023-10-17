using ECommerceBackEnd.Dtos.Category;
using ECommerceBackEnd.Dtos.Product;
using ECommerceBackEnd.Entities;
using System.Runtime.CompilerServices;

namespace ECommerceBackEnd
{
    public static class Extension
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                DepId = product.DepartmentId,
                DName = product.DepartmentName,
                PID = product.ProductCardId,
                PName = product.ProductName,
                Price = product.ProductPrice,
                CID = product.ProductCategoryId,
                OID = product.OrderItemId,
                OPID = product.OrderItemCardprodId,
                Ratio = product.OrderItemProfitRatio,
                Gain = product.Sales,
                Available = product.ProductStatus
            };
        }
        public static CategoryDto AsDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                CName = category.CategoryName,
                CID = category.CategoryId
            };
        }
    }
}
