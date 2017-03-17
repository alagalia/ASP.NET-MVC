using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class SuppliersService : Service
    {
        public IEnumerable<SupplierVm> GetSuppliersFromDb(string type)
        {
            IEnumerable<Supplier> suppliersWanted;
            if (type == null)
            {
                suppliersWanted = this.Context.Suppliers;
            }
            else if (type.ToLower() == "local")
            {
                suppliersWanted = this.Context.Suppliers.Where(supplier => !supplier.IsImporter);
            }
            else if (type.ToLower() == "importers")
            {
                suppliersWanted = this.Context.Suppliers.Where(supplier => supplier.IsImporter);
            }
            else
            {
                throw new ArgumentException("Invalid type of suppliers!");
            }

            //Mapper.Initialize(cfg => cfg.CreateMap<Supplier, SupplierVm>());

            IEnumerable<SupplierVm> viewModels =
                Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierVm>>(suppliersWanted);

            return viewModels;
        }

      
    }
}