﻿using ReactShop.Core.Data.Categories;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class ProductDTO
    {
        private readonly IGetCategory _getCategory;

        public ProductDTO()
        {
            _getCategory = AutoFacHelper.Resolve<IGetCategory>();
        }

        public int Id { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public decimal Price { get; set; }
        public ProductStatusEnum Status { get; set; }

        public string CategoryName => _getCategory.GetById(CategoryId).Description;

        public static explicit operator Product(ProductDTO dto)
        {
            return dto.ToProduct();
        }

        public Product ToProduct()
        {
            return new Product(Id, SKU, CategoryId, Description, SmallImagePath, LargeImagePath, Price, Status);
        }
    }
}