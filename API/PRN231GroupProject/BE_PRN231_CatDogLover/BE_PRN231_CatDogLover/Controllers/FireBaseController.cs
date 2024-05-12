using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FireBaseController : ControllerBase
    {
        private static string ApiKey = "AIzaSyAYQd68fVw9d_jKaRJidWmuLD2nODyu7q0";
        private static string Butket = "voicespire-7162e.appspot.com";
        private static string AuthEmail = "baolongtp54@gmail.com";
        private static string AuthPassword = "123456";


       // [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost("UploadImageFile")]
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

        private async Task<string> UploadIamges(FileStream file, string ext)
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
    }
}
