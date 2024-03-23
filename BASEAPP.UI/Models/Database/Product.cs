using System;

namespace BASEAPP.UI.Models.Database
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Description { get; set; }

        public double Price { get; set; }

        public int StockQuantity { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double AverageRating { get; set; }

        public string ImagePath { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }
    }
}
