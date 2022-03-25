using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Models;
using WebAPI.Services;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        private readonly AppSettings _appSettings;

        public AccountsController(IAccountRepository accountRepository, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _accountRepository = accountRepository;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll(string search, int PAGE_SIZE = 3, int page = 1)
        {
            try
            {
                return Ok(_accountRepository.GetAll(search, PAGE_SIZE, page));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _accountRepository.GetById(id);
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
        public IActionResult Add(AccountModel accountModel)
        {
            try
            {
                return Ok(_accountRepository.Add(accountModel));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(Guid id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            try
            {
                _accountRepository.Update(account);
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
                _accountRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _accountRepository.Login(model.Username, model.Password);
            if (user == null) //không đúng
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }

            //cấp token

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            });
        }
        private string GenerateToken(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim("Phone", account.Phone),
                    new Claim("Level", account.Level.ToString()),
                    new Claim("Id", account.Id.ToString()),

                    //roles

                    new Claim("TokenId", account.Id.ToString())
                }),
                // Thời gian hết hạn
                // DateTime.UtcNow. : lấy múi giờ GMT
                // Mặc định thời điểm hết hạn token sẽ trễ hơn 5ph so với chỉ định
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
