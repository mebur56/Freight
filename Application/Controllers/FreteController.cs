using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using Service.Validators;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using ClosedXML.Excel;
using System.Net.Http.Headers;
using ExcelDataReader;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Data;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreteController : ControllerBase
    {
        private IBaseService<Archive> _baseFreteService;

        public FreteController(IBaseService<Archive> baseFreteService)
        {
            _baseFreteService = baseFreteService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formData)
        {
            var files = HttpContext.Request.Form.Files;
            List<Archive> archives = new List<Archive>(); 
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using var fileStream = file.OpenReadStream();
                    // act on the Base64 data
                    var worksheet = new Worksheet();
                    using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                    {

                        DataSet ds = reader.AsDataSet();
                        DataTable dt = ds.Tables[0];
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
                    }
                }
        };
            return null;//Execute(() => _baseFreteService.Add<ArchiveValidator>(archive).Id);
        }


    private IActionResult Execute(Func<object> func)
    {
        try
        {
            var result = func();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
}
