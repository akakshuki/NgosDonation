using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models.ModelView
{
    public class CategoryDTO
    {
        public int ID { get; set; }

        [Display(Name = "Category Name"), Required(ErrorMessage = "This is required")]
        public string CateName { get; set; }

        public List<DonateDTO> Donates { get; set; }
    }
}