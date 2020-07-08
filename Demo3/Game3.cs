using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/**
 * Example for gamepad input 
 */
namespace Demo3 {
  public class Game3 : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Vector2 position;
    Texture2D texture;

    public Game3() {
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
      // exit if escape is pressed
      if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        Exit();
      }

      // check device for P1
      GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

      // if there is a controller connected, handle it
      if (capabilities.IsConnected) {
        // get state of controller 1
        GamePadState state = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

        // explicitly check if gamepad has features you're looking for
        if (capabilities.HasLeftXThumbStick) {
          // original tutorial code
          //// check X-axis of left analog stick
          //if (state.ThumbSticks.Left.X < -.5f) {
          //  position.X -= 10.0f;
          //}
          //if (state.ThumbSticks.Left.X > .5f) {
          //  position.X += 10.0f;
          //}

          // my version
          position.X += state.ThumbSticks.Left.X * 10.0f;
          position.Y -= state.ThumbSticks.Left.Y * 10.0f;
        }

        if (capabilities.GamePadType == GamePadType.GamePad) {
          if (state.IsButtonDown(Buttons.A)) {
            Exit();
          }
        }
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
