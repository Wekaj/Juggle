using System;

namespace Juggle.Objects {
    public class Coin {
        Random random = new Random();
        Level level;
        public float x, y;
        public float speed;

        public Coin(Level level) {
            this.level = level;
            speed = Level.scrollSpeed;
            x = Game1.screenWidth + Game1.coinTexture.Width;
            y = (float)random.Next(Game1.screenHeight - Game1.coinTexture.Height);
        }

        public void Update() {
            x -= speed;

            if (x < -Game1.coinTexture.Width)
                level.Remove(this, ObjectType.coin);
        }
    }
}
