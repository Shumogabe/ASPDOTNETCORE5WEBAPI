using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _env;


        public DocumentsController(IDocumentRepository documentRepository, IWebHostEnvironment env)
        {
            _documentRepository = documentRepository;
            _env = env;
        }
        [HttpGet]
        public IActionResult GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            try
            {
                var result = _documentRepository.GetAll(search, PAGE_SIZE, page);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _documentRepository.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(DocumentModel documentModel)
        {
            try
            {
                return Ok(_documentRepository.Add(documentModel));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(Guid id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }
            try
            {
                _documentRepository.Update(document);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _documentRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("SaveFile")]
        [Authorize]
        public IActionResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Files/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

                //return new JsonResult("03092020- BT va Dai su Italia.png");
            }
        }
    }
}
