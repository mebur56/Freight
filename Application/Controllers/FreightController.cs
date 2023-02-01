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
using Service.Services;
using System.Reflection.Metadata.Ecma335;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private IBaseService<Freight> _baseFreightService;
        private IFreightService<Freight> _freightService;


        public FreightController(IBaseService<Freight> baseFreightService, IFreightService<Freight> freightService)
        {
            _baseFreightService = baseFreightService;
            _freightService = freightService;
        }
        [HttpPost]
        public async Task<IActionResult?> Create()
        {
            var files = HttpContext.Request.Form.Files;
            List<Archive> archives = new List<Archive>();
            
            // Read File
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using var fileStream = file.OpenReadStream();
                    var worksheet = new Worksheet();
                    using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                    {

                        DataSet ds = reader.AsDataSet();
                        DataTable dt = ds.Tables[0];
                        return  Execute(() => _freightService.SaveFreights(dt));
                    }
                }

            }
            return default;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Execute(() => _baseFreightService.Get());
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
