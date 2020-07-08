using System;

namespace Demo3 {
  public static class Program {
    [STAThread]
    static void Main() {

      // keyboard input example
      //using (var game = new Game1())
      //game.Run();

      // mouse input example
      //using (var game = new Game2())
      //game.Run();

      // gamepad input example
      using (var game = new Game3())
        game.Run();
    }
  }
}
