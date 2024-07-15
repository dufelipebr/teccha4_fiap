using apibronco.bronco.com.br.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apibronco.bronco.com.br.Entity
{
    public class Entidade 
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdateOn { get; set; }

        public int Id_Status { get; set; }

        public string Id_Object_Type { get; set; }

     //   public string MongoDb_Entity_Name { get; set; }

        //public bool IsValid() 
        //{
        //    if (Id == null)
        //        return false;
        //    if (CreatedOn < new DateTime(2000, 1, 1))
        //        return false;
        //    if (LastUpdateOn < new DateTime(2000, 1, 1))
        //        return false;

        //    return true;
        //}
    }
}
