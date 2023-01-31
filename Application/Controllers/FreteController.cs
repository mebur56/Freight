using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using Service.Validators;
using System;
using Microsoft.AspNetCore.Identity;

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
        public  async Task<IActionResult> Create(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            Archive archive = new Archive();

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
