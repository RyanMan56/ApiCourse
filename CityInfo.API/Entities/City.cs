using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Automagically regarded as primary key. CityId would be accepted as well. But we could apply [Key] otherwise / as well. Same story with [DatabaseGenerated]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>(); // Good idea to initialise to avoid null references when trying to manipulate the collection
        // NumberOfPointsOfInterest omitted because we don't want to persist it in the database; it's a calculated field
    }
}
