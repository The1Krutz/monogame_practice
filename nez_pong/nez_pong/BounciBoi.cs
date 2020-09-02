using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace nez_pong {
    public class PongScene : Scene {

        public override void Initialize() {
            base.Initialize();

            SetDesignResolution(1280, 720, SceneResolutionPolicy.None);
            Screen.SetSize(1280, 720);

            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");

            float friction = 0.3f;
            float elasticity = 0.4f;

            // paddle1
            CreateEntity(new Vector2(50, 50), 15f, friction, elasticity, new Vector2(0, 0), paddleTexture);
            // paddle2
            CreateEntity(new Vector2(1220, 50), 15f, friction, elasticity, new Vector2(0, 0), paddleTexture);
            // ball
            CreateEntity(new Vector2(1280 / 2, 720 / 2), 15f, friction, elasticity, new Vector2(0, 0), ballTexture)
                .AddImpulse(new Vector2(10, 10));


        }

        ArcadeRigidbody CreateEntity(Vector2 position, float mass, float friction, float elasticity, Vector2 velocity, Texture2D texture) {
            ArcadeRigidbody rigidbody = new ArcadeRigidbody()
                .SetMass(mass)
                .SetFriction(friction)
                .SetElasticity(elasticity)
                .SetVelocity(velocity);

            var entity = CreateEntity(Utils.RandomString(3));
            entity.Position = position;
            entity.AddComponent(new SpriteRenderer(texture));
            entity.AddComponent(rigidbody);
            entity.AddComponent<BoxCollider>();


            return rigidbody;
        }


    }


}