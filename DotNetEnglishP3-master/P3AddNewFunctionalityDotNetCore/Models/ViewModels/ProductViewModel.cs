using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        public object ViewData;

        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "MissingName", ErrorMessageResourceType = typeof(Resources.ProductService))]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessageResourceName = "MissingStock", ErrorMessageResourceType = typeof(Resources.ProductService))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "StockNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.ProductService))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "StockNotAnInteger", ErrorMessageResourceType = typeof(Resources.ProductService))]
        public string Stock { get; set; }


        [Required(ErrorMessageResourceName = "MissingPrice", ErrorMessageResourceType = typeof(Resources.ProductService))]
        [Range(double.Epsilon, double.MaxValue, ErrorMessageResourceName = "PriceNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.ProductService))]
        [RegularExpression(@"^(?:\d+(?:\.\d+)?|\.\d+)(?:,(?:\d+(?:\.\d+)?|\.\d+))*$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = typeof(Resources.ProductService))]
        public string Price { get; set; }
    }
}

