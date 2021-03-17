using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Eureka", Location = "Epe", Cuisine = CuisineType.Dutch },
                new Restaurant { Id = 2, Name = "'t Tonnetje", Location = "Epe", Cuisine = CuisineType.Dutch },
                new Restaurant { Id = 1, Name = "Domino's Pizza", Location = "Epe", Cuisine = CuisineType.Italian }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
