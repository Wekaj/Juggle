using Microsoft.Xna.Framework.Graphics;
using System;

namespace Juggle.Objects {
    public class Background {
        private Random random = new Random();
        public Texture2D texture;
        public float x, y;
        public float scrollSpeed;

        public Background(Texture2D texture, bool start) {
            this.texture = texture;
            scrollSpeed = Level.scrollSpeed / 4;
            if (start) {
                x = 0;
                y = 0;
            }
            else {
                x = Game1.screenWidth + this.texture.Width + random.Next(texture.Width);
                y = Game1.screenHeight - this.texture.Height + random.Next(this.texture.Height / 2);
            }
        }

        public void Update(Level level) {
            x -= scrollSpeed;
            if (x + texture.Width < 0)
                level.Remove(this, ObjectType.background);
        }
    }
}
