using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace ChatGPT_LineCreat.Controllers
{
    public class ChatGptController : ApiController
    {

        private static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// 一句話的問答與回應
        /// </summary>
        /// <param name="Param">使用者文字輸入</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> ChatGPTAns(string Param)
        {
            string apiKey = "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";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            ///httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var jsonContent = new
            {
                prompt = Param,
                model = "text-davinci-003",
                max_tokens = 1000
            };
            var responseContent = await httpClient.PostAsync("https://api.openai.com/v1/completions",
                                    new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json")
                                );

            var resContext = await responseContent.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(resContext);
            if (data == null)
            {
                return "Data Error";
            }
            return data.choices[0].text;
        }



    }
}
