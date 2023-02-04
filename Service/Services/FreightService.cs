using Domain.Entities;
using Domain.Interfaces;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Validators;
using static Service.Services.Commons.Constants;

namespace Service.Services
{
    public class FreightService<TEntity> : IFreightService<TEntity> where TEntity : Freight
    {
        private readonly IBaseService<Freight> _baseService;
        private readonly IFreightPriceService<FreightPrice> _freightPriceService;

        public FreightService( IBaseService<Freight> baseService, IFreightPriceService<FreightPrice> freightPriceService)
        {
            _baseService = baseService;
            _freightPriceService = freightPriceService;
        }

        public IList<Freight> Search(string filterType, string filterText)
        {
            switch (filterType)
            {
                case FilterTypes.DRIVER:
                    return _baseService.Get().Where(x => x.Driver.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                case FilterTypes.DATE:
                    return  _baseService.Get().Where(x => x.Date.ToString().Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                case FilterTypes.DESTINATION:
                    return _baseService.Get().Where(x => x.Destination.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                default:
                    return _baseService.Get();
            }
        }

        public IList<Freight> SaveFreights(DataTable dt)
        {
            List<Archive> archives = handleDataTable(dt);

            IList<Freight> freights = GetFreights(archives);

            return _baseService.AddList<FreightValidator>(freights);
           
        }

        public List<Archive> handleDataTable (DataTable dt)
        {
            List<Archive> archives = new List<Archive>();

            // Read File

            foreach (var row in dt.Rows.Cast<DataRow>().Skip(1))
            {
                Archive archive = new Archive();
                if (row.ItemArray.All(x => x.ToString() != ""))
                {
                    archive.Origin = row.ItemArray[0].ToString();
                    archive.Dellivery = Int32.Parse(row.ItemArray[1].ToString());
                    archive.TravelNumber = Int32.Parse(row.ItemArray[2].ToString());
                    archive.Date = DateTime.Parse(row.ItemArray[3].ToString());
                    archive.Destination = row.ItemArray[4].ToString();
                    archive.Plate = row.ItemArray[5].ToString();
                    archive.Driver = row.ItemArray[6].ToString();
                    archive.VechicleType = row.ItemArray[7].ToString();
                    archive.Km = Int32.Parse(row.ItemArray[8].ToString());
                    archive.Boxes = Int32.Parse(row.ItemArray[9].ToString());
                    archive.TravelType = row.ItemArray[10].ToString();
                    archives.Add(archive);
                }
            };

            return archives;
        }
        private IList<Freight> GetFreights(List<Archive> archives)
        {
            IList<Freight> freights = new List<Freight>();
            foreach (var archive in archives)
            {

                var freightPrice = _freightPriceService.GetFreightPrice(archive);
                int price;
                Freight freight = new Freight()
                {
                    Date = archive.Date,
                    TravelNumber = archive.TravelNumber,
                    Driver = archive.Driver,
                    Plate = archive.Plate,
                    VechicleType = archive.VechicleType,
                    Origin = archive.Origin,
                    Destination = archive.Destination,
                    Boxes = archive.Boxes,
                    Dellivery = archive.Dellivery,
                    Km = archive.Km,
                    TravelType = archive.TravelType,
                    FreightTable = freightPrice?.TableName,
                    TravelValue = Int32.TryParse(freightPrice?.Value, out price) ? price : 0,

                };
                freights.Add(freight);


            }
            return freights;
        }

    }
}
