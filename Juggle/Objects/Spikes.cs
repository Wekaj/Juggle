using System;

namespace Juggle.Objects {
    public class Spikes {
        Random random = new Random();
        Level level;
        public float x, y;
        public float speed;
        public float extraSpeed = -Level.scrollSpeed;

        public Spikes(Level level) {
            this.level = level;
            speed = Level.scrollSpeed;
            x = Game1.screenWidth + Game1.spikesTexture.Width;
            y = (float)random.Next(Game1.screenHeight - Game1.spikesTexture.Height);
        }

        public void Update() {
            x -= speed + extraSpeed;

            if (x < -Game1.spikesTexture.Width)
                level.Remove(this, ObjectType.spikes);

            extraSpeed += 0.0125f * Level.scrollSpeed;
        }
    }
}
