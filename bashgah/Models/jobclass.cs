using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Telegram.Bot;
namespace bashgah.Models
{
    public class jobclass:IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var bot = new TelegramBotClient("155807990:AAFzda1vESOcvsoExqh3KoO8ganjct7uBIM");
            var s = await bot.SendTextMessageAsync("@alipc2020", "im sending pm every 10 seconds");

        }
    }
}
