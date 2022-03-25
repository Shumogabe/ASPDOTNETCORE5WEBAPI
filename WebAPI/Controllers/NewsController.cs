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
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly IWebHostEnvironment _env;
        public NewsController(INewsRepository newsRepository, IWebHostEnvironment env)
        {
            _newsRepository = newsRepository;
            _env = env;
        }
        [HttpGet]
        public IActionResult GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            try
            {
                var result = _newsRepository.GetAll(search, PAGE_SIZE, page);
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
                var data = _newsRepository.GetById(id);
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
        public IActionResult Add(NewsModel newsModel)
        {
            try
            {
                return Ok(_newsRepository.Add(newsModel));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(Guid id, Models.News news)
        {
            if (id != news.Id)
            {
                return BadRequest();
            }
            try
            {
                _newsRepository.Update(news);
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
                _newsRepository.Delete(id);
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
                var physicalPath = _env.ContentRootPath + "/Images/" + filename;

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
