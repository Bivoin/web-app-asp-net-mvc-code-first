using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace web_app_asp_net_mvc_code_first.Models.Entities
{
    public class City
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название города
        /// </summary>    
        [Required]
        [Display(Name = "Название", Order = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Список филиалов
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<StoreBranch> StoreBranches { get; set; }
    }
}