using Pyrite;

namespace Sandbox
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            //try
            {
                using Game game = new(new SandboxGame());
                game.Run();
            }
            //catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
