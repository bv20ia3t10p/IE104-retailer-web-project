using ECommerceBackEnd.Dtos.Category;
using ECommerceBackEnd.Dtos.Customer;
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
        public static CustomerDTO AsDto (this  Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                City = customer.CustomerCity,
                Country = customer.CustomerCountry,
                Segment = customer.CustomerSegment,
                Street = customer.CustomerStreet,
                State = customer.CustomerState,
                Zip = customer.CustomerZipcode,
                CusID = customer.CustomerId,
                PW = customer.CustomerPassword,
                Email = customer.CustomerEmail,
                Fname = customer.CustomerFname,
                Lname = customer.CustomerLname
            };
        }
    }
}
