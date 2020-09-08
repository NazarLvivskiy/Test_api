using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_api.Models
{
    public class CarPrototype
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CarPrototype()
        {
            Description = string.Empty;
            Name = string.Empty;
        }
    }
}
