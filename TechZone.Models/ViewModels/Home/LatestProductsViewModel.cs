﻿namespace TechZone.Models.ViewModels.Home
{
    public class LatestProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
