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

        public override void Initialize() {
            base.Initialize();

            SetDesignResolution(_sceneWidth, _sceneHeight, SceneResolutionPolicy.None);
            Screen.SetSize(_sceneWidth, _sceneHeight);

            Texture2D ballTexture = Content.Load<Texture2D>("ball");

            // new left paddle
            CreateEntity("leftPaddle", new Vector2(50, _sceneHeight / 2))
                .AddComponent(new Paddle(PlayerIndex.One))
                .AddComponent(new BoxCollider());

            // new right paddle
            CreateEntity("rightPaddle", new Vector2(_sceneWidth - 50, _sceneHeight / 2))
                .AddComponent(new Paddle(PlayerIndex.Two))
                .AddComponent(new BoxCollider());

            // ball
            CreateEntity("ball", new Vector2(_sceneWidth / 2, _sceneHeight / 2))
                .AddComponent(new SpriteRenderer(ballTexture))
                .AddComponent(new ArcadeRigidbody() {
                    Mass = 10f,
                    Friction = 0f,
                    Elasticity = 1,
                    Velocity = new Vector2(300, 300),
                    ShouldUseGravity = false
                })
                .AddComponent<BoxCollider>();

            // top and bottom bouncy walls
            CreateEntity("bottomwall")
                .AddComponent(new BoxCollider(0, _sceneHeight, _sceneWidth, 10f));
            CreateEntity("topwall")
                .AddComponent(new BoxCollider(0, -10, _sceneWidth, 10f));

            // left and right point zones
            CreateEntity("rightwall")
                .AddComponent(new BoxCollider(_sceneWidth, 0, 10f, _sceneHeight));
            CreateEntity("leftwall")
                .AddComponent(new BoxCollider(-10f, 0, 10f, _sceneHeight));

        }
    }

    public class Paddle : Component, IUpdatable {
        public float MoveSpeed = 400;

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

            _mover = Entity.AddComponent(new Mover());

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
            } else {
                throw new NotImplementedException();
            }
        }

        void IUpdatable.Update() {
            _velocity.Y = _yAxisInput.Value * MoveSpeed * Time.DeltaTime;
            _mover.CalculateMovement(ref _velocity, out var res);
            _mover.ApplyMovement(_velocity);
        }
    }
}