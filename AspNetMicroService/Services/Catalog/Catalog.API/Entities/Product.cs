using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string ProductName { get; set; }

        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
