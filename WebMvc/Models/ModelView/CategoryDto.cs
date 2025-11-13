using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a category.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Display(Name = "Category Name"), Required(ErrorMessage = "This is required")]
        public string CateName { get; set; }

        /// <summary>
        /// Gets or sets the list of donations in this category.
        /// </summary>
        public List<DonateDTO> Donates { get; set; }
    }
}