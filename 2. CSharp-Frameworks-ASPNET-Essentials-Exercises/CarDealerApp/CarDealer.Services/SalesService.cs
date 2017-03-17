using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class SalesService :Service
    {
        public IEnumerable<SaleVm> GetAllSales()
        {
            IEnumerable<Sale> sales =  this.Context.Sales;
            Mapper.Initialize(cfg => cfg.CreateMap<Sale, SaleVm>());
            IEnumerable<SaleVm> viewModels =
               Mapper.Map<IEnumerable<Sale>, IEnumerable<SaleVm>>(sales);
            return viewModels;
        }

        public SaleVm GetSaleById(int id)
        {
            Sale sale = this.Context.Sales.Find(id);
            SaleVm viewModel = Mapper.Map<Sale, SaleVm>(sale);
            return viewModel;
        }

        public IEnumerable<SaleVm> GetSaleByDiscount(double? percent)
        {
            IEnumerable<Sale> sales;
            if (percent == null)
            {
                sales = this.Context.Sales.Where(sale => sale.Discount > 0);
            }
            else
            {
                sales = this.Context.Sales.Where(sale => sale.Discount == (percent/100));
            }
            Mapper.Initialize(cfg => cfg.CreateMap<Sale, SaleVm>());

            IEnumerable<SaleVm> viewModels =
                Mapper.Map<IEnumerable<Sale>, IEnumerable<SaleVm>>(sales);
            return viewModels;
            
        }
    }
}