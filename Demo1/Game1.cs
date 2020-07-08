using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo1 {
  public class MainGame : Game {
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;

    private Texture2D texture;
    private Vector2 position;

    private readonly float boxSpeed = 200.0f;

    public MainGame() {
      _graphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      position = new Vector2(0, 0);

      IsFixedTimeStep = false;
      _graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
      TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 33); // 33ms = 30fps
    }

    protected override void Initialize() {
      texture = new Texture2D(GraphicsDevice, 100, 100);
      Color[] colorData = new Color[100 * 100];
      for (int i = 0; i < colorData.Length; i++) {
        colorData[i] = Color.Red;
      }

      texture.SetData(colorData);

      base.Initialize();
    }

    protected override void OnActivated(object sender, EventArgs args) {
      Window.Title = "Active Application";
      base.OnActivated(sender, args);
    }

    protected override void OnDeactivated(object sender, EventArgs args) {
      Window.Title = "Inactive Application";
      base.OnDeactivated(sender, args);
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void UnloadContent() {
      _spriteBatch.Dispose();
    }

    protected override void Update(GameTime gameTime) {
      if (!IsActive) {
        return;
      }

      var keyboardState = Keyboard.GetState();
      if (keyboardState.IsKeyDown(Keys.Escape)) {
        Exit();
      }

      position.X += boxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      if (position.X > GraphicsDevice.Viewport.Width) {
        position.X = 0;
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
      _spriteBatch.Draw(texture, position, Color.Red);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
