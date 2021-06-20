namespace CryptoBot.Helper
{
    public class Alert
    {
        public ulong Id { get; init; }
        
        public string Currency { get; init; }
        
        public double Price { get; init; }
        
        public bool CheckDown { get; init; }
    }
}