using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace mg_pong {
  public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _kenneyBold;

    private Texture2D _headerBar;
    private Texture2D _paddle;
    private Texture2D _ball;

    private readonly int _ballSide = 15;
    private readonly int _paddleWidth = 5;
    private readonly int _paddleHeight = 75;

    private Rectangle playableArea;

    private BouncyBoi leftPaddle;
    private BouncyBoi rightPaddle;
    private BouncyBoi ball;

    private int leftScore;
    private int rightScore;

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

      // header bar texture
      _headerBar = new Texture2D(GraphicsDevice, playableArea.Width, 50);
      Color[] headerColor = new Color[playableArea.Width * 50];
      Array.Fill(headerColor, Color.Red);
      _headerBar.SetData(headerColor);

      // paddle texture
      _paddle = new Texture2D(GraphicsDevice, _paddleWidth, _paddleHeight);
      Color[] paddleColor = new Color[_paddleWidth * _paddleHeight];
      Array.Fill(paddleColor, Color.White);
      _paddle.SetData(paddleColor);

      // ball texture
      _ball = new Texture2D(GraphicsDevice, _ballSide, _ballSide);
      Color[] ballColor = new Color[_ballSide * _ballSide];
      Array.Fill(ballColor, Color.White);
      _ball.SetData(ballColor);

      // add paddles and ball
      leftPaddle = new BouncyBoi(Vector2.Zero, _paddle, new Rectangle(playableArea.Left + 50, playableArea.Top + 50, _paddleWidth, _paddleHeight), Keys.W, Keys.S);
      rightPaddle = new BouncyBoi(Vector2.Zero, _paddle, new Rectangle(playableArea.Right - 60, playableArea.Top + 50, _paddleWidth, _paddleHeight), Keys.Up, Keys.Down);
      ball = new BouncyBoi(new Vector2(1000), _ball, new Rectangle(400, 300, _ballSide, _ballSide));

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

      // update positions
      leftPaddle.Update(state, gameTime);
      rightPaddle.Update(state, gameTime);
      ball.Update(state, gameTime);

      // keep the ball in bounds
      if (ball.BoundingBox.Bottom >= playableArea.Bottom) {
        ball.BounceUp();
      }
      if (ball.BoundingBox.Top <= playableArea.Top) {
        ball.BounceDown();
      }
      if (ball.BoundingBox.Left <= playableArea.Left) {
        rightScore++;
        ball.BounceRight();
        ResetBall();
      }
      if (ball.BoundingBox.Right >= playableArea.Right) {
        leftScore++;
        ball.BounceLeft();
        ResetBall();
      }

      // handle bouncing it off paddles
      if (ball.BoundingBox.Intersects(rightPaddle.BoundingBox)) {
        ball.BounceLeft();
      }
      if (ball.BoundingBox.Intersects(leftPaddle.BoundingBox)) {
        ball.BounceRight();
      }

      base.Update(gameTime);
    }

    private void ResetBall() {
      ball.BoundingBox.Location = new Point(400, 300);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      _spriteBatch.Begin();
      _spriteBatch.Draw(_headerBar, new Rectangle(Point.Zero, new Point(800, 50)), Color.Gray);
      _spriteBatch.DrawString(_kenneyBold, $"{leftScore}  {rightScore}", new Vector2(400, 15), Color.Black);
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
