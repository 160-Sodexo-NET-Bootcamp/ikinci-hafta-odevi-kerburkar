using Data.DataModel;

namespace API.Model
{
    //Container sınıfındaki listeden gelen değerlerin başlangıç noktasına olan uzaklığını tutmak için kullanıldı. 
    //Container miras alındı.
    public class ContainerDistanceModel:Container
    {
        public double Distance { get; set; }
    }
}
