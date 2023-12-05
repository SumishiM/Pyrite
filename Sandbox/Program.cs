using Microsoft.Xna.Framework;

namespace Sandbox
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using Game game = new Pyrite.Game(1280, 720, 1280, 720, "Pyrite Debug", false);
                game.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
