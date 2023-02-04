using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Validators;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHost(args);
            var baseService = host.Services.GetRequiredService<IBaseService<FreightPrice>>();
            onApplicationStart(baseService);
            host.Run();
        }

        public static IWebHost CreateWebHost(string[] args) {

            return WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
             .UseDefaultServiceProvider(options =>
                 options.ValidateScopes = false)
             .Build();
        }

        private static void onApplicationStart(IBaseService<FreightPrice> baseService)
        {
            using (StreamReader r = new StreamReader("./Assets/tabela-frete.json"))
            {
                string json = r.ReadToEnd();
                dynamic freightTables = JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);
                if (freightTables != null)
                {
                    baseService.CleanFreightPriceTable();
                    List<FreightPrice> freightTablesList = new List<FreightPrice>();
                    foreach (var table in freightTables)
                    {
                        FreightPrice freightTable = new FreightPrice();
                        freightTable.TableName = table.Key;
                        freightTable.Value = table.Value["value"];
                        freightTable.VechicleType = table.Value["vehicle_type"];
                        freightTable.Destination = table.Value["destination"];
                        freightTable.Client = table.Value["client"];
                        freightTablesList.Add(freightTable);
                    }
                    baseService.AddList<FreightPriceValidator>(freightTablesList);

                }
            }
        }
    }
}