using Pyrite;

namespace ODEs
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                using Game game = new(new ODEsGame());
                game.Run();
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}