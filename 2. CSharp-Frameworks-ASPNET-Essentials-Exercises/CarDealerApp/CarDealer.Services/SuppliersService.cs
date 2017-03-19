using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Supplier;

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

            Mapper.Initialize(cfg => cfg.CreateMap<Supplier, SupplierVm>());

            IEnumerable<SupplierVm> viewModels =
                Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierVm>>(suppliersWanted);

            return viewModels;
        }


        public AddSupplierVm GetAddSupplierVm(AddSupliersBm bind)
        {
            return new AddSupplierVm()
            {
                Name = bind.Name,
                IsImporter = bind.IsImporter
            };
        }

        public void AddSupplier(AddSupliersBm bind)
        {
            this.Context.Suppliers.Add(new Supplier()
            {
                Name = bind.Name,
                IsImporter = bind.IsImporter
            });
            this.Context.SaveChanges();
        }

        public DeleteSupplierVm GetDeleteSupplierVm(int id)
        {
            Supplier supplier = Context.Suppliers.Find(id);
            return new DeleteSupplierVm()
            {
                Id = id,
                Name = supplier.Name,
                IsImporter = supplier.IsImporter,
                HasParts = supplier.Parts.Any()
            };
        }

        public void DeleteSupplier(DeleteSupplierBm bind, int userId)
        {
            Supplier supplier = this.Context.Suppliers.Find(bind.Id);
            this.Context.Suppliers.Remove(supplier);
            this.Context.SaveChanges();

            //this.AddLog(userId, OperationLog.Delete, "suppliers");
        }

        public EditSupplierVm GetEditSupplierVm(int id)
        {
            Supplier supplier = Context.Suppliers.Find(id);
            return new EditSupplierVm()
            {
                Id = id,
                Name = supplier.Name,
                IsImporter = supplier.IsImporter,
                HasParts = supplier.Parts.Any()
            };
        }

        public void EditSupplier(EditSupplierBm bind, int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(bind.Id);
            supplier.IsImporter = bind.IsImporter;
            supplier.Name = bind.Name;
            this.Context.SaveChanges();

            //this.AddLog(userId, OperationLog.Edit, "suppliers");
        }
    }
}