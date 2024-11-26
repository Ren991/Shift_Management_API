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
        public int id { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public bool pending { get; set; }

        [Required]
        public bool confirmed { get; set; }

        [Required]
        public bool isPayabled { get; set; }

        [Required]
        public User client { get; set; }

        [Required]
        public User barber { get; set; }

        [Required]

        public BarberShop barberShop { get; set; }

        [Required]

        public List<ServicesAndHaircuts> services { get; set; }

        [Required]

        public DateTime day { get; set; }
    }
}
