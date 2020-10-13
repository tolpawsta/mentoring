using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MongoBDCore.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [BsonRequired]
        public string Name { get; set; }
        [BsonIgnoreIfNull(true)]
        public string Author { get; set; }
        [BsonIgnoreIfDefault(true)]
        [BsonDefaultValue(0)]
        public int Count { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<string> Genres { get; set; }
        [BsonIgnoreIfDefault(true)]
        [BsonDefaultValue(0)]
        public int Year { get; set; }
        public Book()
        {
            Genres = new HashSet<string>();
        }
    }
}
