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
        public async Task<ActionResult<Car>> Post(Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            if (car.Id == null)
            {
                await cars.Add(car);
                return Ok("Add");
            }
            else
            {
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
