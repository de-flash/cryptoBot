using System;
using System.Net;
using System.Web;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CryptoBot.BotControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CryptoBot
{
    class Program
    {
        public static void Main()
        {
            Bot.MainAsync().GetAwaiter().GetResult();
        }
    }
}