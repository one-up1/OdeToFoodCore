using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFoodCore.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
                if (Restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }
            }
            else
            {
                Restaurant = new Restaurant();
                Restaurant.Cuisine = CuisineType.Dutch;
            }
            
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id == 0)
            {
                restaurantData.Add(Restaurant);
            }
            else
            {
                restaurantData.Update(Restaurant);
            }
            restaurantData.Commit();
            TempData["Message"] = "Saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
