using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace nez_pong {
    public class PongScene : Scene {

        private readonly int _sceneWidth = 1280;
        private readonly int _sceneHeight = 720;

        public override void Initialize() {
            base.Initialize();

            SetDesignResolution(_sceneWidth, _sceneHeight, SceneResolutionPolicy.None);
            Screen.SetSize(_sceneWidth, _sceneHeight);

            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");

            // paddle1
            CreateEntity(new Vector2(50, 150), .5f, 0, new Vector2(0, 0), paddleTexture);
            // paddle2
            CreateEntity(new Vector2(_sceneWidth - 60, 150), .5f, 0, new Vector2(0, 0), paddleTexture);
            // ball
            CreateEntity(new Vector2(_sceneWidth / 2, _sceneHeight / 2), 0, 1, new Vector2(300, 300), ballTexture);

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

        ArcadeRigidbody CreateEntity(Vector2 position, float friction, float elasticity, Vector2 velocity, Texture2D texture) {
            ArcadeRigidbody rigidbody = new ArcadeRigidbody() {
                Friction = friction,
                Elasticity = elasticity,
                Velocity = velocity,
                ShouldUseGravity = false
            };

            var entity = CreateEntity(Utils.RandomString(3));
            entity.Position = position;
            entity.AddComponent(new SpriteRenderer(texture));
            entity.AddComponent(rigidbody);
            entity.AddComponent<BoxCollider>();


            return rigidbody;
        }


    }


}