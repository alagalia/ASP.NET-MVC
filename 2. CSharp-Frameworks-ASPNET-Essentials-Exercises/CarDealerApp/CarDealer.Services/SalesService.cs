using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models.BindingModels.Sales;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Sales;

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

        public AddSaleVm GetSalesVm()
        {
            AddSaleVm vm = new AddSaleVm();
            IEnumerable<AddSaleCarVm> carVms= this.Context.Cars.Select(car =>
            new AddSaleCarVm
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make
            });
            IEnumerable<AddSaleCustomerVm> customerVms = this.Context.Customers.Select(customer => new AddSaleCustomerVm
            {
                Id = customer.Id,
                Name = customer.Name
            });

           

            List<int> discounts = new List<int>();
            for (int i = 0; i <= 50; i += 5)
            {
                discounts.Add(i);
            }

            vm.Cars = carVms;
            vm.Customers = customerVms;
            vm.Discounts = discounts;
            return vm;
        }

        public void AddSale(AddSalesBm bind)
        {
            Car car = this.Context.Cars.Find(bind.CarId);
            Customer customer = this.Context.Customers.Find(bind.CustomerId);
            this.Context.Sales.Add(new Sale()
            {
                Car = car,
                Customer = customer,
                Discount = (double) bind.Discount
            });
            this.Context.SaveChanges();
        }

        public AddSaleConfirmationVm GetAddSaleConfirmationVm(AddSalesBm bind)
        {
            Car car = this.Context.Cars.Find(bind.CarId);
            Customer customer = this.Context.Customers.Find(bind.CustomerId);
            AddSaleConfirmationVm vm = new AddSaleConfirmationVm()
            {
                CarId = bind.CarId,
                CustomerId = bind.CustomerId,
                CustomerName = customer.Name,
                CarRepresentation = car.Make + " " +car.Model,
                Discount = bind.Discount,
                CarPrice = (decimal)car.Parts.Sum(part => part.Price).Value
            };

            vm.Discount += customer.IsYoungDriver ? 5 : 0;
            vm.FinalCarPrice = vm.CarPrice*vm.Discount/100;
            return vm;
        }
    }
}