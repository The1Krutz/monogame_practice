using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

/**
 * Example for keyboard input 
 */
namespace Demo3 {
  public class Game1 : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Vector2 position;
    Texture2D texture;
    KeyboardState previousState;

    public Game1() {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      base.Initialize();
      position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 64, graphics.GraphicsDevice.Viewport.Height / 2 - 64);
      previousState = Keyboard.GetState();
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
      // poll for current keyboard state
      KeyboardState state = Keyboard.GetState();

      // exit if they hit escape
      if (state.IsKeyDown(Keys.Escape)) {
        Exit();
      }

      StringBuilder sb = new StringBuilder();
      sb.Append("Keys pressed: ");

      if (state.GetPressedKeys().Length == 0) {
        sb.Append("none");
      } else {
        foreach (var key in state.GetPressedKeys()) {
          sb.Append(key);
        }
      }

      System.Console.WriteLine(sb);

      if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right)) {
        position.X += 10;
      }
      if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left)) {
        position.X -= 10;
      }
      if (state.IsKeyDown(Keys.Up)) {
        position.Y -= 10;
      }
      if (state.IsKeyDown(Keys.Down)) {
        position.Y += 10;
      }

      base.Update(gameTime);
      previousState = state;
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
