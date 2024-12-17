using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Shift
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int? Price { get; set; }


        [Required]
        public bool? Confirmed { get; set; }

        [Required]
        public bool? IsPayabled { get; set; }

        [Required]
        public User? Client { get; set; }

        [Required]
        public User? Barber { get; set; }

        [Required]

        public BarberShop? BarberShop { get; set; }

        [Required]

        public List<ServicesAndHaircuts>? Services { get; set; }

        [Required]

        public DateTime? Day { get; set; }

        [Required]
        public string? ShiftTime { get; set; }
    }
}
