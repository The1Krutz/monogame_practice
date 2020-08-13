using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Demo4 {
  public class Game2 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<SoundEffect> soundEffects;

    public Game2() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      soundEffects = new List<SoundEffect>();
    }

    protected override void Initialize() {
      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      soundEffects.Add(Content.Load<SoundEffect>("button-1"));
      soundEffects.Add(Content.Load<SoundEffect>("button-2"));
      soundEffects.Add(Content.Load<SoundEffect>("button-3"));
      soundEffects.Add(Content.Load<SoundEffect>("button-4"));

      // fire and forget play
      soundEffects[0].Play();

      // play a sound that can be manipulated afterwards
      var instance = soundEffects[0].CreateInstance();
      //instance.IsLooped = true;
      instance.Play();
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
    }


    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        soundEffects[0].CreateInstance().Play();
      if (Keyboard.GetState().IsKeyDown(Keys.S))
        soundEffects[1].CreateInstance().Play();
      if (Keyboard.GetState().IsKeyDown(Keys.D))
        soundEffects[2].CreateInstance().Play();
      if (Keyboard.GetState().IsKeyDown(Keys.F))
        soundEffects[3].CreateInstance().Play();

      if (Keyboard.GetState().IsKeyDown(Keys.Up))
        SoundEffect.MasterVolume = MathHelper.Min(SoundEffect.MasterVolume + 0.1f, 1.0f);
      if (Keyboard.GetState().IsKeyDown(Keys.Down))
        SoundEffect.MasterVolume = MathHelper.Max(SoundEffect.MasterVolume - 0.1f, 0.0f);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);
      base.Draw(gameTime);
    }
  }
}
