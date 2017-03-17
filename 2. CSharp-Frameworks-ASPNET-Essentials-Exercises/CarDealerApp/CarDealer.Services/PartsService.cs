using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class PartsService :Service
    {

        public IEnumerable<AddPartSupplierVm> GetAddVm()
        {
            return this.Context.Suppliers.Select(supplier => new AddPartSupplierVm()
            {
                Id = supplier.Id,
                Name = supplier.Name
            });
        }

        public IEnumerable<AddPartSupplierVm> GetAddPartSuppliersVm()
        {
            IEnumerable<Supplier> suppliers = this.Context.Suppliers;

            Mapper.Initialize(cfg => cfg.CreateMap<Supplier, AddPartSupplierVm>());
            IEnumerable<AddPartSupplierVm> viewModels =
                 Mapper.Map<IEnumerable<Supplier>, IEnumerable<AddPartSupplierVm>>(suppliers);
            return viewModels;
        }


        public IEnumerable<AllPartVm> GetAllPartVms()
        {
            IEnumerable<Part> parts = this.Context.Parts;


            Mapper.Initialize(cfg => cfg.CreateMap<Part, AllPartVm>()
                .ForMember(vm => vm.SupplierName,
                    configurationExpression =>
                        configurationExpression.MapFrom(part => part.Supplier.Name)));

            IEnumerable<AllPartVm> viewModel = Mapper.Map<IEnumerable<Part>, IEnumerable<AllPartVm>>(parts);
            return viewModel;
        }

        public void AddPart(AddPartBm bind)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<AddPartBm, Part>()
            .ForMember(m => m.Supplier,
                    configurationExpression =>
                        configurationExpression.MapFrom(p => this.Context.Suppliers.Find(p.SupplierId))));

            Part part = Mapper.Map<AddPartBm, Part>(bind);
            this.Context.Parts.Add(part);
            this.Context.SaveChanges();
        }

        public DeletePartVm GetDeleteVm(int id)
        {
            Part part = this.Context.Parts.Find(id);
            DeletePartVm vm = new DeletePartVm()
            {
                Id = part.Id,
                Name = part.Name,
                Price = part.Price
            };
            return vm;
        }

        public void DeletePart(DeletePartBm bind)
        {
            Part part = this.Context.Parts.Find(bind.PartId);
            this.Context.Parts.Remove(part);
            this.Context.SaveChanges();
        }

        public EditPartVm GetEditVm(int id)
        {
            Part part = this.Context.Parts.Find(id);

            return new EditPartVm()
            {
                Id = part.Id,
                Price = part.Price,
                Quantity = part.Quantity
            };
        }

        //public EditPartVm EditPart(EditPartBm bind)
        //{
        //    return new EditPartVm()
        //    {
        //        Id = bind.Id,
        //        Price = bind.Price,
        //        Quantity = bind.Quantity
        //    };
        //}
    }
}