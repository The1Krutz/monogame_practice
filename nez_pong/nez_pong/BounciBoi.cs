using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez;
using System;
using Microsoft.Xna.Framework.Input;

namespace nez_pong {
    public class PongScene : Scene {
        private readonly int _sceneWidth = 1280;
        private readonly int _sceneHeight = 720;

        private int _leftScore;
        private int _rightScore;

        private Entity _ball;
        private Entity _leftPaddle;
        private Entity _rightPaddle;
        private Entity _leftWall;
        private Entity _rightWall;
        private TextComponent _scoreboard;


        public override void Initialize() {
            base.Initialize();

            ClearColor = Color.Black;
            SetDesignResolution(_sceneWidth, _sceneHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(_sceneWidth, _sceneHeight);

            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            Texture2D headerTexture = Content.Load<Texture2D>("header");

            // new left paddle
            _leftPaddle = CreateEntity("leftPaddle", new Vector2(50, _sceneHeight / 2));
            _leftPaddle.AddComponent(new Paddle(PlayerIndex.One));
            _leftPaddle.AddComponent<BoxCollider>();

            // new right paddle
            _rightPaddle = CreateEntity("rightPaddle", new Vector2(_sceneWidth - 50, _sceneHeight / 2));
            _rightPaddle.AddComponent(new Paddle(PlayerIndex.Two));
            _rightPaddle.AddComponent<BoxCollider>();

            // ball
            _ball = CreateEntity("ball", new Vector2(_sceneWidth / 2, _sceneHeight / 2));
            _ball.AddComponent(new SpriteRenderer(ballTexture));
            _ball.AddComponent(new ArcadeRigidbody() {
                Mass = 10f,
                Friction = 0f,
                Elasticity = 1,
                Velocity = new Vector2(300, 300),
                ShouldUseGravity = false
            });
            _ball.AddComponent<BoxCollider>();

            // top and bottom bouncy walls
            var bottomwall = CreateEntity("bottomwall");
            bottomwall.AddComponent(new BoxCollider(0, _sceneHeight, _sceneWidth, 10f));

            _scoreboard = new TextComponent(Graphics.Instance.BitmapFont,
                                            "0 : 0",
                                            new Vector2(0, -5),
                                            Color.White);
            var topwall = CreateEntity("topwall", new Vector2(_sceneWidth / 2, 27));
            topwall.AddComponent(new SpriteRenderer(headerTexture));
            topwall.AddComponent<BoxCollider>();
            topwall.AddComponent(_scoreboard);

            // left and right point zones
            _rightWall = CreateEntity("rightwall");
            _rightWall.AddComponent(new BoxCollider(_sceneWidth, 0, 10f, _sceneHeight));

            _leftWall = CreateEntity("leftwall");
            _leftWall.AddComponent(new BoxCollider(-10f, 0, 10f, _sceneHeight));

            // put all the normal bouncy stuff in layer 1
            Flags.SetFlagExclusive(ref topwall.GetComponent<BoxCollider>().PhysicsLayer, 1);
            Flags.SetFlagExclusive(ref bottomwall.GetComponent<BoxCollider>().PhysicsLayer, 1);
            Flags.SetFlagExclusive(ref _ball.GetComponent<BoxCollider>().PhysicsLayer, 1);
            Flags.SetFlagExclusive(ref _leftPaddle.GetComponent<BoxCollider>().PhysicsLayer, 1);
            Flags.SetFlagExclusive(ref _rightPaddle.GetComponent<BoxCollider>().PhysicsLayer, 1);

            // put the score zones in layer 2
            Flags.SetFlagExclusive(ref _leftWall.GetComponent<BoxCollider>().PhysicsLayer, 2);
            Flags.SetFlagExclusive(ref _rightWall.GetComponent<BoxCollider>().PhysicsLayer, 2);

            // make the ball only interact with physics layer 1, the bouncy things
            Flags.SetFlagExclusive(ref _ball.GetComponent<BoxCollider>().CollidesWithLayers, 1);
        }

        public override void Update() {
            var ballCollider = _ball.GetComponent<BoxCollider>();
            var rightWallCollider = _rightWall.GetComponent<BoxCollider>();
            var leftWallCollider = _leftWall.GetComponent<BoxCollider>();
            var updateScore = false;

            if (ballCollider.CollidesWith(rightWallCollider, out _)) {
                _ball.Position = new Vector2(_sceneWidth / 2, _sceneHeight / 2);
                _leftScore++;
                updateScore = true;
            }
            if (ballCollider.CollidesWith(leftWallCollider, out _)) {
                _ball.Position = new Vector2(_sceneWidth / 2, _sceneHeight / 2);
                _rightScore++;
                updateScore = true;
            }

            if (updateScore) {
                Console.WriteLine($"{_leftScore} : {_rightScore}");
                _scoreboard.SetText($"{_leftScore} : {_rightScore}");
            }

            base.Update();
        }
    }

    public class Paddle : Component, IUpdatable {
        public float MoveSpeed { get; set; } = 400;

        private readonly PlayerIndex _player;

        private Mover _mover;
        private Vector2 _velocity;
        private VirtualIntegerAxis _yAxisInput;

        public Paddle(PlayerIndex player) {
            _player = player;
        }

        public override void OnAddedToEntity() {
            var texture = Entity.Scene.Content.Load<Texture2D>("paddle");
            Entity.AddComponent(new SpriteRenderer(texture));

            _mover = Entity.AddComponent<Mover>();

            SetupInput();
        }

        public override void OnRemovedFromEntity() {
            _yAxisInput.Deregister();
        }

        private void SetupInput() {
            _yAxisInput = new VirtualIntegerAxis();
            if (_player == PlayerIndex.One) {
                _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));
            } else if (_player == PlayerIndex.Two) {
                _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
            }
        }

        void IUpdatable.Update() {
            _velocity.Y = _yAxisInput.Value * MoveSpeed * Time.DeltaTime;
            _mover.CalculateMovement(ref _velocity, out _);
            _mover.ApplyMovement(_velocity);
        }
    }
}