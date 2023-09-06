using ChatGPT_LineCreat.Domain;
using ChatGPT_LineCreat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ChatGPT_LineCreat.Controllers
{
    //public class LogMessageResponse
    //{
    //    public string lastLogMessage { get; set; }
    //}



    [ApiController]
    [Route("api/[controller]")]
    public class LineBotController : ControllerBase
    {
        // 宣告 service
        private readonly LineBotService _lineBotService;
        public string lastLogMessage;
        //private UdpServiceController _udpServiceController;
        //private UdpService udpService;

        private readonly string channel_Access_Token = "UvZMLvpeD3+HW+rTns/3QmnzWEpzctuVb8M64rplTT6aF8ShPGxMe6Xuacl751A0q5JzKr36LlasT++mPGtcxRvm7fw1zu5WE6ZgjA0DXNW5oaU8Ko5EbgU8vV88oTHOhb+5drh4Bz5jyF+AN0Q+zwdB04t89/1O/w1cDnyilFU=";


        // constructor
        public LineBotController()
        {
            //udpService = new UdpService(8080);
            //_udpServiceController = new UdpServiceController(8080);
            _lineBotService = new LineBotService();
            _lineBotService.LogEvent += LogMessageToView; // 訂閱事件
        }

        [HttpPost]
        [Route("Webhook")]
        public ActionResult Webhook(WebhookRequestBodyDto body)
        {
            try
            {
                _lineBotService.ReceiveWebhook(body); // 呼叫 Service

                // 接收UDP數據
                //string udpData = _udpServiceController.ReceiveData();

                //string receivedData = udpServiceController.ReceiveData();

                return Content("UDP數據成功接收：" + lastLogMessage);
            }
            catch (Exception ex)
            {
                return Content("發生錯誤：" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var replyEvent = new ReplyEvent(channel_Access_Token);

            try
            {
                //Get Post RawData (json format)
                var req = HttpContext.Request;
                using (var bodyReader = new StreamReader(stream: req.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 1024, leaveOpen: true))
                {
                    var body = await bodyReader.ReadToEndAsync();
                    var lineReceMsg = ReceivedMessageConvert.ReceivedMessage(body);

                    if (lineReceMsg != null && lineReceMsg.Events[0].Type == WebhookEventType.message.ToString())
                    {
                        //get user msg
                        var userMsg = lineReceMsg.Events[0].Message.Text;

                        //send reply msg
                        var txtMessage = new TextMessage(userMsg);
                        await replyEvent.ReplyAsync(lineReceMsg.Events[0].ReplyToken,
                                                   new List<IMessage>() {
                                                       txtMessage
                                                   });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok();
            }
            return Ok();

        }

        //[HttpPost]
        //[Route("Webhook")]
        //public ActionResult Webhook(string inputText = "測試成功")
        //{
        //    string result = "You entered: " + inputText;
        //    return Content(result);
        //}

        //[HttpGet]
        //public JsonResult GetData()
        //{
        //    //string data = udpService.ReceiveData();
        //    string data = "111";
        //    var datas = JsonConvert.SerializeObject(data);
        //    //return JsonResult(data, JsonRequestBehavior.AllowGet); ;
        //    return Json(new { datas }, JsonRequestBehavior.AllowGet);
        //}

        //private JsonResult Json(object value, JsonRequestBehavior allowGet)
        //{
        //    throw new NotImplementedException();
        //}

        public void LogMessageToView(string logMessage)
        {
            // 將 logMessage 儲存起來
            lastLogMessage = logMessage;
        }
    }

}
