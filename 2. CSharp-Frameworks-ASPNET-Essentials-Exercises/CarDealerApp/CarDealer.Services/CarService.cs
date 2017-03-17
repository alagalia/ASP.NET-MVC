using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
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
    }
}