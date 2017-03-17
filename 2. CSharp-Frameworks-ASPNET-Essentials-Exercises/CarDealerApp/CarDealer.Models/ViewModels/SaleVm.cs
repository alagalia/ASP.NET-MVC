using System.Linq;
using CarDealer.Models.EntityModels;

namespace CarDealer.Models.ViewModels
{
    public class SaleVm
    {
        private double? price;
        private double? priceWithDiscount;

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public double Discount { get; set; }


        public double? Price
        {
            get { return this.price = Car.Parts.Sum(part => part.Price); }
        }

        public double? PriceWithDiscount
        {
            get
            {
                double? discountAsMoney = this.price * this.Discount;
                return this.price - discountAsMoney; }
        }
    }
}