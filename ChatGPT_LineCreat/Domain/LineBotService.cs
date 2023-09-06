using ChatGPT_LineCreat.Enum;
using ChatGPT_LineCreat.Models;

namespace ChatGPT_LineCreat.Domain
{
    public class LineBotService 
    {

        // 貼上 messaging api channel 中的 accessToken & secret
        private readonly string channel_Access_Token = "UvZMLvpeD3+HW+rTns/3QmnzWEpzctuVb8M64rplTT6aF8ShPGxMe6Xuacl751A0q5JzKr36LlasT++mPGtcxRvm7fw1zu5WE6ZgjA0DXNW5oaU8Ko5EbgU8vV88oTHOhb+5drh4Bz5jyF+AN0Q+zwdB04t89/1O/w1cDnyilFU=";
        private readonly string channel_Secret = "82267fad77c5f1106b91271f97d1d55a";

        public event Action<string> LogEvent; // 定義事件

        public LineBotService()
        {
        }

        public void ReceiveWebhook(WebhookRequestBodyDto requestBody)
        {
            foreach (var eventObject in requestBody.Events)
            {
                string logMessage = null;

                switch (eventObject.Type)
                {
                    case WebhookEventTypeEnum.Message:
                        logMessage = $"收到使用者傳送訊息！\n {eventObject.Message.Text}";
                        break;
                    case WebhookEventTypeEnum.Unsend:
                        logMessage = $"使用者{eventObject.Source.UserId}在聊天室收回訊息！";
                        break;
                    case WebhookEventTypeEnum.Follow:
                        logMessage = $"使用者{eventObject.Source.UserId}將我們新增為好友！";
                        break;
                    case WebhookEventTypeEnum.Unfollow:
                        logMessage = $"使用者{eventObject.Source.UserId}封鎖了我們！";
                        break;
                    case WebhookEventTypeEnum.Join:
                        logMessage = "我們被邀請進入聊天室了！";
                        break;
                    case WebhookEventTypeEnum.Leave:
                        logMessage = "我們被聊天室踢出了";
                        break;
                }

                if (!string.IsNullOrEmpty(logMessage))
                {
                    LogEvent?.Invoke(logMessage); // 觸發事件
                }
            }
        }
    }

}
