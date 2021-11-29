using System.ComponentModel.DataAnnotations;

namespace web_app_asp_net_mvc_code_first.Models.Enums
{
    public enum Region
    {
        [Display(Name = "Якутия")]
        Yakutia = 1,

        [Display(Name = "Приморский край")]
        PrimorskyTerritory = 2,

        [Display(Name = "Хабаровский край")]
        KhabarovskTerritory = 3,

        [Display(Name = "Амурская область")]
        AmurRegion = 4,

        [Display(Name = "Камчатская область")]
        KamchatkaRegion = 5,

        [Display(Name = "Корякский автономный округ")]
        OfWhichKoryakAutonomousArea = 6,

        [Display(Name = "Магаданская область")]
        MagadanRegion = 7,

        [Display(Name = "Сахалинская область")]
        SakhalinRegion = 8,

        [Display(Name = "Еврейская автономная область")]
        JewishAutonomousRegion = 9,

        [Display(Name = "Чукотский автономный округ")]
        ChukotkaAutonomousArea = 10,
    }
}
