using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace nez_pong {
    public class Game1 : Core {
        public Game1() : base(width: 1280,
                              height: 720,
                              isFullScreen: false) {
        }

        protected override void Initialize() {
            base.Initialize();

            Window.AllowUserResizing = true;
            Scene = new PongScene();
        }
    }
}