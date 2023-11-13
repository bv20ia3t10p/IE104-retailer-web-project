﻿using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Dtos
{
    public record CreateProductDto
    {
        public int DepartmentId { get; init; }
        public string DepartmentName { get; init; }
        public int ProductCategoryId { get; init; }
        public string ProductName { get; init; }
        public double ProductPrice { get; init; }
        public int OrderItemId { get; init; }
        public double OrderItemProfitRatio { get; init; }
        public double Sales { get; init; }
        public bool ProductStatus { get; init; }
    }
    public record ProductDto
    {
        public int DepartmentId { get; init; }
        public string DepartmentName { get; init; }
        public int ProductCardId { get; init; }
        public int ProductCategoryId { get; init; }
        public string ProductName { get; init; }
        public double ProductPrice { get; init; }
        public int OrderItemCardprodId { get; init; }
        public int OrderItemId { get; init; }
        public double OrderItemProfitRatio { get; init; }
        public double Sales { get; init; }
        public bool ProductStatus { get; init; }
    }
    public record UpdateProductDto
    {
        public int DepartmentId { get; init; }
        public string DepartmentName { get; init; }
        public int ProductCardId { get; init; }
        public int ProductCategoryId { get; init; }
        public string ProductName { get; init; }
        public double ProductPrice { get; init; }
        public int OrderItemCardprodId { get; init; }
        public int OrderItemId { get; init; }
        public double OrderItemProfitRatio { get; init; }
        public double Sales { get; init; }
        public bool ProductStatus { get; init; }
    }
}
