using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/**
 * Example for mouse input 
 */
namespace Demo3 {
  public class Game2 : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Vector2 position;
    Texture2D texture;

    public Game2() {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      base.Initialize();
      position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 64, graphics.GraphicsDevice.Viewport.Height / 2 - 64);
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      texture = Content.Load<Texture2D>("potato");
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
    }

    protected override void Update(GameTime gameTime) {
      MouseState state = Mouse.GetState();

      // update sprite location to match cursor location
      position.X = state.X;
      position.Y = state.Y;

      System.Console.WriteLine($"{position.X.ToString()}, {position.Y.ToString()}");

      // exit if right mouse button clicked
      if (state.RightButton == ButtonState.Pressed) {
        Exit();
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      spriteBatch.Draw(texture, position, Color.White);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
