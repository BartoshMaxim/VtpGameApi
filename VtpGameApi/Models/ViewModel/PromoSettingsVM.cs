using System.ComponentModel.DataAnnotations;
using VtpGameApi.Helpers;

namespace VtpGameApi.Models.ViewModel
{
    public class PromoSettingsVM
    {
        public PromoList PromoList { get; set; }

        public SmallPromo SmallPromo { get; set; }

        public BigPromo BigPromo { get; set; }

        public PromoSettingsVM() { }

        public PromoSettingsVM(PromoList promoList, SmallPromo smallPromo, BigPromo bigPromo)
        {
            PromoList = promoList;
            SmallPromo = smallPromo;
            BigPromo = bigPromo;
        }

        public void SetDefault()
        {
            PromoList = DataHelper.GetPromo<PromoList>();
            SmallPromo = DataHelper.GetPromo<SmallPromo>();
            BigPromo = DataHelper.GetPromo<BigPromo>();
        }

        public void Save()
        {
            PromoList.SavePromo();
            SmallPromo.SavePromo();
            BigPromo.SavePromo();
        }
    }

    public abstract class PromoBase
    {
        [Display(Name = "Promo Period(minutes)")]
        public int PromoPeriod { get; set; }
    }

    public class PromoList : PromoBase
    {
        public ArticleType ArticleType { get; set; }

        [Display(Name = "Articles Count")]
        public int ArticlesCount { get; set; }
    }

    public class SmallPromo : PromoBase
    {
        public ArticleType ArticleType { get; set; }
    }

    public class BigPromo : PromoBase
    {

    }
}