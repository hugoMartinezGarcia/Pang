using System;

namespace Pang
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GestorDePantallas())
                game.Run();
        }
    }
}
