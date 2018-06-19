using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IronPython.Hosting; //導入IronPython庫文件
using Microsoft.Scripting.Hosting; //導入微軟腳本解釋庫文件


namespace LUIS.Controllers
{
    public class TestLUISController : isRock.LineBot.LineWebHookControllerBase
    {

        const string channelAccessToken = "gu+2fchcvM83yTRH3y3uuRl+F3Sa3i7aPLbydC4ZqYTwX3bx+vCEUGjwyl83vNTQGEH0ECBoIskIe3UkxGRChcHAhBbp7gKYWWApvjmWE+YjrkOtHzE3IsVMfqP4ziRlrpmIYJPnye5kc3+ULqgAUAdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "U88b866cb84b99670c7571222f07fe779";
        const string LuisAppId = "e1c7f354-d37a-4ab5-8559-3bdfc99c7ddc";
        const string LuisAppKey = "0e23d89972bd40689f5e5a0711320bef";
        const string Luisdomain = "southeastasia"; //ex.westus


        [Route("api/TestLUIS")]
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
                    var repmsg = "";
                    string postData = Request.Content.ReadAsStringAsync().Result;
                    var ReceiveMessage = isRock.LineBot.Utility.Parsing(postData);
                    var actions = new
                        List<isRock.LineBot.TemplateActionBase>();
                    var actions2 = new
                        List<isRock.LineBot.TemplateActionBase>();
                    var actions3 = new
                        List<isRock.LineBot.TemplateActionBase>();
                    var Medical = new
                        isRock.LineBot.ButtonsTemplate();
                    var Hospital = new
                        isRock.LineBot.ButtonsTemplate();
                    var ConfirmTemplateMsg = new
                        isRock.LineBot.ConfirmTemplate();

                    //var botevent = this.ReceivedMessage.events.FirstOrDefault();
                    //var Postdata = botevent.postback.data;


                    //抓取用戶UserId、UserInfo
                    var UserId = ReceivedMessage.events.FirstOrDefault().source.userId;
                    isRock.LineBot.Bot bot1 = new isRock.LineBot.Bot(channelAccessToken);
                    var UserInfo = bot1.GetUserInfo(UserId);
                    var Address = ReceivedMessage.events[0].message.address;
                    var latitude = ReceivedMessage.events[0].message.latitude;
                    var longitude = ReceivedMessage.events[0].message.longitude;

                    string Message;
                    Message = "" + ReceivedMessage.events[0].type;
                    if (LineEvent.message.type == "location" || ReceivedMessage.events[0].message.text == "搜尋中...")
                    {
                        actions.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("http://www.wellseen.com.tw/") });
                        actions.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E6%83%9F%E6%96%B0%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x4b1a2fedbf9b04ff!8m2!3d25.083483!4d121.5516588?hl=zh-TW") });
                        var Column = new isRock.LineBot.Column
                        {
                            text = "距離2.3公里，開車前往約7分",
                            title = "惟新動物醫院",
                            thumbnailImageUrl = new Uri("https://6.share.photo.xuite.net/phibus/169386b/9792593/434439194_m.jpg"),
                            actions = actions
                        };
                        actions2.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("https://sites.google.com/site/cahvet/") });
                        actions2.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E5%8A%A0%E5%B7%9E%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x994d64cddf53704!8m2!3d25.0786487!4d121.5799052?hl=zh-TW") });
                        var Column2 = new isRock.LineBot.Column
                        {
                            text = "距離2.7公里，開車前往約8分",
                            title = "加州動物醫院",
                            thumbnailImageUrl = new Uri("https://s3-media3.fl.yelpcdn.com/bphoto/l3Dq4i27euT0gijwXKjvXg/ls.jpg"),
                            actions = actions2
                        };
                        actions3.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("http://www.petline.com.tw/petline/cgi/index_factory.cgi?t=petfactory_view&ID=11010&R23=1000") });
                        actions3.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E8%A5%BF%E6%B9%96%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x8868f530d4bda2ab!8m2!3d25.0822514!4d121.5688866?hl=zh-TW") });
                        var Column3 = new isRock.LineBot.Column
                        {
                            text = "距離1.5公里，開車前往約5分",
                            title = "西湖動物醫院",
                            thumbnailImageUrl = new Uri("https://www.3cu.com.tw/UploadFile/UserFiles/images/no_artist_p-b.gif"),
                            actions = actions3
                        };

                        var CarouseTemplate = new isRock.LineBot.CarouselTemplate();
                        CarouseTemplate.columns.Add(Column);
                        CarouseTemplate.columns.Add(Column2);
                        CarouseTemplate.columns.Add(Column3);
                        repmsg = $"";
                        isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                        bot.PushMessage(UserId, CarouseTemplate);

                        return Ok();
                    }






                    if (LineEvent.message.type == "text") //收到文字
                    {
                        //建立LuisClient
                        Microsoft.Cognitive.LUIS.LuisClient lc =
                          new Microsoft.Cognitive.LUIS.LuisClient(LuisAppId, LuisAppKey, true, Luisdomain);

                        //Call Luis API 查詢
                        var ret = lc.Predict(LineEvent.message.text).Result;
                        ///////////////////////////////
                        ///////////////////////////////
                        var Uri = "http://netinfo.takming.edu.tw/tip/";
                        var uri1 = "https://petbird.tw/article2979.html";
                        var uri2 = "https://kknews.cc/zh-tw/health/39a95vo.html";
                        var uri3 = "https://read01.com/zh-tw/az4K3x.html#.WyYrqVUzaUk";
                        var uri4 = "https://petbird.tw/article6910.html";
                        DateTime date1 = DateTime.Now;
                        if (ReceivedMessage.events[0].message.text == "詢問醫療" || ReceivedMessage.events[0].message.text == "醫療")
                        {
                            Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                            Medical.text = "其他相關醫療資訊";
                            Medical.title = "動物醫療";
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問疫苗", text = "疫苗" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問疾病", text = "疾病" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問晶片", text = "晶片" });
                            Medical.actions = actions;
                            isRock.LineBot.Bot bot2 = new isRock.LineBot.Bot(channelAccessToken);
                            repmsg = $"";
                            bot2.PushMessage(UserId, Medical);
                            return Ok();
                        }



                        if (ReceivedMessage.events[0].message.text == "research" || ReceivedMessage.events[0].message.text == "寵物受傷")
                        {
                            ConfirmTemplateMsg.text = "您的寵物是?";
                            actions.Add(new isRock.LineBot.MessageAction() { label = "狗", text = "dog" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "貓", text = "cat" });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        if (ReceivedMessage.events[0].message.text == "dog")
                        {
                            ConfirmTemplateMsg.text = "您的狗狗怎麼了?";
                            actions.Add(new isRock.LineBot.MessageAction() { label = "生病", text = "Dsick" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "受傷", text = "Dhurt" });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        else if (ReceivedMessage.events[0].message.text == "cat")
                        {
                            ConfirmTemplateMsg.text = "您的貓咪怎麼了?";
                            actions.Add(new isRock.LineBot.MessageAction() { label = "生病", text = "Csick" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "受傷", text = "Churt" });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        if (ReceivedMessage.events[0].message.text == "Dsick")
                        {
                            ConfirmTemplateMsg.text = "您的狗狗生病了";
                            actions.Add(new isRock.LineBot.UriAction() { label = "生病症狀", uri = new Uri("https://petbird.tw/article2974.html") });
                            actions.Add(new isRock.LineBot.UriAction() { label = "處理辦法", uri = new Uri("https://petbird.tw/article8971.html") });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        else if (ReceivedMessage.events[0].message.text == "Dhurt")
                        {
                            ConfirmTemplateMsg.text = "您的狗狗受傷了";
                            actions.Add(new isRock.LineBot.UriAction() { label = "緊急處理", uri = new Uri("https://petbird.tw/article11192.html") });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "附近醫院", text = "附近的動物醫院" });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        else if (ReceivedMessage.events[0].message.text == "Csick")
                        {
                            ConfirmTemplateMsg.text = "您的貓咪生病了";
                            actions.Add(new isRock.LineBot.UriAction() { label = "生病症狀", uri = new Uri("https://petbird.tw/article3359.html") });
                            actions.Add(new isRock.LineBot.UriAction() { label = "處理辦法", uri = new Uri("https://petbird.tw/article9058.html") });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }
                        else if (ReceivedMessage.events[0].message.text == "Churt")
                        {
                            ConfirmTemplateMsg.text = "您的貓咪受傷了";
                            actions.Add(new isRock.LineBot.UriAction() { label = "緊急處理", uri = new Uri("https://petbird.tw/article6910.html") });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "附近醫院", text = "附近的動物醫院" });
                            ConfirmTemplateMsg.actions = actions;
                            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                            bot.PushMessage(UserId, ConfirmTemplateMsg);
                            return Ok();
                        }





                        if (ret.TopScoringIntent.Name == "None")
                        {
                            Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                            Medical.text = "您可以透過以下來選擇您想詢問的資訊";
                            Medical.title = "動物醫生";
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問醫療", text = "詢問醫療" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問食品", text = "詢問食品" });
                            actions.Add(new isRock.LineBot.MessageAction() { label = "詢問飼料牌子", text = "推薦的牌子" });
                            Medical.actions = actions;
                            isRock.LineBot.Bot bot2 = new isRock.LineBot.Bot(channelAccessToken);
                            repmsg = $"你說了 '{LineEvent.message.text}' ，但不在我的服務範圍內喔!";
                            bot2.PushMessage(UserId, Medical);
                            return Ok();
                        }







                        else if (ret.TopScoringIntent.Name == "點餐")
                            repmsg = $"你想 '{LineEvent.message.text}',要的是 '{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}',這裡是我們的官網" + Uri;
                        else if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "尿尿")
                            repmsg = $"那可能是因為狗狗沒喝水";
                        else if (ret.TopScoringIntent.Name == "詢問時間")
                        {
                            repmsg = $"你想 '{LineEvent.message.text}',現在時間" + date1.ToString("yyyy/MM/dd,hh:mm:ss");
                            //ScriptRuntime pyRunTime = Python.CreateRuntime();
                            // dynamic obj = pyRunTime.UseFile("PythonApplication1.py");
                            // obj.movie();
                            // repmsg = $"{obj.movie}";
                        }














                        else if (ret.Intents.Count() >= 0 && ret.TopScoringIntent.Name == "詢問醫療")
                        {
                            if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "動物醫院" || ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "獸醫院" || ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "醫院")
                            {
                                actions.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("http://www.wellseen.com.tw/") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E6%83%9F%E6%96%B0%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x4b1a2fedbf9b04ff!8m2!3d25.083483!4d121.5516588?hl=zh-TW") });
                                var Column = new isRock.LineBot.Column
                                {
                                    text = "距離2.3公里，開車前往約7分",
                                    title = "惟新動物醫院",
                                    thumbnailImageUrl = new Uri("https://6.share.photo.xuite.net/phibus/169386b/9792593/434439194_m.jpg"),
                                    actions = actions
                                };
                                actions2.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("https://sites.google.com/site/cahvet/") });
                                actions2.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E5%8A%A0%E5%B7%9E%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x994d64cddf53704!8m2!3d25.0786487!4d121.5799052?hl=zh-TW") });
                                var Column2 = new isRock.LineBot.Column
                                {
                                    text = "距離2.7公里，開車前往約8分",
                                    title = "加州動物醫院",
                                    thumbnailImageUrl = new Uri("https://s3-media3.fl.yelpcdn.com/bphoto/l3Dq4i27euT0gijwXKjvXg/ls.jpg"),
                                    actions = actions2
                                };
                                actions3.Add(new isRock.LineBot.UriAction() { label = "查看官網", uri = new Uri("http://www.petline.com.tw/petline/cgi/index_factory.cgi?t=petfactory_view&ID=11010&R23=1000") });
                                actions3.Add(new isRock.LineBot.UriAction() { label = "Google Map導航", uri = new Uri("https://www.google.com.tw/maps/place/%E8%A5%BF%E6%B9%96%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x8868f530d4bda2ab!8m2!3d25.0822514!4d121.5688866?hl=zh-TW") });
                                var Column3 = new isRock.LineBot.Column
                                {
                                    text = "距離1.5公里，開車前往約5分",
                                    title = "西湖動物醫院",
                                    thumbnailImageUrl = new Uri("https://www.3cu.com.tw/UploadFile/UserFiles/images/no_artist_p-b.gif"),
                                    actions = actions3
                                };

                                var CarouseTemplate = new isRock.LineBot.CarouselTemplate();
                                CarouseTemplate.columns.Add(Column);
                                CarouseTemplate.columns.Add(Column2);
                                CarouseTemplate.columns.Add(Column3);
                                repmsg = $"";
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                bot.PushMessage(UserId, CarouseTemplate);
                                return Ok();
                            }
                            else if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "疫苗" || ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "預防針")
                            {
                                Medical.thumbnailImageUrl = new Uri("https://bpic.588ku.com/element_origin_min_pic/17/07/14/7a018b83b03dc478b662d36bd22e671f.jpg");
                                Medical.text = "相關動物疫苗的資訊";
                                Medical.title = "動物疫苗";
                                actions.Add(new isRock.LineBot.UriAction() { label = "認識動物疫苗及種類", uri = new Uri("http://aetasah.pixnet.net/blog/post/321991-%E7%96%AB%E8%8B%97-%E5%9F%BA%E6%9C%AC%E8%AA%8D%E8%AD%98") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "施打疫苗週期", uri = new Uri("http://blog.xuite.net/g5223086/twblog4/188516832-%E5%B9%AB%E7%8B%97%E7%8B%97%E6%B3%A8%E5%B0%84%E7%96%AB%E8%8B%97%E3%80%81%E9%A9%85%E8%9F%B2%E7%9A%84%E6%99%82%E5%88%BB%E8%88%87%E7%A8%AE%E9%A1%9E") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "施打疫苗相關資訊", uri = new Uri("https://petbird.tw/article6685.html") });
                                Medical.actions = actions;
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"您所{ret.TopScoringIntent.Name}而以上是查詢到的{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}資訊";
                                bot.PushMessage(UserId, Medical);
                            }
                            else if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "晶片")
                            {
                                Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                                Medical.text = "您附近所在的獸醫院";
                                Medical.title = "獸醫院";
                                actions.Add(new isRock.LineBot.UriAction() { label = "惟新動物醫院", uri = new Uri("https://www.google.com.tw/maps/place/%E6%83%9F%E6%96%B0%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x4b1a2fedbf9b04ff!8m2!3d25.083483!4d121.5516588?hl=zh-TW") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "加州動物醫院", uri = new Uri("https://www.google.com.tw/maps/place/%E5%8A%A0%E5%B7%9E%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x994d64cddf53704!8m2!3d25.0786487!4d121.5799052?hl=zh-TW") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "西湖動物醫院", uri = new Uri("https://www.google.com.tw/maps/place/%E8%A5%BF%E6%B9%96%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x8868f530d4bda2ab!8m2!3d25.0822514!4d121.5688866?hl=zh-TW") });
                                Medical.actions = actions;
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"您所{ret.TopScoringIntent.Name}而以上是查詢到附近的{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}";
                                bot.PushMessage(UserId, Medical);
                            }
                            else if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "疾病")
                            {
                                Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                                Medical.text = "有關寵物疾病的資訊";
                                Medical.title = "動物疾病";
                                actions.Add(new isRock.LineBot.UriAction() { label = "狗狗十大常見疾病", uri = new Uri("https://kknews.cc/zh-tw/health/qpz3ng.html") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "貓咪六大常見疾病", uri = new Uri("https://pet.talk.tw/article.aspx?Article_ID=20") });
                                actions.Add(new isRock.LineBot.MessageAction() { label = "查詢更多", text = "research" });
                                Medical.actions = actions;
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"您所{ret.TopScoringIntent.Name}而以上是查詢到的{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}";
                                bot.PushMessage(UserId, Medical);
                            }

                            else if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "晶片")
                            {
                                Medical.thumbnailImageUrl = new Uri("https://bpic.588ku.com/element_origin_min_pic/17/07/14/7a018b83b03dc478b662d36bd22e671f.jpg");
                                Medical.text = "相關動物晶片的資訊";
                                Medical.title = "動物晶片";
                                actions.Add(new isRock.LineBot.UriAction() { label = "認識動物晶片與最佳位置", uri = new Uri("http://ckx613.pixnet.net/blog/post/10319749-%E3%80%90%E8%BD%89%E8%BC%89%E3%80%91%E8%AA%8D%E8%AD%98%E5%AF%B5%E7%89%A9%E6%99%B6%E7%89%87%E5%8F%8A%E6%A4%8D%E5%85%A5%E7%9A%84%E6%9C%80%E4%BD%B3%E4%BD%8D%E7%BD%AE") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "動保處寵物登記與植入晶片入口網站", uri = new Uri("https://animal.coa.gov.tw/html/index_02_5.html") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "附近的動物醫院", uri = new Uri("https://www.google.com.tw/maps/place/%E8%A5%BF%E6%B9%96%E5%8B%95%E7%89%A9%E9%86%AB%E9%99%A2/@25.0865262,121.5567785,15z/data=!4m8!1m2!2m1!1z54246Yar6Zmi!3m4!1s0x0:0x8868f530d4bda2ab!8m2!3d25.0822514!4d121.5688866?hl=zh-TW") });
                                Medical.actions = actions;
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"您所{ret.TopScoringIntent.Name}而以上是查詢到的{ ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value}資訊";
                                bot.PushMessage(UserId, Medical);
                            }
                            else
                            {
                                Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                                Medical.text = "其他相關醫療資訊";
                                Medical.title = "動物醫療";
                                actions.Add(new isRock.LineBot.MessageAction() { label = "詢問疫苗", text = "疫苗" });
                                actions.Add(new isRock.LineBot.MessageAction() { label = "詢問疾病", text = "疾病" });
                                actions.Add(new isRock.LineBot.MessageAction() { label = "詢問晶片", text = "晶片" });
                                Medical.actions = actions;
                                isRock.LineBot.Bot bot2 = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"以上是有關醫療的資訊";
                                bot2.PushMessage(UserId, Medical);
                            }
                        }













                        else if (ret.Intents.Count() >= 0 && ret.TopScoringIntent.Name == "詢問狗狗")
                        {
                           if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "生病")
                            {
                                ConfirmTemplateMsg.text = "您的狗狗生病了";
                                actions.Add(new isRock.LineBot.UriAction() { label = "生病症狀", uri = new Uri("https://petbird.tw/article2974.html") });
                                actions.Add(new isRock.LineBot.UriAction() { label = "處理辦法", uri = new Uri("https://petbird.tw/article8971.html") });
                                ConfirmTemplateMsg.actions = actions;
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                bot.PushMessage(UserId, ConfirmTemplateMsg);
                                return Ok();
                            }
                            Medical.thumbnailImageUrl = new Uri("https://cdn-images-1.medium.com/max/2000/1*Vk4qnZdU-VkOlWiayzbIyQ.png");
                            Medical.text = "其他狗狗相關資訊";
                            Medical.title = "詢問狗狗";
                            actions2.Add(new isRock.LineBot.MessageAction() { label = "詢問牌子", text = "推薦的牌子" });
                            actions2.Add(new isRock.LineBot.MessageAction() { label = "詢問食品", text = "詢問食品" });
                            actions2.Add(new isRock.LineBot.MessageAction() { label = "詢問醫療", text = "詢問醫療" });
                            Medical.actions = actions2;
                            isRock.LineBot.Bot bot2 = new isRock.LineBot.Bot(channelAccessToken);
                            repmsg = $"以上是有關狗狗相關的資訊";
                            bot2.PushMessage(UserId, Medical);
                        }




















                        else if (ret.TopScoringIntent.Name == "詢問食品")
                        {
                            if (ret.Entities.FirstOrDefault().Value.FirstOrDefault().Value == "巧克力")
                            {
                               
                                isRock.LineBot.Bot bot = new isRock.LineBot.Bot(channelAccessToken);
                                repmsg = $"巧克力對狗來說是致命的毒藥，巧克力中毒的狗，在食用之後二到四小時，會有嘔吐和下痢的現象，狗狗也會顯現不安和活動增加的狀況，由於甲基黃漂吟有利尿作用，所以狗會有頻尿現象，都可作為狗主人研判的指標。 嚴重者，會在食用後的十二到三十六小時內死亡。如果狗狗不是一次食用巧克力過量中毒，而是持續幾天食用，則有可能死於心臟衰竭。";
                                Uri dogcanteat = new Uri("https://maoup.com.tw/wp-content/uploads/2018/02/180118_1.png");
                                bot.PushMessage(UserId, dogcanteat);
                            }
                            //repmsg = $"尚未更新";
                        }



















                        else if (ret.TopScoringIntent.Name == "詢問牌子")
                        {
                            isRock.LineBot.Bot bot =
                                new isRock.LineBot.Bot(ChannelAccessToken);  //傳入Channel access token
                            var ImageCarouselColumn1 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://img1.momoshop.com.tw/goodsimg/0004/272/609/4272609_B.jpg?t=1507335911"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "愛肯拿", text = "愛肯拿" }
                            };
                            var ImageCarouselColumn2 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://a.ecimg.tw/items/DEBV6RA90078EG9/000001_1478528276.jpg"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "法國皇家", text = "法國皇家" }
                            };
                            var ImageCarouselColumn3 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://a.ecimg.tw/items/DEBV6RA90078EG9/000001_1478528276.jpg"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "法國皇家", text = "法國皇家" }
                            };
                            var ImageCarouselColumn4 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://a.ecimg.tw/items/DEBV6RA90078EG9/000001_1478528276.jpg"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "法國皇家", text = "法國皇家" }
                            };
                            var ImageCarouselColumn5 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://a.ecimg.tw/items/DEBV6RA90078EG9/000001_1478528276.jpg"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "法國皇家", text = "法國皇家" }
                            };
                            var ImageCarouselColumn6 = new isRock.LineBot.ImageCarouselColumn
                            {
                                //設定圖片
                                imageUrl = new Uri("https://a.ecimg.tw/items/DEBV6RA90078EG9/000001_1478528276.jpg"),
                                //設定回覆動作
                                action = new isRock.LineBot.MessageAction() { label = "法國皇家", text = "法國皇家" }
                            };
                            var ImageCarouselTemplate = new isRock.LineBot.ImageCarouselTemplate();

                            //這是範例，所以用一組樣板建立三個
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn1);
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn2);
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn3);
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn4);
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn5);
                            ImageCarouselTemplate.columns.Add(ImageCarouselColumn6);
                            //發送 CarouselTemplate
                            bot.PushMessage(AdminUserId, ImageCarouselTemplate);
                            repmsg = $"以上是部分狗糧的牌子";
                        }

















                        //回覆
                        this.ReplyMessage(LineEvent.replyToken, repmsg);
                    }

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
