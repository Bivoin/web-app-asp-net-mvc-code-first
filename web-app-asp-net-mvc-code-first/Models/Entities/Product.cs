using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_app_asp_net_mvc_code_first.Models.Attributes;
using web_app_asp_net_mvc_code_first.Extensions;

namespace web_app_asp_net_mvc_code_first.Models.Entities
{
    public class Product
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>    
        [Required]
        [Display(Name = "Название", Order = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Количество продуктов в наличии
        /// </summary>  
        [Display(Name = "Количество продуктов в наличии", Order = 10)]
        public int NumberOfProducts { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>  
        [Display(Name = "Цена", Order = 20)]
        public decimal Price { get; set; }

        /// <summary>
        /// Дата следующего поступления
        /// </summary> 
        [Required]
        [Display(Name = "Дата следующего поступления", Order = 40)]
        public DateTime NextArrivalDate { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary> 
        [ScaffoldColumn(false)]
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Изображение продукта
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ProductImage ProductImage { get; set; }

        [Display(Name = "Изображение продукта", Order = 60)]
        [NotMapped]
        public HttpPostedFileBase ProductImageFile { get; set; }

        /// <summary>
        /// Филиалы магазина
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual ICollection<StoreBranch> StoreBranches { get; set; }


        [ScaffoldColumn(false)]
        public List<int> StoreBranchIds { get; set; }

        [Display(Name = "Филиалы магазина", Order = 70)]
        [UIHint("MultipleDropDownList")]
        [TargetProperty("StoreBranchIds")]
        [NotMapped]
        public IEnumerable<SelectListItem> StoreBranchDictionary
        {
            get
            {
                var db = new StoreContext();
                var query = db.StoreBranches;

                if (query != null)
                {
                    var Ids = query.Where(s => s.Products.Any(ss => ss.Id == Id)).Select(s => s.Id).ToList();
                    var dictionary = new List<SelectListItem>();
                    dictionary.AddRange(query.ToSelectList(c => c.Id, c => $"{c.StoreBranchName} {c.WorkingHours}", c => Ids.Contains(c.Id)));
                    return dictionary;
                }

                return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            }
        }

    }
}