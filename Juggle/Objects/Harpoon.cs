using System;
using System.Collections;

namespace Juggle.Objects {
    public class Harpoon {
        Random random = new Random();
        Level level;
        public ArrayList chains = new ArrayList();
        public float x, y;
        public float speed;
        public float extraSpeed = 0;
        public int dir = 0;

        public Harpoon(Level level) {
            this.level = level;
            speed = Level.scrollSpeed;
            x = 0 - Game1.harpoonHead.Width;
            y = (float)random.Next(Game1.screenHeight - Game1.harpoonHead.Height);
        }

        public void Update() {
            if (x > chains.Count * Game1.harpoonChain.Width)
                chains.Add(new Chain(this, chains.Count + 1));

            for (int i = 0; i < chains.Count; i++) {
                Chain targChain = (Chain)chains[i];
                targChain.Update();
            }

            if (dir == 0)
                x += speed + extraSpeed;
            if (dir == 1)
                x -= speed;

            if (x > Game1.screenWidth)
                dir = 1;

            if (x < -Game1.harpoonHead.Width)
                level.Remove(this, ObjectType.harpoon);

            extraSpeed += 0.0125f * speed;
        }
    }
}
