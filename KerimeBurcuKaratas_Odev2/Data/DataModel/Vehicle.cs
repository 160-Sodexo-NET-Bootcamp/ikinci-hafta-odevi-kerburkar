using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModel
{
    //Vehicle sınıfı oluşturuldu. 
    public class Vehicle
    {
        //Yeni bir satır eklendiğinde bu alan Identity özelliği alır. (PK)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //VehicleName maksimum karakter sayısı ayarlandı.
        [MaxLength(50)]
        public string VehicleName { get; set; }

        //VehiclePlate maksimum karakter sayısı ayarlandı.
        [MaxLength(14)]
        public string VehiclePlate { get; set; }

        //ForeignKey bağlantısı eklendi.
        //Bir vehicle birden fazla container ile bağlı. Bire çok ilişki.
        public virtual List<Container> Containers { get; set; }

    }
}
