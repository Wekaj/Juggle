using System;

namespace Juggle.Objects {
    public class Spikes2 {
        Random random = new Random();
        Level level;
        public float x, y;
        public float speed;
        public float extraSpeed = -Level.scrollSpeed;

        public Spikes2(Level level) {
            this.level = level;
            speed = Level.scrollSpeed;
            x = (float)random.Next(Game1.screenWidth - Game1.spikesTexture2.Width);
            y = Game1.screenHeight + Game1.spikesTexture2.Height;
        }

        public void Update() {
            y -= speed + extraSpeed;

            if (y < -Game1.spikesTexture.Height)
                level.Remove(this, ObjectType.spikes2);

            extraSpeed += 0.0125f * Level.scrollSpeed;
        }
    }
}
