using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAndMsg.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace TestLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestLogController : ControllerBase
    {
        private readonly TestLogContext TestLogContext; //連接DB進行各項操作

        public TestLogController(TestLogContext _TestLogContext)    //Constructor
        {
            TestLogContext = _TestLogContext;
        }

        // GET: api/<TestLogController>
        [HttpGet]
        public ActionResult<IEnumerable<LogItem>> Get()
        {
            return TestLogContext.LogItems; //取得所有資料
        }

        // GET api/<TestLogController>/5
        [HttpGet("{Id}")]
        public ActionResult<LogItem> Get(int Id)
        {
            var Result = TestLogContext.LogItems.Find(Id);  //搜尋by Id
            if (Result == null) return NotFound("找不到資料");   //回傳錯誤訊息
            return Result;
        }

        // POST api/<TestLogController>
        [HttpPost]
        public ActionResult<LogItem> Post([FromBody] LogItem Value)
        {
            TestLogContext.LogItems.Add(Value); //新增資料
            TestLogContext.SaveChanges();   //儲存

            return CreatedAtAction(nameof(Get), new { Id = Value.Id }, Value);  //回傳結果(CreatedAtActionResult)
        }

        // PUT api/<TestLogController>/5
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody] LogItem Value)
        {
            if (Id != Value.Id)
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
                if (!TestLogContext.LogItems.Any(e => e.Id == Id)) return NotFound();   //資料不存在
                else return StatusCode(500, "存取發生錯誤");  //回傳錯誤訊息
            }
            return Ok(); //更新成功
        }

        // DELETE api/<TestLogController>/5
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var Delete = TestLogContext.LogItems.Find(Id);  //取得物件
            if (Delete == null) return NotFound();  //若資料不存在回傳錯誤訊息
            TestLogContext.LogItems.Remove(Delete); //刪除資料
            TestLogContext.SaveChanges();   //儲存
            return Ok();
        }
    }
}