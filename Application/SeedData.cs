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

namespace Application
{
    internal class SeedFreightPrices
    {

        public void Initialize(IBaseRepository<FreightPrice> context)
        {
            var currentDBList = context.Select();
            if (currentDBList == null || currentDBList.Count == 0)
            {
                using (StreamReader r = new StreamReader("./Assets/tabela-frete.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic freightPrices = JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);
                    if (freightPrices != null)
                    {
                        foreach (var table in freightPrices)
                        {
                            FreightPrice freightPrice = new FreightPrice();
                            freightPrice.TableName = table.Key;
                            freightPrice.Value = table.Value["value"];
                            freightPrice.VechicleType = table.Value["vehicle_type"];
                            freightPrice.Destination = table.Value["destination"];
                            freightPrice.Client = table.Value["client"];
                            context.Insert(
                            new FreightPrice
                            {
                                Client = table.Value["client"],
                                Destination = table.Value["destination"]
                                ,
                                TableName = table.Key,
                                Value = table.Value["value"]
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
