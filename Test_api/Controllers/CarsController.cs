using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_api.Models;
using Test_api.Models.Repository;

namespace Test_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        IRepository<Car> cars;

        public CarsController(IRepository<Car> repository)
        {
            cars = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return cars.GetAll().Result.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await cars.Get(id);
            if (car == null)
                return NotFound();
            return new ObjectResult(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> Post(CarPrototype prototype)
        {
            if (prototype == null)
            {
                return BadRequest();
            }
            if (prototype.Id == null)
            {
                var car = new Car();

                if (prototype.Name == string.Empty || prototype.Name == null)
                {
                    return BadRequest();
                }
                car.Name = prototype.Name;
                if (prototype.Description == string.Empty)
                {
                    car.Description = null;
                }
                else
                {
                    car.Description = prototype.Description;
                }


                await cars.Add(car);
                return Ok("Add");
            }
            else
            {
                var car = await cars.Get(prototype.Id);

                if (prototype.Name==null)
                {
                    return BadRequest();
                }
                if (prototype.Name!=string.Empty)
                {
                    car.Name = prototype.Name;
                }

                if (prototype.Description != string.Empty)
                {
                    car.Description = prototype.Description;
                }
                

                await cars.Update(car, car.Id);

                return Ok("PUT");
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(string id)
        {
            await cars.Delete(id);
            return Ok("Delete");
        }
    }
}
