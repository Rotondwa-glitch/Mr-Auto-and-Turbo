using Microsoft.AspNetCore.Mvc.Rendering;

namespace MisterAutoAndTurbo.Web.Data
{
    /// <summary>
    /// Common South African vehicle makes for dropdowns.
    /// </summary>
    public static class CarMakes
    {
        public static readonly string[] Makes = new[]
        {
            "Toyota", "Volkswagen", "Ford", "Hyundai", "Nissan",
            "Kia", "BMW", "Mercedes-Benz", "Audi", "Isuzu",
            "Mazda", "Suzuki", "Honda", "Renault", "Chevrolet",
            "Opel", "Peugeot", "Mitsubishi", "Land Rover", "Volvo",
            "Other"
        };

        public static List<SelectListItem> ToSelectList(string? selected = null)
        {
            return Makes.Select(m => new SelectListItem
            {
                Text = m,
                Value = m,
                Selected = m == selected
            }).ToList();
        }
    }
}
