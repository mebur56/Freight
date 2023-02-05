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
using System.Net;

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
        public IActionResult Create()
        {
            var files = HttpContext.Request.Form.Files;
            List<Archive> archives = new List<Archive>();
            if (files.Count != 1)
            {
                return BadRequest();
            }
            // Read File
            var file = files[0];

            if (file.Length > 0)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using var fileStream = file.OpenReadStream();
                var worksheet = new Worksheet();
                using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                {

                    DataSet ds = reader.AsDataSet();
                    DataTable dt = ds.Tables[0];
                    _freightService.SaveFreights(dt);
                    return StatusCode(201);
                }
            }


            return BadRequest();

        }

        [HttpGet]
        public IActionResult GetList()
        {

            try
            {
                string filterType = HttpContext.Request.Query["filterType"].ToString();
                string filterText = HttpContext.Request.Query["filterText"].ToString();

                var freightList = _freightService.Search(filterType, filterText);

                if (freightList == null || freightList.Count == 0)
                {
                    return NoContent();
                }
                return Ok(freightList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
