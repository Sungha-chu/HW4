using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UserAndMsg.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAndMsg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private static string Path = "";
        private readonly TestLogContext TestLogContext;
        public ImageController(TestLogContext TestLogContext, IWebHostEnvironment WebHostEnvironment)
        {
            this.TestLogContext = TestLogContext;
            Path = WebHostEnvironment!.WebRootPath;
        }

        // Get: https://localhost:7076/api/Image/ +Id
        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            return TestLogContext.Images; //取得所有資料
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public ActionResult<Image> Get(int id)
        {
            var Result = TestLogContext.Images.Find(id);  //搜尋by Id
            if (Result == null) return NotFound("找不到資料");   //回傳錯誤訊息
            return Result;
        }

        // POST api/<ImageController>
        [HttpPost]
        public ActionResult<Image> Post(IFormFile File)
        {
            try
            {
                if (File == null)
                    return BadRequest("None File Upload.");
                if (File.ContentType != "image/jpeg")
                    return new UnsupportedMediaTypeResult();
                using (FileStream stream = new FileStream(Path + "\\images\\" + File.FileName, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                var Image = new Image();
                Image.Time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                Image.Path = "\\images\\" + File.FileName;
                TestLogContext.Images.Add(Image);// 新增ImageURL一個欄位, Id 資料庫會自動產生
                TestLogContext.SaveChanges();   //儲存
                return Ok("File Upload successful.");
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, IFormFile File)
        {
            try
            {
                var Image = TestLogContext.Images.Find(id);//依照Id取得資料庫資料
                if (Image == null)
                    return BadRequest("Image Id Not Found.");

                if (!System.IO.File.Exists(Path + Image.Path))
                    return BadRequest("Image File Not Found.");

                if (File.ContentType != "image/jpeg")
                    return new UnsupportedMediaTypeResult();

                System.IO.File.Delete(Path + Image.Path);
                using (FileStream stream = new FileStream(Path + "\\images\\" + File.FileName, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                Image.Path = "\\images\\" + File.FileName;
                Image.Time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                TestLogContext.SaveChanges();// await 等待執行完再接續執行
                return Ok("File Update successful.");
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Image = TestLogContext.Images.Find(id);//依照Id取得資料庫資料
                if (Image == null)
                    return BadRequest("Image Id Not Found.");

                if (!System.IO.File.Exists(Path + Image.Path))
                    return BadRequest("Image File Not Found.");

                System.IO.File.Delete(Path + Image.Path);
                TestLogContext.Images.Remove(Image);
                TestLogContext.SaveChanges();
                return Ok("File Delete successful.");
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            }
        }
    }
}
