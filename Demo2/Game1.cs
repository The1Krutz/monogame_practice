using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo2 {
  public class Game1 : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D potatoTexture;
    Texture2D spriteTexture;

    public Game1() {
      graphics = new GraphicsDeviceManager(this) {
        PreferredBackBufferWidth = 500,
        PreferredBackBufferHeight = 500
      };
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      potatoTexture = Content.Load<Texture2D>("potato");
      spriteTexture = Content.Load<Texture2D>("sprite_haha");
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
      spriteBatch.Draw(spriteTexture, Vector2.Zero, layerDepth: 1.0f, color: Color.White);
      spriteBatch.Draw(spriteTexture, new Vector2(100, 0), Color.White);
      spriteBatch.Draw(potatoTexture, destinationRectangle: new Rectangle(0, 0, 500, 500), layerDepth: 0.0f, color: Color.White);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
