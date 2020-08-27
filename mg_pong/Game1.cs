using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace mg_pong {
  public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _kenneyBold;
    private Texture2D _headerBar;

    private Rectangle playableArea;

    private BouncyBoi leftPaddle;
    private BouncyBoi rightPaddle;
    private BouncyBoi ball;

    public Game1() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      playableArea = new Rectangle(0, 50, 800, 550);
    }

    protected override void Initialize() {
      // set window size
      _graphics.PreferredBackBufferHeight = playableArea.Height + 50;
      _graphics.PreferredBackBufferWidth = playableArea.Width;
      _graphics.ApplyChanges();
      Window.AllowUserResizing = true;

      // header bar
      _headerBar = new Texture2D(GraphicsDevice, playableArea.Width, 50);
      Color[] colorData = new Color[playableArea.Width * 50];
      System.Array.Fill(colorData, Color.Red);
      _headerBar.SetData(colorData);

      // add paddles and ball
      leftPaddle = new BouncyBoi(Vector2.Zero, Content.Load<Texture2D>("minus"), new Rectangle(playableArea.Left + 50, playableArea.Top + 50, 10, 100), Keys.W, Keys.S);
      rightPaddle = new BouncyBoi(Vector2.Zero, Content.Load<Texture2D>("minus"), new Rectangle(playableArea.Right - 60, playableArea.Top + 50, 10, 100), Keys.Up, Keys.Down);
      ball = new BouncyBoi(new Vector2(1000), Content.Load<Texture2D>("stop"), new Rectangle(200, 200, 30, 30));

      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      _kenneyBold = Content.Load<SpriteFont>("KenneyBoldTTF");
    }

    protected override void Update(GameTime gameTime) {
      KeyboardState state = Keyboard.GetState();

      if (state.IsKeyDown(Keys.Escape))
        Exit();


      leftPaddle.Update(state, gameTime);
      rightPaddle.Update(state, gameTime);
      ball.Update(state, gameTime);

      if (ball.BoundingBox.Bottom >= playableArea.Bottom) {
        ball.BounceUp();
      }
      if (ball.BoundingBox.Top <= playableArea.Top) {
        ball.BounceDown();
      }
      if (ball.BoundingBox.Right >= rightPaddle.BoundingBox.Left) {
        ball.BounceLeft();
      }
      if (ball.BoundingBox.Left <= leftPaddle.BoundingBox.Right) {
        ball.BounceRight();
      }


      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      _spriteBatch.Begin();
      _spriteBatch.Draw(_headerBar, new Rectangle(Point.Zero, new Point(800, 50)), Color.Gray);
      _spriteBatch.DrawString(_kenneyBold, "Header Bar", new Vector2(50, 15), Color.Black);
      _spriteBatch.Draw(leftPaddle.Sprite, leftPaddle.BoundingBox, Color.White);
      _spriteBatch.Draw(rightPaddle.Sprite, rightPaddle.BoundingBox, Color.White);
      _spriteBatch.Draw(ball.Sprite, ball.BoundingBox, Color.White);
      _spriteBatch.End();

      base.Draw(gameTime);
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
    }
  }
}
