using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class CarService
    {
        CarDealerContext Context = new CarDealerContext();

        public IEnumerable<CarVm> GetAllCars(string make)
        {
            IEnumerable<Car> cars;
            if (make == null)
            {
                cars = this.Context.Cars
                .OrderBy(car => car.Model).ThenByDescending(car => car.TravelledDistance);
            }
            else
            {
                cars = this.Context.Cars.Where(car => car.Make.Contains(make))
                .OrderBy(car => car.Model).ThenByDescending(car => car.TravelledDistance);
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Car, CarVm>());

            IEnumerable<CarVm> viewModels =
                Mapper.Instance.Map<IEnumerable<Car>, IEnumerable<CarVm>>(cars);

            return viewModels;
        }
        

        public CarPartsVm GetACarWithParts(int id)
        {
            Car wantedCar = this.Context.Cars.Find(id);
            IEnumerable<Part> carParts = wantedCar.Parts;
            
            IEnumerable<PartVm> carPartsVms = Mapper.Map<IEnumerable<Part>, IEnumerable<PartVm>>(carParts);
            CarVm wantedCarVm = Mapper.Map<Car, CarVm>(wantedCar);

            return new CarPartsVm()
            {
                Car = wantedCarVm,
                Parts = carPartsVms
            };
        }

        public void AddCar(AddCarBm bind, int userId)
        {
            this.Context.Cars.Add(new Car()
            {
                Make = bind.Make,
                Model = bind.Model,
                TravelledDistance = bind.TravelledDistance
            });
            this.Context.SaveChanges();

            this.AddLog(userId, OperationLog.Add, "cars");
        }

        public AddCarVm GetAddCarVm(AddCarBm bind)
        {
            return new AddCarVm()
            {
                Make = bind.Make,
                Model = bind.Model,
                TravelledDistance = bind.TravelledDistance
            };
        }

        public AddCarWithPartsVm GetAddCarWithPartsVm(AddCarBm bind)
        {
            IEnumerable<AllPartVm> partsVm = this.Context.Parts.Take(10).Select(p => new AllPartVm()
            {
                Id = p.Id,
                Price = p.Price,
                Quantity = p.Quantity,
                Name = p.Name,
                SupplierName = p.Supplier.Name
            });
            return  new AddCarWithPartsVm()
            {
                Parts = partsVm,
                Make = bind.Make,
                Model = bind.Model
            };
        }

        private void AddLog(int userId, OperationLog operation, string modifiedTable)
        {
            User loggedUser = this.Context.Users.Find(userId);
            Log log = new Log()
            {
                User = loggedUser,
                ModifiedAt = DateTime.Now,
                ModifiedTableName = modifiedTable,
                Operation = operation
            };

            this.Context.Logs.Add(log);
            this.Context.SaveChanges();
        }
    }
}