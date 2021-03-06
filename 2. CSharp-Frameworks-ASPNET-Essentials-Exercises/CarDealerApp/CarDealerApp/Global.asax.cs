﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Sales;

namespace CarDealerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            

            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Customer, AllCustomerVm>();
                expression.CreateMap<Car, CarVm>();
                expression.CreateMap<Supplier, SupplierVm>()
                    .ForMember(vm => vm.NumberOfPartsToSupply,
                        configurationExpression =>
                            configurationExpression.MapFrom(supplier => supplier.Parts.Count));
                expression.CreateMap<Part, PartVm>();
                expression.CreateMap<Sale, SaleVm>()
                .ForMember(vm => vm.Price,
                    configurationExpression =>
                    configurationExpression.MapFrom(sale =>
                            sale.Car.Parts.Sum(part => part.Price)));
                expression.CreateMap<AddCustomerBm, Customer>();
                //expression.CreateMap<EditCustomerBm, Customer>();
                expression.CreateMap<Customer, EditCustomerVm>();
                //expression.CreateMap<EditCustomerBm, EditCustomerVm>();
                //expression.CreateMap<AddPartBm, Part>();
                //expression.CreateMap<Part, AllPartVm>();
                //expression.CreateMap<Part, DeletePartVm>();
                //expression.CreateMap<Part, EditPartVm>();
                //expression.CreateMap<AddCarBm, Car>().ForMember(car => car.Parts, configurationExpression => configurationExpression.Ignore());


            });
        }
    }
}
