using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace MongoModels
{
    public class MongoEntityBase
    {
        public ObjectId _id { get; set; }

    }
}
