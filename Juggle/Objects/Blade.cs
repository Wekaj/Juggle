using System;

namespace Juggle.Objects {
    public class Blade {
        Random random = new Random();
        Level level;
        public float x, y;
        public float speed;
        public float extraSpeed = 1;
        public float ySpeed = 0;

        public Blade(Level level) {
            this.level = level;
            speed = Level.scrollSpeed;
            if (random.Next(4) == 0)
                extraSpeed = 1.5f;
            x = Game1.screenWidth + Game1.bladeTexture.Width;
            y = (float)random.Next(Game1.screenHeight - Game1.bladeTexture.Height);

            if (Game1.gameLevel == 1 || (Game1.gameLevel == 2 && random.Next(2) == 0))
                ySpeed = random.Next(4) - random.Next(4);
        }

        public void Update() {
            x -= speed * extraSpeed;

            y += ySpeed;

            if (x < -Game1.bladeTexture.Width)
                level.Remove(this, ObjectType.blade);
        }
    }
}
