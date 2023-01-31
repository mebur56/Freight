using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

    }

    public abstract class InfoBase : BaseEntity
    {

        public DateTime Date { get; set; }

        public int TravelNumber { get; set; }

        public string Driver { get; set; }

        public string Plate { get; set; }

        public string VechicleType { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public int Boxes { get; set; }

        public string Dellivery { get; set; }

        public int Km { get; set; }

        public string TravelType { get; set; }
    }

    public class Frete : InfoBase
    {
        public string FreightTable { get; set; }

        public int TravelValue { get; set; }
    }

    public class Archive : InfoBase
    {
    }
}
