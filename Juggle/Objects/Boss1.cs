using System;

namespace Juggle.Objects {
    public class Boss1 {
        Random random = new Random();
        public float x, y;
        public float speed = 0.5f;
        public static int arrivalScore = 900;
        public static int leavingScore = 1750;
        public static int arrivalScore2 = 2800;
        public static int leavingScore2 = 3650;
        public static int arrivalScore3 = 5675;
        public static int leavingScore3 = 6100;
        public int controlCount = 50;
        public int controlCounter = 0;
        public int xMove, yMove;
        public float alpha = 1;

        public Boss1(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void Update(Platform platform, Player player) {
            if (Game1.timer >= arrivalScore && Game1.timer < arrivalScore2) {
                if (y > Game1.screenHeight - Game1.boss1Head.Height + 32) {
                    y -= speed;
                    if (alpha > 0)
                        alpha -= 0.001f;
                }
                if (x > -Game1.boss1Head.Width)
                    x -= Level.scrollSpeed / 10;

                if (Game1.timer <= leavingScore)
                    Control(platform, player);
            }

            if (Game1.timer >= arrivalScore2 && Game1.timer < arrivalScore3) {
                if (x < Game1.screenWidth)
                    x += Level.scrollSpeed / 8;

                if (Game1.timer <= leavingScore2)
                    Control(platform, player);
            }

            if (Game1.timer >= arrivalScore3) {
                if (x > -Game1.boss1Head.Width)
                    x -= Level.scrollSpeed / 4;

                if (Game1.timer <= leavingScore3)
                    Control(platform, player);
            }
        }

        public void Control(Platform platform, Player player) {
            controlCounter++;
            if (controlCounter >= controlCount) {
                controlCounter = 0;
                xMove = 0;
                yMove = random.Next((int)(Platform.speed)) - random.Next((int)(Platform.speed));
            }

            platform.Move(xMove, yMove, player);
        }
    }
}
