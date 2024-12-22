using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ServicesAndHaircuts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string? Category { get; set; }
        
        [Required]      
        public bool IsActive { get; set; }

        //[Required]
        public int? ShiftId { get; set; }

        //[Required]
        //[JsonIgnore]
        public Shift? Shift { get; set; }
    }
}
