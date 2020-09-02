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
            return cars.GetAll_async().Result.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await cars.Get_async(id);
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

            await cars.Add_async(car);
            return Ok(car);
        }

        [HttpPut]
        public async Task<ActionResult<Car>> Put(Car user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (user.Id == null)
            {
                await Post(user);
                return Ok(user);
            }
            

            await cars.Update_async(user, user.Id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(string id)
        {
            await cars.Delete_async(id);
            return Ok();
        }
    }
}
