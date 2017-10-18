using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; } // If we want to add another field to the db we need to run in the package manager console: Add-Migration CityInfoDbAddPOIDescription (with the name being something relevant)

        [ForeignKey("CityId")]  
        public City City { get; set; }  // By convention, a relationship will be created when there is a navigation property discovered on a type. Considered a navigation property if the type 
                                        // it points to can not be mapped as a scalar type by the current db provider. Id of City is foreign key. Not required to explicity define but we can
                                        // anyway. Like so. Can manually define foreign key by using [ForeignKey("CityId")]
        public int CityId { get; set; }

    }
}
