using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories_DocumentsController : ControllerBase
    {
        private readonly ICategory_DocumentsRepository _category_DocumentsRepository;

        public Categories_DocumentsController(ICategory_DocumentsRepository category_DocumentsRepository)
        {
            _category_DocumentsRepository = category_DocumentsRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_category_DocumentsRepository.GetAll());
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
                var data = _category_DocumentsRepository.GetById(id);
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
        public IActionResult Add(Category_DocumentsModel category_DocumentsModel)
        {
            try
            {
                return Ok(_category_DocumentsRepository.Add(category_DocumentsModel));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Category_Documents category_Documents)
        {
            if (id != category_Documents.Id)
            {
                return BadRequest();
            }
            try
            {
                _category_DocumentsRepository.Update(category_Documents);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _category_DocumentsRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
