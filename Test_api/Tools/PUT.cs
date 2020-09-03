using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_api.Models;

namespace Test_api.Tools
{
    public static class PUT<TEntity> where TEntity : class
    {
        public static TEntity Up(TEntity newEntity, TEntity oldEntity)
        {
            if (newEntity is Car)
            {
                var newCar = newEntity as Car;
                var oldCar = oldEntity as Car;
                if (newCar.Id != null)
                {
                    if (newCar.Name != null)
                    {
                        oldCar.Name = newCar.Name;
                    }
                }

                if (newCar.Description != null)
                {
                    oldCar.Description = newCar.Description;
                }
                return oldCar as TEntity;
            }
            return null;
        }
    }
}
