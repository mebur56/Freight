using Domain.Entities;
using Domain.Interfaces;
using Infra.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapping
{
    internal class SeedFreightPrices
    {

        public void Initialize(BaseRepository<FreightPrice> context)
        {
            if (context.Select() != null)
            {
                using (StreamReader r = new StreamReader("./Assets/tabela-frete.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic freightPrices = JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);
                    if (freightPrices != null)
                    {
                        List<FreightPrice> freightPricesList = new List<FreightPrice>();
                        foreach (var table in freightPrices)
                        {
                            FreightPrice freightPrice = new FreightPrice();
                            freightPrice.TableName = table.Key;
                            freightPrice.Value = table.Value["value"];
                            freightPrice.VechicleType = table.Value["vehicle_type"];
                            freightPrice.Destination = table.Value["destination"];
                            freightPrice.Client = table.Value["client"];
                            freightPricesList.Add(freightPrice);
                            context.Insert(
                            new FreightPrice
                            {
                                Client = table.Value["client"],
                                Destination = table.Value["destination"]
                                ,
                                TableName = table.Key,
                                Value = table.Value["vehicle_type"]
                                ,
                                VechicleType = table.Value["vehicle_type"]
                            }

                            );
                        }
                    }
                }
            }
        }
    }
}
