﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo1 {
  public class MainGame : Game {
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;

    public MainGame() {
      _graphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void UnloadContent() {
      _spriteBatch.Dispose();
    }

    protected override void Update(GameTime gameTime) {
      var keyboardState = Keyboard.GetState();

      if (keyboardState.IsKeyDown(Keys.Escape))
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.DarkGray);

      _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
