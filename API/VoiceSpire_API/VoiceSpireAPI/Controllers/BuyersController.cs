using AutoMapper;
using BusinessObject;
using DTO;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VoiceSpireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private static string ApiKey = "AIzaSyAYQd68fVw9d_jKaRJidWmuLD2nODyu7q0";
        private static string Butket = "voicespire-7162e.appspot.com";
        private static string AuthEmail = "baolongtp54@gmail.com";
        private static string AuthPassword = "123456";

        private IBuyerRepository buyerRepository;
        private IVoiceSellerRepository voiceSellerRepository;
        private IVoiceDetailRepository voiceDetailRepository;
        private IVoiceProjectRepository voiceProjectRepository;
        private readonly IConfiguration Configuration;
        private IVoiceTransactionRepository voiceTransactionRepository;
        private readonly IMapper mapper;
        private IVoiceJobRepository voiceJobRepository;
        public BuyersController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            buyerRepository = new BuyerRepository();
            voiceSellerRepository = new VoiceSellerRepository();
            voiceDetailRepository = new VoiceDetailRepository();
            voiceProjectRepository = new VoiceProjectRepository();
            voiceTransactionRepository = new VoiceTransactionRepository();
            voiceJobRepository = new VoiceJobRepository();
            this.mapper = mapper;
        }


        // GET: api/Buyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuyerDTO>> GetBuyer(int id)
        {
            if (buyerRepository.GetAll() == null)
            {
                return NotFound();
            }
            var buyer = mapper.Map<BuyerDTO>(buyerRepository.GetByID(id));

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(BuyerDTO buyerDTO)
        {
            if (buyerRepository.GetByID(buyerDTO.BuyerId) == null)
            {
                return BadRequest("Not found buyer to update");
            }


            try
            {
                buyerRepository.UpdateBuyer(mapper.Map<Buyer>(buyerDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            if (buyerRepository.GetAll() == null)
            {
                return NotFound();
            }
            var buyer = buyerRepository.GetByID(id);
            if (buyer == null)
            {
                return NotFound();
            }

            buyerRepository.RemoveBuyer(id);

            return NoContent();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<BuyerDTO>> PostBuyer(BuyerRegisterDTO buyerRegisterDTO)
        {
            if (buyerRegisterDTO.Fullname == null
                || buyerRegisterDTO.Status == null
                || buyerRegisterDTO.Email == null
                || buyerRegisterDTO.Password == null
                || buyerRegisterDTO.PasswordConfirm == null
                || buyerRegisterDTO.Fullname.Trim() == ""
                || buyerRegisterDTO.Email.Trim() == ""
                || buyerRegisterDTO.Password.Trim() == ""
                || buyerRegisterDTO.PasswordConfirm.Trim() == "")
            {
                return BadRequest("Data invalid");
            }
            if (!buyerRegisterDTO.Email.Contains("@")) return BadRequest("Email invalid");
            if (buyerRegisterDTO.Password != buyerRegisterDTO.PasswordConfirm)
            {
                return BadRequest("Password not match");
            }
            if (buyerRegisterDTO.Password.Length < 8)
            {
                return BadRequest("Password's length must greater than 8");
            }
            if (buyerRepository.checkBuyerExits(buyerRegisterDTO.Email) || voiceSellerRepository.checkVoiceSellerExits(buyerRegisterDTO.Email))
            {
                return BadRequest("Email has already been used");
            }
            try
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hash = BCrypt.Net.BCrypt.HashPassword(buyerRegisterDTO.Password, salt);
                BuyerDTO buyerDTO = mapper.Map<BuyerDTO>(buyerRegisterDTO);
                buyerDTO.Password = hash;
                buyerRepository.AddBuyer(mapper.Map<Buyer>(buyerDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            LoginRequestDTO login = new LoginRequestDTO
            {
                Email = buyerRegisterDTO.Email,
                Password = buyerRegisterDTO.Password
            };
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                user.Token = tokenString;
            }
            return Ok(user);
        }

        [HttpGet("GetPage")]
        public async Task<ActionResult<Page<BuyerDTO>>> GetPage(int pageIndex, int pageSize, string? search)
        {
            if (search == null)
            {
                search = "";
            }
            Page<BuyerDTO> result;
            try
            {
                Page<Buyer> p = buyerRepository.GetPage(pageIndex, pageSize, search);
                result = mapper.Map<Page<BuyerDTO>>(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return result;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<BuyerDTO>>> GetAll()
        {
            if (buyerRepository.GetAll() == null)
            {
                return BadRequest("There are no buyers");
            }
            return Ok(buyerRepository.GetAll());
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<BuyerDTO>>> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword is empty or null");
            }
            IEnumerable<Buyer> buyers;
            try
            {
                buyers = buyerRepository.GetBuyerByName(keyword);

                if (!buyers.Any())
                {
                    return NotFound("No buyers found matching the keyword");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(buyers);
        }

        public class LinkUploadProject
        {
            public string LinkDocDemo { get; set; }
            public string LinkDocMain { get; set; }
            public string LinkThumbnail { get; set; }
        }

        [HttpPost("UploadVoiceProject/{BuyerId},{title},{description},{price},{duration},{numberOfEdit},{deadline}")]
        public async Task<ActionResult<VoiceProject>> UploadVoiceProject(int BuyerId, string title, string description, string? request, int price, string? voiceProperty, int duration, int textLength, string? voiceGender, int numberOfEdit, int voiceTone, string? voiceRegion, string? voiceLocal, int voiceInspirational, int voiceStress, int voicePronuonce, int voiceSpeed, DateTime deadline, LinkUploadProject linkUploadProject)
        {
            if (title == null
                || description == null
                || linkUploadProject.LinkDocDemo == null
                || linkUploadProject.LinkDocMain == null
                || price == 0)
            {
                return BadRequest("Data invalid");
            }

            VoiceProject voiceProject = new VoiceProject()
            {
                BuyerId = BuyerId,
                Title = title,
                Description = description,
                Request = request,
                LinkDocDemo = linkUploadProject.LinkDocDemo,
                LinkDocMain = linkUploadProject.LinkDocMain,
                LinkThumbnail = linkUploadProject.LinkThumbnail,
                BankCode = GenerateBankCode(20),
                Price = price,
                ToalOutputPrice = price * duration,
                VoiceProperty = (voiceProperty == null ? "" : voiceProperty),
                Duration = duration,
                VoiceGender = (voiceGender == null ? "" : voiceGender),
                VoiceTone = (voiceTone == null ? 0 : voiceTone),
                VoiceRegion = (voiceRegion == null ? "" : voiceRegion),
                VoiceLocal = (voiceLocal == null ? "" : voiceLocal),
                TextLength = (textLength == null ? 0 : textLength),
                VoiceInspirational = (voiceInspirational == null ? 0 : voiceInspirational),
                VoiceStress = (voiceStress == null ? 0 : voiceStress),
                VoicePronouce = (voicePronuonce == null ? 0 : voicePronuonce),
                VoiceSpeed = (voiceSpeed == null ? 0 : voiceSpeed),
                NumberOfEdit = numberOfEdit,
                Deadline = deadline,
                CreateDate = DateTime.Now,
                ProjectStatus = "WaitApprove",
                PaymentStatus = "Pending",
                ProjectType = "Post",
                Status = true
            };
            VoiceProject result;
            try
            {
                result = voiceProjectRepository.AddVoiceProject(voiceProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("SendVoiceProject/{BuyerId},{voiceSellerId},{title},{description},{duration},{deadline}")]
        public async Task<ActionResult<VoiceProject>> SendProject(int BuyerId,string title, int voiceSellerId, string description, string? request, int duration, DateTime deadline, LinkUploadProject linkUploadProject)
        {

            if (duration == 0)
            {
                return BadRequest("Duration invalid");
            }
            if (request == null) request = "";
            VoiceDetail voiceDetail = voiceDetailRepository.GetByID(voiceSellerId);
            VoiceProject voiceProject = new VoiceProject()
            {
                BuyerId = BuyerId,
                Title = title,
                Description = description,
                Request = request,
                LinkDocDemo = linkUploadProject.LinkDocDemo,
                LinkDocMain = linkUploadProject.LinkDocMain,
                LinkThumbnail = linkUploadProject.LinkThumbnail,
                BankCode = GenerateBankCode(20),
                Price = (int)voiceDetail.Price,
                ToalOutputPrice = (int) voiceDetail.Price * duration,
                VoiceProperty = "",
                Duration = duration,
                VoiceGender = "",
                VoiceTone = 0,
                VoiceRegion = "",
                VoiceLocal = "",
                TextLength = 0,
                VoiceInspirational = 0,
                VoiceStress = 0,
                VoicePronouce = 0,
                VoiceSpeed = 0,
                NumberOfEdit = ((voiceDetail.NumberOfEdit!=null&&voiceDetail.NumberOfEdit!=0)?((int)voiceDetail.NumberOfEdit):3),
                Deadline = deadline,
                CreateDate = DateTime.Now,
                ProjectStatus = "WaitApprove",
                PaymentStatus = "pending",
                ProjectType = "send",
                Status = true
            };
            VoiceProject result;
            try
            {
                result = voiceProjectRepository.AddVoiceProject(voiceProject);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            VoiceJob voiceJob = new VoiceJob()
            {
                VoiceProjectId = result.VoiceProjectId,
                VoiceSellerId = voiceSellerId,
                VoiceJobStatus = "waitToAccept",
                Status = false
            };
            try
            {
                voiceJob = voiceJobRepository.AddVoiceJob(voiceJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        private LoginResponseDTO AuthenticateUser(LoginRequestDTO login)
        {
            LoginResponseDTO user = null;

            if (voiceSellerRepository.GetByEmailAndPassword(login.Email, login.Password) != null)
            {
                VoiceSellerDTO voiceSeller = mapper.Map<VoiceSellerDTO>(voiceSellerRepository.GetByEmailAndPassword(login.Email, login.Password));
                user = new LoginResponseDTO { VoiceSeller = voiceSeller, Role = "seller" };
            }
            else if (buyerRepository.GetByEmailAndPassword(login.Email, login.Password) != null)
            {
                BuyerDTO buyerDTO = mapper.Map<BuyerDTO>(buyerRepository.GetByEmailAndPassword(login.Email, login.Password));
                user = new LoginResponseDTO { Buyer = buyerDTO, Role = "buyer" };
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

        [HttpPost("UploadDocFile")]
        public async Task<IActionResult> UploadDocs(IFormFile file)
        {
            FileStream stream;
            if (file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                string[] audioExtensions = { ".txt", ".doc", ".docx", ".pdf" };


                if (audioExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    stream = ConvertIFormFileToFileStream(file);
                    string link = await Task.Run(() => UploadDoc(stream, fileExtension));
                    return Ok(link);
                }
                else
                {
                    return BadRequest("Extensions invalid");
                }
            }
            return BadRequest();
        }
        [HttpPost("UploadImageFile")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            FileStream stream;
            if (file.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                string[] audioExtensions = { ".jpg", ".png", ".gif", ".bmp", ".tiff", ".svg", ".psd", ".ico", ".raw", ".eps", ".ai", ".jpeg 2000", ".webp", ".exif", ".pcx", ".pbm", ".pgm", ".ppm", ".pnm", ".tga", ".xbm", ".xpm" };


                if (audioExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    stream = ConvertIFormFileToFileStream(file);
                    string link = await Task.Run(() => UploadIamges(stream, fileExtension));
                    return Ok(link);
                }
                else
                {
                    return BadRequest("Extensions invalid");
                }
            }
            return BadRequest();
        }

        [HttpPut("ApproveDemo/{voiceJobId},{projectId}")]
        public async Task<ActionResult> ApproveDemo(int voiceJobId, int projectId) {
            List<VoiceJob> listVJ = voiceJobRepository.GetByProjectIdForProjectPost(projectId).ToList();
            if (listVJ == null || listVJ.Count == 0) return BadRequest("Dont find project");
            if (!listVJ.Any(v => v.VoiceJobId == voiceJobId)) {
                return BadRequest("Dont find this demo in Project");
            }
            for (int i = 0; i < listVJ.Count; i++) {
                VoiceJob vj = voiceJobRepository.GetById(listVJ[i].VoiceJobId);
                if (vj.VoiceJobId == voiceJobId) vj.VoiceJobStatus = "Processing";
                else vj.VoiceJobStatus = "Denied";
                voiceJobRepository.UpdateVoiceJob(vj);
            }
            voiceProjectRepository.UpdateVoiceProjectStatus(projectId, "Processing");
            return Ok();
        }

        [HttpPut("AcceptTransaction/{transactionId}")]
        public async Task<ActionResult> AcceptTransaction(int transactionId) {
            VoiceTransaction voiceTransaction = voiceTransactionRepository.getById(transactionId);
            if (voiceTransaction == null) return NotFound("Not found transaction");
            voiceTransaction.VoiceTransactionStatus = "Done";
            voiceTransactionRepository.Update(voiceTransaction);
            voiceProjectRepository.UpdateVoiceProjectStatus(voiceTransaction.VoiceProjectId, "Done");
            VoiceJob voiceJob = voiceJobRepository.GetByProjectIdAndSellerId(voiceTransaction.VoiceProjectId, voiceTransaction.VoiceSellerId);
            voiceJob.VoiceJobStatus = "Done";
            voiceJobRepository.UpdateVoiceJob(voiceJob);

            return Ok("Accepted");
        }

        [HttpPut("RequestEdit/{transactionId}")]
        public async Task<ActionResult> RequestEdit(int transactionId, string feedback)
        {

            VoiceTransaction voiceTransaction = voiceTransactionRepository.getById(transactionId);
            if (voiceTransactionRepository.CountTransactionOfProject(voiceTransaction.VoiceProjectId) >= voiceProjectRepository.GetById(voiceTransaction.VoiceProjectId).NumberOfEdit)
            {
                return BadRequest("Exceeded project revision count");
            }
            if (voiceTransaction == null) return NotFound("Not found transaction");
            voiceTransaction.VoiceTransactionStatus = "Continue";
            voiceTransaction.Feedback = feedback;
            voiceTransactionRepository.Update(voiceTransaction);
            return Ok("Requeted");
        }

        [HttpGet("GetProjectByBuyerId/{buyerId}")]
        public async Task<ActionResult<List<VoiceProjectDTO>>> GetVoiceProjectByBuyerId(int buyerId) {
            List<VoiceProject> tmp = voiceProjectRepository.GetByBuyerId(buyerId).ToList();
            if (tmp == null || tmp.Count <= 0) return NotFound("Buyer dont have any project");
            return Ok(mapper.Map<List<VoiceProjectDTO>>(tmp));
        }

        [HttpPut("UpdateBankInformation/{buyerId},{bankNumber},{bankName},{bankAccoutName}")]
        public async Task<IActionResult> UpdateBankInformation(int buyerId, string bankNumber, string bankName, string bankAccoutName)
        {
            if (buyerRepository.GetByID(buyerId) == null)
            {
                return BadRequest("Not found buyer to update");
            }
            if (bankNumber.Trim() == "" || bankName.Trim() == "" || bankAccoutName.Trim() == "")
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }
            Buyer buyer = buyerRepository.GetByID(buyerId);
            buyer.BankNumber = bankNumber;
            buyer.BankAccountName = bankAccoutName;
            buyer.BankName = bankName;
            try
            {
                buyerRepository.UpdateBuyer(buyer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("CheckPaymentInfo/{buyerId}")]
        public async Task<ActionResult<bool>> checkPaymentStatus(int buyerId) {
            if (buyerRepository.GetByID(buyerId) == null) return NotFound("Not found buyer");
            return Ok(buyerRepository.checkPaymentInfo(buyerId));
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
        private async Task<string> UploadDoc(FileStream file, string ext)
        {
            string link = "";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+ext;
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
                .Child("docs")
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
        private async Task<string> UploadIamges(FileStream file, string ext)
        {
            string link = "";
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+ext;
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
                .Child("imgs")
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

        private string GenerateBankCode(int length)
        {
            string format = "yyyyMMddHHmmss";
            string dateTimeString = DateTime.Now.ToString(format);
            Random random = new Random();

            if (length < format.Length)
            {
                throw new ArgumentException("Length is too short to generate a bank code.");
            }

            int remainingLength = length - format.Length;
            string randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] keyChars = new char[length];

            for (int i = 0; i < format.Length; i++)
            {
                keyChars[i] = dateTimeString[i];
            }

            for (int i = format.Length; i < length; i++)
            {
                keyChars[i] = randomChars[random.Next(randomChars.Length)];
            }

            return new string(keyChars);
        }


    }


}
