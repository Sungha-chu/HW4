using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAndMsg.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAndMsg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TestLogContext TestLogContext; //連接DB進行各項操作

        public UserController(TestLogContext _TestLogContext)    //Constructor
        {
            TestLogContext = _TestLogContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return TestLogContext.Users; //取得所有資料
        }

        // GET api/<ValuesController>/5
        [HttpGet("{Id}")]
        public ActionResult<User> Get(string Id)
        {
            var Result = TestLogContext.Users.Find(Id);  //搜尋by Id
            if (Result == null) return NotFound("找不到資料");   //回傳錯誤訊息
            return Result;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<LogItem> Post([FromBody] User Value)
        {
            TestLogContext.Users.Add(Value); //新增資料
            TestLogContext.SaveChanges();   //儲存

            return CreatedAtAction(nameof(Get), new { Account = Value.Account }, Value);  //回傳結果(CreatedAtActionResult)
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{Id}")]
        public IActionResult Put(string Id, [FromBody] User Value)
        {
            if (Id != Value.Account)
            {
                return BadRequest();    //資料不合法
            }

            TestLogContext.Entry(Value).State = EntityState.Modified;   //更新資料

            try
            {
                TestLogContext.SaveChanges();   //儲存
            }
            catch
            {
                if (!TestLogContext.Users.Any(e => e.Account == Value.Account)) return NotFound();   //資料不存在
                else return StatusCode(500, "存取發生錯誤");  //回傳錯誤訊息
            }
            return Ok(); //更新成功
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id)
        {
            var Delete = TestLogContext.Users.Find(Id);  //取得物件
            if (Delete == null) return NotFound();  //若資料不存在回傳錯誤訊息
            TestLogContext.Users.Remove(Delete); //刪除資料
            TestLogContext.SaveChanges();   //儲存
            return Ok();
        }
    }
}
