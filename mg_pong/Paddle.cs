using Microsoft.Xna.Framework;


namespace mg_pong {
  public class Paddle {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    private readonly double _maxSpeed;

    public Paddle(Vector2 position, Vector2 velocity) {
      Position = position;
      Velocity = velocity;

      _maxSpeed = 5;
    }

    public void UpdatePosition() {
      Position += Velocity;

    }

  }
}
