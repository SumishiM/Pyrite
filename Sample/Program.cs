using Pyrite;

namespace Sample
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                using Game game = new(new SampleGame());
                game.Run();
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}