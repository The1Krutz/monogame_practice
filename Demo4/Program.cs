﻿using System;

namespace Demo4 {
  public static class Program {
    [STAThread]
    static void Main() {
      //using (var game = new Game1())
      using (var game = new Game2())
        game.Run();
    }
  }
}
