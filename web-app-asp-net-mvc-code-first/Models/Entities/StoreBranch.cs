using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using web_app_asp_net_mvc_code_first.Models.Attributes;
using web_app_asp_net_mvc_code_first.Extensions;
using web_app_asp_net_mvc_code_first.Models.Enums;

namespace web_app_asp_net_mvc_code_first.Models.Entities
{
    public class StoreBranch
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название филиала
        /// </summary>    
        [Required]
        [Display(Name = "Название филиала", Order = 5)]
        public string StoreBranchName { get; set; }

        /// <summary>
        /// Время работы
        /// </summary>    
        [Required]
        [Display(Name = "Время работы", Order = 5)]
        public string WorkingHours { get; set; }

        /// <summary>
        /// Регион
        /// </summary> 
        [ScaffoldColumn(false)]
        public Region Region { get; set; }

        [Display(Name = "Регион", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("Region")]
        [NotMapped]
        public IEnumerable<SelectListItem> RegionDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Region type in Enum.GetValues(typeof(Region)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Region
                    });
                }

                return dictionary;
            }
        }

        /// <summary>
        /// Город
        /// </summary> 
        [ScaffoldColumn(false)]
        public int CityId { get; set; }
        [ScaffoldColumn(false)]
        public virtual City City { get; set; }

        [Display(Name = "Город", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("CityId")]
        [NotMapped]
        public IEnumerable<SelectListItem> CityDictionary
        {
            get
            {
                var db = new StoreContext();
                var query = db.Cities;

                if (query != null)
                {
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == CityId));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }

        /// <summary>
        /// Продукты
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Города
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<City> Cities { get; set; }
    }
}