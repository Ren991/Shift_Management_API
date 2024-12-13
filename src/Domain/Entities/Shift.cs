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
        public User User { get; set; }

        [Required]
        public double? Price { get; set; }


        [Required]
        public bool? Confirmed { get; set; }

        [Required]
        public bool? IsPayabled { get; set; }



        [Required]
        public int? ClientID { get; set; } = null;

        [Required]
        public int? BarberID { get; set; }

        [Required]

        public BarberShop? BarberShop { get; set; }

        [Required]

        public int? BarberShopID { get; set; }

        [Required]

        public ICollection<ServicesAndHaircuts>? Services { get; set; } = new List<ServicesAndHaircuts>();

        [Required]

        public DateTime? Day { get; set; }

        [Required]
        public string? ShiftTime { get; set; }
    }
}
