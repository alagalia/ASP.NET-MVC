using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.ViewModels;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealer.Services
{
    public class CustomersService
    {
        CarDealerContext Context = new CarDealerContext();

        public IEnumerable<AllCustomerVm> GetAllOrderedCustomers(string order)
        {
            if (order == null)
            {
                order = "ascending";
            }
            IEnumerable<Customer> customers;
            if (order == "ascending")
            {
                customers =
                    this.Context.Customers.OrderBy(customer => customer.BirthDate)
                        .ThenBy(customer => customer.IsYoungDriver);
            }
            else if (order == "descending")
            {
                customers =
                    this.Context.Customers.OrderByDescending(customer => customer.BirthDate)
                        .ThenBy(customer => customer.IsYoungDriver);
            }
            else
            {
                throw new ArgumentException("The argument you have given for the order is invalid!");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Customer, AllCustomerVm>());
            IEnumerable<AllCustomerVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Customer>, IEnumerable<AllCustomerVm>>(customers);

            return viewModels;
        }

        public AboutCustomerVm GetAboutCustomers(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            return new AboutCustomerVm()
            {
                Name = customer.Name,
                BoughtCarsCount = customer.Sales.Count,
                TotalSpentMoney = customer.Sales.Sum(sale => sale.Car.Parts.Sum(part => part.Price))
            };
        }

        public void AddCustomerBm(AddCustomerBm bind)
        {
            Customer customer = Mapper.Map<AddCustomerBm, Customer>(bind);
            if (DateTime.Now.Year - bind.BirthDate.Year < 21)
            {
                customer.IsYoungDriver = true;
            }

            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
        }

        public EditCustomerVm GetEditCustomerVm(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            EditCustomerVm customerVm = Mapper.Map<Customer, EditCustomerVm>(customer);
            return customerVm;
        }

        public void EditCustomer(EditCustomerBm bind)
        {
            Customer model = this.Context.Customers.Find(bind.Id);
            if (model == null)
            {
                throw new ArgumentException("Id is not valid!");
            }

            model.Name = bind.Name;
            model.BirthDate = bind.BirthDate;
            this.Context.SaveChanges();
        }
    }
}
