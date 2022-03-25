using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories_NewsController : ControllerBase
    {
        private readonly ICategory_NewsRepository _category_NewsRepository;

        public Categories_NewsController(ICategory_NewsRepository category_NewsRepository)
        {
            _category_NewsRepository = category_NewsRepository;
        }
        [HttpGet]
        public IActionResult GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            try
            {
                return Ok(_category_NewsRepository.GetAll(search, PAGE_SIZE, page));
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
                var data = _category_NewsRepository.GetById(id);
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
        public IActionResult Add(Category_NewsModel category_NewsModel)
        {
            try
            {
                return Ok(_category_NewsRepository.Add(category_NewsModel));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(Guid id, Category_News category_News)
        {
            if (id != category_News.Id)
            {
                return BadRequest();
            }
            try
            {
                _category_NewsRepository.Update(category_News);
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
                _category_NewsRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
