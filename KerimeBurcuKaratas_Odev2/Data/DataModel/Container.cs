using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModel
{
    //Container sınıfı oluşturuldu.
    public class Container
    {
        //Yeni bir satır eklendiğinde bu alan Identity özelliği alır. (PK)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        //VehicleName maksimum karakter sayısı ayarlandı.
        [MaxLength(50)]
        public string ContainerName { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
        
        public long VehicleId { get; set; }

        //ForeignKey bağlantısı eklendi.
        public virtual Vehicle Vehicle { get; set; }


    }
}
