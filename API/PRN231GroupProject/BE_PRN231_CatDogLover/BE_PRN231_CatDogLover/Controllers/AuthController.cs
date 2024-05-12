using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly IMapper mapper;
        private IAccountRepository accountRepository;
        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            accountRepository = new AccountRepository();
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            Account account = accountRepository.GetAll().SingleOrDefault(a => a.Email == login.Email && a.Password == login.Password && a.Role.RoleName == "user");
            if (account != null)
            {
                account.Version = account.Version + 1;
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName,(int) account.Version, account.AccountId);
                
                account.RefreshToken = loginRespone.RefreshToken;
                account = await accountRepository.UpdateAccount(account);
                loginRespone.Account = mapper.Map<AccountDTO>(account);
                loginRespone.Account.Password = null;
                response = Ok(loginRespone);
            }
            return response;
        }
        [AllowAnonymous]
        [HttpPost("LoginStaff")]
        public async Task<IActionResult> LoginStaff([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            Account account = accountRepository.GetAll().SingleOrDefault(a => a.Email == login.Email && a.Password == login.Password && a.Role.RoleName == "staff");
            if (account != null)
            {
                account.Version = account.Version + 1;
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName, (int) account.Version, account.AccountId);
                
                account.RefreshToken = loginRespone.RefreshToken;
                account = await accountRepository.UpdateAccount(account);
                loginRespone.Account = mapper.Map<AccountDTO>(account);
                loginRespone.Account.Password = null;
                response = Ok(loginRespone);
            }
            return response;
        }
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            Account account = accountRepository.GetAll().SingleOrDefault(a => a.Email == login.Email && a.Password == login.Password && a.Role.RoleName == "admin");
            if (account != null)
            {
                account.Version = account.Version + 1;
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName, (int)account.Version, account.AccountId);

                account.RefreshToken = loginRespone.RefreshToken;
                account = await accountRepository.UpdateAccount(account);
                loginRespone.Account = mapper.Map<AccountDTO>(account);
                loginRespone.Account.Password = null;
                response = Ok(loginRespone);
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("Logout/{refreshToken}")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            Account account = await accountRepository.GetByRefreshToken(refreshToken);
            if (account == null) return BadRequest("Refresh token invalid!");
            try
            {
                account = await accountRepository.UpdateVersion(account.AccountId);
                account.RefreshToken = null;
                await accountRepository.UpdateAccount(account);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Refresh Token");
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AccountDTO>> RegisterUser(RegisterRequest register)
        {
            if (!ModelState.IsValid) return BadRequest("Data invalid");
            if (register.PasswordConfirm != register.Password)
            {
                return BadRequest("Password not match with confirm");
            }
            if (accountRepository.GetAll().Any(a => a.Email.ToLower().Trim() == register.Email.ToLower().Trim()))
            {
                return BadRequest("Email are already exist");
            }
            Account account = new Account();
            try
            {              
                account = mapper.Map<Account>(register);
                account.CreateDate = DateTime.Now;
                account.RoleId = 1;
                account.Status = true;
                account.Version = 1;
                accountRepository.AddAccount(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("", mapper.Map<AccountDTO>(account));
        }

        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult Test()
        {
            return Ok("hihi");
        }

        [HttpGet("RefreshToken/{refreshToken}")]
        public async Task<ActionResult<LoginRespone>> RefreshToken(string refreshToken)
        {
            Account account = await accountRepository.GetByRefreshToken(refreshToken);
            if (account == null) return BadRequest("Refresh token invalid!");
            try
            {
                account = await accountRepository.UpdateVersion(account.AccountId);
                account = await accountRepository.GetAccountById(account.AccountId);
                LoginRespone loginRespone = GenerateToken(account.Role.RoleName,(int) account.Version, account.AccountId);
                account.RefreshToken = loginRespone.RefreshToken;
                await accountRepository.UpdateAccount(account);
                return Ok(loginRespone); 
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Refresh Token");
            }
        }




        private LoginRespone GenerateToken(string Role, int version, int id)
        {
            var jwtKey = Encoding.ASCII.GetBytes(Configuration["jwtKey"]);
            var refreshKey = Encoding.ASCII.GetBytes(Configuration["jwtKeyRefresh"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, Role),
                    new Claim("version", version.ToString()),
                    new Claim("id", id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60), // Thời hạn của Access Token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, Role),
                    new Claim("version", version.ToString()),
                    new Claim("id", id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(refreshKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var refresh = tokenHandler.CreateToken(refreshDescriptor);
            var refreshToken = tokenHandler.WriteToken(refresh);

            return new LoginRespone
            {
                Role = Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
