using System.Collections.Generic;
using OpenQA.Selenium;

namespace CryptoBot.Helper
{
    public static class Globals
    {
        public static List<Alert> AlertList { get; set; } = new List<Alert>();

        public static string CurrencyIds { get; set; } = "";
        
    }
}