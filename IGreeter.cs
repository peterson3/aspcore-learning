namespace testDotNetCOre
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {
        public string GetMessageOfTheDay()
        {
            return "Olá! Bom Domingo!";
        }
    }
}