using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository.Interface;
using Repository;
using DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using NuGet.Protocol.Plugins;
using Firebase.Storage;
using FirebaseAdmin;
using Firebase.Auth;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DataAccess;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Microsoft.Build.Evaluation;

namespace VoiceSpireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceSellersController : ControllerBase
    {
        private static string ApiKey = "AIzaSyAYQd68fVw9d_jKaRJidWmuLD2nODyu7q0";
        private static string Butket = "voicespire-7162e.appspot.com";
        private static string AuthEmail = "baolongtp54@gmail.com";
        private static string AuthPassword = "123456";

        private IVoiceSellerRepository voiceSellerRepository;
        private IBuyerRepository buyerRepository;
        private IVoiceDetailRepository voiceDetailRepository;
        private IVoiceTransactionRepository voiceTransactionRepository;
        private IVoiceProjectRepository voiceProjectRepository;
        private IVoiceJobRepository voiceJobRepository;
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        public VoiceSellersController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            voiceSellerRepository = new VoiceSellerRepository();
            voiceJobRepository = new VoiceJobRepository();
            buyerRepository = new BuyerRepository();
            voiceTransactionRepository = new VoiceTransactionRepository();
            voiceDetailRepository = new VoiceDetailRepository();
            voiceProjectRepository = new VoiceProjectRepository();
            this.mapper = mapper;
        }

        // GET: api/VoiceSellers/5

        [HttpGet("{id}")]
        public async Task<ActionResult<VoiceSellerDTO>> GetVoiceSeller(int id)
        {
            if (voiceSellerRepository.GetAll() == null)
            {
                return NotFound();
            }
            var voiceSeller = mapper.Map<VoiceSellerDTO>(voiceSellerRepository.GetByID(id));

            if (voiceSeller == null)
            {
                return NotFound();
            }

            return voiceSeller;
        }

        [HttpGet("RefreshToken/{token}")]
        public async Task<ActionResult<string>> RefreshToken(string token)
        {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);

                // Thiết lập các tham số giải mã
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                // Lấy các claims từ token
                var roleClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                var idClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "id");
                if (roleClaim == null || idClaim == null)
                {
                    BadRequest("Old token invalid");
                }
                return Ok(RefreshJSONWebToken(roleClaim.Value, int.Parse(idClaim.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest("Old token invalid");
            }



        }

        [HttpGet("GetPage")]
        public async Task<ActionResult<Page<VoiceSellerDTO>>> GetPage(int pageIndex, int pageSize, string? search)
        {
            if (search == null) {
                search = "";
            }
            Page<VoiceSellerDTO> result;
            try
            {
                Page<VoiceSeller> p = voiceSellerRepository.GetPage(pageIndex, pageSize, search);
                result = mapper.Map<Page<VoiceSellerDTO>>(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDTO login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                user.Token = tokenString;
                response = Ok(user);
            }

            return response;
        }



        [HttpPost("UploadVoice")]
        public async Task<IActionResult> UploadVoice(IFormFile file)
        {
            FileStream stream;
            if (file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                string[] audioExtensions = { ".mp3", ".wav", ".ogg", ".flac", ".aac" };


                if (audioExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    stream = ConvertIFormFileToFileStream(file);
                    string link = await Task.Run(() => UploadFile(stream, fileExtension));
                    return Ok(link);
                }
                else {
                    return BadRequest("Extensions invalid");
                }
            }
            return BadRequest();
        }
        public class UploadVoiceModel
        {
            public int voiceSellerId { get; set; }
            public string linkVoice { get; set; }
            public int numberOfEdit { get; set; }
            public int price { get; set; }
        }
        [HttpPost("UploadVoiceProfile")]
        public async Task<IActionResult> UploadVoice(UploadVoiceModel uploadVoiceModel)
        {
            if (voiceSellerRepository.GetByID(uploadVoiceModel.voiceSellerId) == null) return BadRequest("VoiceSeller ID is not exist");
            if (uploadVoiceModel.linkVoice == null || uploadVoiceModel.linkVoice.Trim() == "") return BadRequest("link voice is required");
            if (uploadVoiceModel.numberOfEdit == null) return BadRequest("Number if edit is required");
            if (uploadVoiceModel.numberOfEdit <= 0) return BadRequest("Number of edit invalid");
            if (uploadVoiceModel.price == null) return BadRequest("Price is required");
            if (uploadVoiceModel.price <= 0) return BadRequest("Price invalid");
            if (voiceDetailRepository.GetByID(uploadVoiceModel.voiceSellerId) == null)
            {
                VoiceDetail voiceDetail = new VoiceDetail
                {
                    CreateDate = DateTime.Now,
                    VoiceSellerId = uploadVoiceModel.voiceSellerId,
                    MainVoiceLink = uploadVoiceModel.linkVoice,
                    NumberOfEdit = uploadVoiceModel.numberOfEdit,
                    Price = uploadVoiceModel.price,
                    IsApprove = false,
                    Status = true
                };
                voiceDetailRepository.AddVoiceDetail(voiceDetail);
            }
            else {
                VoiceDetail voiceDetail = voiceDetailRepository.GetByID(uploadVoiceModel.voiceSellerId);
                voiceDetail.CreateDate = DateTime.Now;
                voiceDetail.NumberOfEdit = uploadVoiceModel.numberOfEdit;
                voiceDetail.Price = uploadVoiceModel.price;
                voiceDetail.MainVoiceLink = uploadVoiceModel.linkVoice;
                voiceDetail.IsApprove = false;
                voiceDetail.Status = true;
                voiceDetailRepository.UpdateVoiceDetail(voiceDetail);
            }
            return Ok("Upload success");
        }





        // PUT: api/VoiceSellers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoiceSeller(VoiceSellerDTO voiceSellerDTO)
        {
            if (voiceSellerRepository.GetByID(voiceSellerDTO.VoiceSellerId) == null)
            {
                return BadRequest("Not found voice seller to update");
            }
            try
            {
                voiceSellerRepository.UpdateVoiceSeller(mapper.Map<VoiceSeller>(voiceSellerDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPut("UpdateBankInformation/{sellerId},{bankNumber},{bankName},{bankAccoutName}")]
        public async Task<IActionResult> UpdateBankInformation(int sellerId, string bankNumber, string bankName, string bankAccoutName)
        {
            if (voiceSellerRepository.GetByID(sellerId) == null)
            {
                return BadRequest("Not found voice seller to update");
            }
            if (bankNumber.Trim() == "" || bankName.Trim() == "" || bankAccoutName.Trim() == "") {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }
            VoiceSeller voiceSeller = voiceSellerRepository.GetByID(sellerId);
            voiceSeller.BankNumber = bankNumber;
            voiceSeller.BankAccountName = bankAccoutName;
            voiceSeller.BankName = bankName;
            try
            {
                voiceSellerRepository.UpdateVoiceSeller(voiceSeller);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPut("ChangePass")]
        public async Task<IActionResult> PutVoiceSeller(int id, string pass)
        {

            if (voiceSellerRepository.GetByID(id) == null)
            {
                return BadRequest("Not found voice seller to update");
            }
            try
            {
                VoiceSeller voiceSeller = voiceSellerRepository.GetByID(id);
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hash = BCrypt.Net.BCrypt.HashPassword(pass, salt);
                voiceSeller.Password = hash;
                voiceSellerRepository.UpdateVoiceSeller(voiceSeller);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/VoiceSellers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<VoiceSellerDTO>> PostVoiceSeller(VoiceSellerRegisterDTO voiceSellerRegisterDTO)
        {
            if (voiceSellerRegisterDTO.Fullname == null
                || voiceSellerRegisterDTO.Status == null
                || voiceSellerRegisterDTO.Email == null
                || voiceSellerRegisterDTO.Gender == null
                || voiceSellerRegisterDTO.Password == null
                || voiceSellerRegisterDTO.PasswordConfirm == null
                ||voiceSellerRegisterDTO.Fullname.Trim() == ""
                || voiceSellerRegisterDTO.Email.Trim() == ""
                || voiceSellerRegisterDTO.Gender.Trim() == ""
                || voiceSellerRegisterDTO.Password.Trim() == ""
                || voiceSellerRegisterDTO.PasswordConfirm.Trim() == "") {
                return BadRequest("Data invalid");
            }
            if (!voiceSellerRegisterDTO.Email.Contains("@")) return BadRequest("Email invalid");
            if (voiceSellerRegisterDTO.Password != voiceSellerRegisterDTO.PasswordConfirm) {
                return BadRequest("Password not match");
            }
            if (voiceSellerRegisterDTO.Password.Length < 8)
            {
                return BadRequest("Password's length must greater than 8");
            }
            if (voiceSellerRepository.checkVoiceSellerExits(voiceSellerRegisterDTO.Email) || buyerRepository.checkBuyerExits(voiceSellerRegisterDTO.Email)) {
                return BadRequest("Email has already been used");
            }
            try {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hash = BCrypt.Net.BCrypt.HashPassword(voiceSellerRegisterDTO.Password, salt);
                VoiceSellerDTO voiceSellerDTO = mapper.Map<VoiceSellerDTO>(voiceSellerRegisterDTO);
                voiceSellerDTO.Password = hash;
                voiceSellerDTO.RateAvg = 0;
                voiceSellerRepository.AddVoiceSeller(mapper.Map<VoiceSeller>(voiceSellerDTO));
            } catch (Exception ex)
            {
                return BadRequest("Data invalid");
            }
            LoginRequestDTO login = new LoginRequestDTO {
                Email = voiceSellerRegisterDTO.Email,
                Password = voiceSellerRegisterDTO.Password
            };
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                user.Token = tokenString;
            }
            return Ok(user);
        }

        // DELETE: api/VoiceSellers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoiceSeller(int id)
        {
            if (voiceSellerRepository.GetAll() == null)
            {
                return NotFound();
            }
            var voiceSeller = voiceSellerRepository.GetByID(id);
            if (voiceSeller == null)
            {
                return NotFound();
            }

            voiceSellerRepository.RemoveVoiceSeller(id);

            return NoContent();
        }

        [HttpPut("AcceptProject/{id}")]
        public async Task<ActionResult> AcceptProject(int id)
        {
            VoiceJob voiceJob = voiceJobRepository.GetByProjectIdForProjectSend(id);
            if (voiceJob == null) return NotFound("Not found project");
            voiceJob.VoiceJobStatus = "Processing";
            voiceJobRepository.UpdateVoiceJob(voiceJob);
            if (voiceProjectRepository.UpdateVoiceProjectStatus(id, "Processing") != null) return Ok("Accepted");
            return NotFound("Not found project");
        }
        [HttpPut("DeniesProject/{id}")]
        public async Task<ActionResult> DenieProject(int id)
        {
            VoiceJob voiceJob = voiceJobRepository.GetByProjectIdForProjectSend(id);
            if (voiceJob == null) return NotFound("Not found project");
            voiceJob.VoiceJobStatus = "Denied";
            voiceJobRepository.UpdateVoiceJob(voiceJob);
            if (voiceProjectRepository.UpdateVoiceProjectStatus(id, "Denied") != null) return Ok("Denied");
            return NotFound("Not found project");
        }

        [HttpPost("ApllyToProject")]
        public async Task<ActionResult> ApplyToProject(ApplyToProjectDTO applyToProjectDTO) {
            if (voiceJobRepository.GetByProjectIdForProjectPost(applyToProjectDTO.VoiceProjectId).Any(v => v.VoiceSellerId == applyToProjectDTO.VoiceSellerId)) {
                return BadRequest("Voice seller has already applied to this project");
            }
            VoiceJob voiceJob = new VoiceJob
            {
                VoiceSellerId = applyToProjectDTO.VoiceSellerId,
                VoiceJobStatus = "Applying",
                VoiceProjectId = applyToProjectDTO.VoiceProjectId,
                LinkDemo = applyToProjectDTO.LinkDemo,
                Status = true,
            };
            try {
                voiceJobRepository.AddVoiceJob(voiceJob);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok(voiceJob);
        }

        [HttpPost("SendMainVoiceForProject")]
        public async Task<ActionResult> SendMainVoiceForProject(UploadMainVoiceDTO uploadMainVoiceDTO) {
            if (voiceProjectRepository.GetById(uploadMainVoiceDTO.VoiceProjectId).ProjectStatus.ToLower().Trim() != "processing") {
                return BadRequest("Project is not processing");
            }
            if (voiceJobRepository.GetByProjectIdForProjectPost(uploadMainVoiceDTO.VoiceProjectId).SingleOrDefault(v => v.VoiceSellerId == uploadMainVoiceDTO.VoiceSellerId && v.VoiceJobStatus == "Processing") == null) {
                return BadRequest("This seller is not accept to project");
            }
            if (voiceTransactionRepository.CountTransactionOfProject(uploadMainVoiceDTO.VoiceProjectId) >= voiceProjectRepository.GetById(uploadMainVoiceDTO.VoiceProjectId).NumberOfEdit) {
                return BadRequest("Exceeded project revision count");
            }
            if (voiceTransactionRepository.getLastestTransactionByProjectIdAndSellerId(uploadMainVoiceDTO.VoiceProjectId, uploadMainVoiceDTO.VoiceSellerId) != null && voiceTransactionRepository.getLastestTransactionByProjectIdAndSellerId(uploadMainVoiceDTO.VoiceProjectId, uploadMainVoiceDTO.VoiceSellerId).VoiceTransactionStatus == "Wait") {
                return BadRequest("The lastest audio has not yet feedback!");
            }
            VoiceTransaction voiceTransaction = new VoiceTransaction
            {
                Status = true,
                LinkVoice = uploadMainVoiceDTO.LinkVoice,
                VoiceProjectId = uploadMainVoiceDTO.VoiceProjectId,
                VoiceSellerId = uploadMainVoiceDTO.VoiceSellerId,
                VoiceTransactionStatus = "Wait",
                CreateDate = DateTime.Now,
            };
            voiceTransactionRepository.AddVoiceTransaction(voiceTransaction);
            if (voiceTransactionRepository.CountTransactionOfProject(uploadMainVoiceDTO.VoiceProjectId) == voiceProjectRepository.GetById(uploadMainVoiceDTO.VoiceProjectId).NumberOfEdit)
            {
                voiceProjectRepository.UpdateVoiceProjectStatus(uploadMainVoiceDTO.VoiceProjectId, "Done");
                VoiceJob voiceJob = voiceJobRepository.GetByProjectIdAndSellerId(voiceTransaction.VoiceProjectId, voiceTransaction.VoiceSellerId);
                voiceJob.VoiceJobStatus = "Done";
                voiceJobRepository.UpdateVoiceJob(voiceJob);
            }

            return Created("", "Send voice success");
        }
        [HttpGet("SearchVoiceJobBySellerId/{currentPage},{PageSize},{sellerId}")]
        public async Task<ActionResult<Page<VoiceJobDTO>>> SearchBySellerId(int currentPage, int PageSize, int sellerId, int? fromPrice, int? toPrice, int? duration, string? search) {
            if (fromPrice == null) fromPrice = 0;
            if (toPrice == null) toPrice = int.MaxValue;
            if (duration == null) duration = int.MaxValue;
            if (search == null) search = "";
            Page<VoiceJob> tmp = voiceJobRepository.GetBySellerId(currentPage, PageSize, sellerId,(int) fromPrice,(int) toPrice,(int) duration, search);
            if (tmp == null) return NotFound("Not found any job");
            Page<VoiceJobDTO> result = mapper.Map<Page<VoiceJobDTO>>(tmp);
            return Ok(result);
        }

        [HttpGet("CheckPaymentInfo/{sellerId}")]
        public async Task<ActionResult<bool>> checkPaymentStatus(int sellerId)
        {
            if (voiceSellerRepository.GetByID(sellerId) == null) return NotFound("Not found seller");
            return Ok(voiceSellerRepository.checkPaymentInfo(sellerId));
        }

        private LoginResponseDTO AuthenticateUser(LoginRequestDTO login)
        {
            LoginResponseDTO user = null;
            if (login.Email == "VoiceSpireManager@gmail.com" && login.Password == "12345")
            {
                user = new LoginResponseDTO { Role = "manager" };
            }
            if (voiceSellerRepository.GetByEmailAndPassword(login.Email, login.Password) != null)
            {
                VoiceSellerDTO voiceSeller = mapper.Map<VoiceSellerDTO>(voiceSellerRepository.GetByEmailAndPassword(login.Email, login.Password));
                user = new LoginResponseDTO { VoiceSeller = voiceSeller, Role = "seller" };
            } else if (buyerRepository.GetByEmailAndPassword(login.Email, login.Password) != null)
            {
                BuyerDTO buyerDTO = mapper.Map<BuyerDTO>(buyerRepository.GetByEmailAndPassword(login.Email, login.Password));
                user = new LoginResponseDTO {  Buyer = buyerDTO, Role = "buyer"};  
            } 
            return user;
        }
        private string GenerateJSONWebToken(LoginResponseDTO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int id = 0;
            if (userInfo.Role == "seller")
            {
                id = userInfo.VoiceSeller.VoiceSellerId;
            }
            if (userInfo.Role == "buyer")
            {
                id = userInfo.Buyer.BuyerId;
            }

            var claims = new[] {
        new Claim(ClaimTypes.Role, userInfo.Role),
        new Claim("id", id.ToString())
    };

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string RefreshJSONWebToken(string role, int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(ClaimTypes.Role, role),
        new Claim("id", id.ToString())
    };

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private FileStream ConvertIFormFileToFileStream(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return null;
            }

            // Lấy Stream từ IFormFile
            Stream stream = formFile.OpenReadStream();

            // Tạo một tệp tạm thời với phần mở rộng .tmp hoặc tên tệp duy nhất
            string tempFileName = $"{Guid.NewGuid()}.tmp";

            // Tạo FileStream từ Stream
            FileStream fileStream = new FileStream(tempFileName, FileMode.Create);

            // Sao chép dữ liệu từ Stream của IFormFile vào FileStream
            stream.CopyTo(fileStream);

            // Đặt vị trí của FileStream về đầu tệp
            fileStream.Seek(0, SeekOrigin.Begin);

            // Đóng Stream của IFormFile, FileStream vẫn được sử dụng
            stream.Close();

            return fileStream;
        }
        private async Task<string> UploadFile(FileStream file, string ext)
        {
            string link = "";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ext;
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Butket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                )
                .Child("voices")
                .Child(fileName)
                .PutAsync(file, cancellation.Token)
                ;
            try
            {
                link = await task;
            }
            catch (Exception ex)
            {
                return null;
            }
            return link;
        }
    }
}
