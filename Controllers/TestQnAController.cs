using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyHostExampleLinebot.Controllers
{
    public class TestQnAController : isRock.LineBot.LineWebHookControllerBase
    {
        const string channelAccessToken = "gu+2fchcvM83yTRH3y3uuRl+F3Sa3i7aPLbydC4ZqYTwX3bx+vCEUGjwyl83vNTQGEH0ECBoIskIe3UkxGRChcHAhBbp7gKYWWApvjmWE+YjrkOtHzE3IsVMfqP4ziRlrpmIYJPnye5kc3+ULqgAUAdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U88b866cb84b99670c7571222f07fe779";
        const string QnAKBId = "c1248730-e6d8-4af5-a695-8f4c0d203f40";
        const string QnAKey = "5708f51f-8717-4856-b6d2-f32fdf3b4b69";
        const string QnAdomain = "aciqna"; //ex.westus
        const string UnknowAnswer = "不好意思，您可以換個方式問嗎? 我不太明白您的意思...";

        [Route("api/TestQnA")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                if (LineEvent.type == "message")
                {
                    var repmsg = "123";
                    if (LineEvent.message.type == "text") //收到文字
                    {
                        //建立 MsQnAMaker Client
                        var helper = new isRock.MsQnAMaker.Client(
                        new Uri("https://aciqna.azurewebsites.net/qnamaker/knowledgebases/c1248730-e6d8-4af5-a695-8f4c0d203f40/generateAnswer"), 
                        "5708f51f-8717-4856-b6d2-f32fdf3b4b69");
                        var QnAResponse = helper.GetResponse(LineEvent.message.text.Trim());
                        var ret = (from c in QnAResponse.answers
                                   orderby c.score descending
                                   select c
                                ).Take(1);

                        var responseText = UnknowAnswer;
                        //取得用戶資訊
                        var UserId = ReceivedMessage.events.FirstOrDefault().source.userId;
                        isRock.LineBot.Bot bot = new
                            isRock.LineBot.Bot(ChannelAccessToken);
                        var UserInfo = bot.GetUserInfo(UserId); if (LineEvent.type == "message")


                        if (ret.FirstOrDefault().score > 0)
                            responseText = ret.FirstOrDefault().answer;
                        //回覆
                        this.ReplyMessage(LineEvent.replyToken, responseText);
                    }
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 2);
                }
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //如果發生錯誤，傳訊息給Admin
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
