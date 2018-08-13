namespace Juggle.Objects {
    public class Platform {
        public float x, y;
        public static float speed = 2;
        public bool playerMoveX = true;
        public bool playerMoveY = true;

        public Platform(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void Update(Player player) {
            if (player.grounded && Game1.gameLevel != 2 && (Game1.gameLevel != 0 || (Game1.timer < Boss1.arrivalScore || (Game1.timer > Boss1.leavingScore && Game1.timer < Boss1.arrivalScore2) || (Game1.timer > Boss1.leavingScore2 && Game1.timer < Boss1.arrivalScore3) || Game1.timer > Boss1.leavingScore3))) {
                y += speed / 2;
                player.y += speed / 2;
            }
        }

        public void Move(float xMove, float yMove, Player player) {
            if (y + yMove > Game1.screenHeight - Game1.platformTexture.Height && yMove > 0)
                yMove = 0;

            x += xMove;
            y += yMove;

            if (x > Game1.screenWidth - Game1.platformTexture.Width) {
                x = Game1.screenWidth - Game1.platformTexture.Width;
                xMove = 0;
            }
            if (x < 0) {
                x = 0;
                xMove = 0;
            }

            if (y < 32) {
                y = 32;
                yMove = 0;
            }

            if (player.grounded) {
                if (playerMoveX)
                    player.x += xMove;
                if (playerMoveY)
                    player.y += yMove;
            }
        }
    }
}
