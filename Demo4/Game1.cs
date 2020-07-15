using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Demo4 {
  public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Song song;

    public Game1() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      song = Content.Load<Song>("sample_mp3");
      MediaPlayer.Play(song);
      // uncomment the following line to loop the song
      //MediaPlayer.IsRepeating = true;
      MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
    }

    void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e) {
      // 0.0f is silent, 0.1f is full volume
      MediaPlayer.Volume -= 0.1f;
      MediaPlayer.Play(song);
    }


    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);
      base.Draw(gameTime);
    }
  }
}
