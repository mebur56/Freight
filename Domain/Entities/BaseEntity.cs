using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int? Id { get; set; }

    }

    public abstract class InfoBase : BaseEntity
    {

        public DateTime? Date { get; set; }

        [Required]
        public int? TravelNumber { get; set; }

        public string? Driver { get; set; }

        public string? Plate { get; set; }

        public string? VechicleType { get; set; }

        public string? Origin { get; set; }

        public string? Destination { get; set; }

        public int? Boxes { get; set; }

        public int? Dellivery { get; set; }

        public int? Km { get; set; }

        public string? TravelType { get; set; }
    }

    public class Freight : InfoBase
    {
        public string? FreightTable { get; set; }

        public int? TravelValue { get; set; }
    }

    public class Archive : InfoBase
    {
    }
    public class FreightPrice : BaseEntity
    {
        public string? TableName { get; set;}
        public string? Value { get; set;}
        public string? VechicleType { get; set;}
        public string? Destination { get; set;}
        public string? Client { get; set;}
    }
    
}
