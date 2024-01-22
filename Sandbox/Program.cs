using Pyrite;

namespace Sandbox
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using Game game = new SandboxGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    internal class SandboxGame : Game, IPyriteGame
    {
        public string Name => "Sandbox";
    }
}
