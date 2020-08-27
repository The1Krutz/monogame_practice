using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mg_pong {
  public class BouncyBoi {
    public Vector2 Velocity;
    public Texture2D Sprite;
    public Rectangle BoundingBox; // CRITICAL: these coordinates are int pixel values, and won't change at all if the update is less than one pixel.
    public readonly float MaxSpeed; // max speed in pixels per second

    public readonly Keys upKey;
    public readonly Keys downKey;

    private void LogInfo(string additionalInfo = null) {
      System.Console.WriteLine($"Bounding box: {BoundingBox.ToString()}\nVelocity: {Velocity}");
      if (additionalInfo != null) {
        System.Console.WriteLine(additionalInfo);
      }
    }

    public BouncyBoi(Vector2 velocity, Texture2D sprite, Rectangle boundingbox, Keys upkey = Keys.None, Keys downkey = Keys.None) {
      Velocity = velocity;
      Sprite = sprite;
      BoundingBox = boundingbox;
      upKey = upkey;
      downKey = downkey;

      MaxSpeed = 400;
    }

    public void Update(KeyboardState keyMap, GameTime gameTime) {
      HandleInput(keyMap);

      if (Velocity.Length() > MaxSpeed) {
        Velocity.Normalize();
        Velocity *= MaxSpeed;
        LogInfo("Speed Limit!");
      }

      BoundingBox.Offset(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
      //LogInfo();
    }

    public void BounceUp() {
      Velocity = Vector2.Reflect(Velocity, new Vector2(0, -1));
    }

    public void BounceDown() {
      Velocity = Vector2.Reflect(Velocity, new Vector2(0, 1));
    }

    public void BounceLeft() {
      Velocity = Vector2.Reflect(Velocity, new Vector2(-1, 0));
    }

    public void BounceRight() {
      Velocity = Vector2.Reflect(Velocity, new Vector2(1, 0));
    }

    private void HandleInput(KeyboardState keyMap) {
      if (upKey == Keys.None || downKey == Keys.None) {
        return;
      }

      Vector2 impulse;
      if (keyMap.IsKeyDown(upKey)) {
        impulse = new Vector2(0, MaxSpeed * -1);
      } else if (keyMap.IsKeyDown(downKey)) {
        impulse = new Vector2(0, MaxSpeed);
      } else {
        impulse = Vector2.Zero;
      }

      Velocity = Vector2.Lerp(Velocity, impulse, .1f);

    }
  }
}
