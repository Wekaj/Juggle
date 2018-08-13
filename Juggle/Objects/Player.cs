using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Juggle.Objects {
    public class Player {
        public ArrayList dust = new ArrayList();
        public float x, y;
        public float accel = 1;
        public float deccel = 0.75f;
        public float jumpPower = 11;
        public float gravity = 0.5f;
        public float xVel = 0;
        public float xVelMax = 8;
        public float yVel = 0;
        public bool grounded = false;
        public static int score = 0;
        public int coinScore = 1000;
        public static int scoreMult = 1;
        public int delay = 0;
        private int maxDelay = 80;
        public int health = 1;

        public Player(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void Update(Platform platform, Level level) {
            x += xVel;
            y += yVel;

            if (xVel > deccel)
                xVel -= deccel;
            else if (xVel < -deccel)
                xVel += deccel;
            else
                xVel = 0;

            if (y + Game1.playerTexture.Height >= platform.y - 2 && y <= platform.y + (float)Game1.platformTexture.Height / 2 && x >= platform.x - (float)Game1.playerTexture.Width && x <= platform.x + Game1.platformTexture.Width) {
                if (!grounded)
                    dust.Add(new Dust(x, y + 8));
                y = platform.y - Game1.playerTexture.Height;
                yVel = 0;
                grounded = true;
            }
            else {
                yVel += gravity;
                grounded = false;
            }

            if (y > Game1.screenHeight) {
                delay = 0;
                Death(2);
            }

            for (int i = 0; i < level.blades.Count; i++) {
                Blade targBlade = (Blade)level.blades[i];
                if (x + Game1.playerTexture.Width > targBlade.x && x < targBlade.x + Game1.bladeTexture.Width && y + Game1.playerTexture.Height > targBlade.y && y < targBlade.y + Game1.bladeTexture.Height)
                    Death(1);
                if (y + Game1.playerTexture.Height < targBlade.y && platform.y > targBlade.y && x + Game1.playerTexture.Width <= targBlade.x + Game1.bladeTexture.Width && x >= targBlade.x) {
                    Game1.bonusSound.Play();
                    score += 50 * scoreMult;
                    Game1.scoreTextSize += 0.1f;
                }
            }

            for (int i = 0; i < level.spikes.Count; i++) {
                Spikes targSpikes = (Spikes)level.spikes[i];
                if (x + Game1.playerTexture.Width > targSpikes.x && x < targSpikes.x + Game1.spikesTexture.Width && y + Game1.playerTexture.Height > targSpikes.y && y < targSpikes.y + Game1.spikesTexture.Height)
                    Death(1);
                if (y + Game1.playerTexture.Height < targSpikes.y && platform.y > targSpikes.y && x + Game1.playerTexture.Width <= targSpikes.x + Game1.spikesTexture.Width && x >= targSpikes.x) {
                    Game1.bonusSound.Play();
                    score += 100 * scoreMult;
                    Game1.scoreTextSize += 0.1f;
                }
            }

            for (int i = 0; i < level.spikes2.Count; i++) {
                Spikes2 targSpikes2 = (Spikes2)level.spikes2[i];
                if (x + Game1.playerTexture.Width > targSpikes2.x && x < targSpikes2.x + Game1.spikesTexture2.Width && y + Game1.playerTexture.Height > targSpikes2.y && y < targSpikes2.y + Game1.spikesTexture2.Height)
                    Death(1);
                if (y + Game1.playerTexture.Height < targSpikes2.y && platform.y > targSpikes2.y && x + Game1.playerTexture.Width <= targSpikes2.x + Game1.spikesTexture2.Width && x >= targSpikes2.x) {
                    Game1.bonusSound.Play();
                    score += 150 * scoreMult;
                    Game1.scoreTextSize += 0.1f;
                }
            }

            for (int i = 0; i < level.harpoons.Count; i++) {
                Harpoon targHarpoon = (Harpoon)level.harpoons[i];
                if (x + Game1.playerTexture.Width > targHarpoon.x && x < targHarpoon.x + Game1.harpoonHead.Width && y + Game1.playerTexture.Height > targHarpoon.y && y < targHarpoon.y + Game1.harpoonHead.Height)
                    Death(1);
                if (y + Game1.playerTexture.Height < targHarpoon.y && platform.y > targHarpoon.y && x + Game1.playerTexture.Width <= targHarpoon.x + Game1.harpoonHead.Width && x >= targHarpoon.x) {
                    Game1.bonusSound.Play();
                    score += 100 * scoreMult;
                    Game1.scoreTextSize += 0.1f;
                }
            }

            for (int i = 0; i < level.coins.Count; i++) {
                Coin targCoin = (Coin)level.coins[i];
                if (x + Game1.playerTexture.Width > targCoin.x && x < targCoin.x + Game1.coinTexture.Width && y + Game1.playerTexture.Height > targCoin.y && y < targCoin.y + Game1.coinTexture.Height) {
                    Game1.coinSound.Play();
                    score += coinScore * scoreMult;
                    Game1.scoreTextSize += 0.5f;
                    level.Remove(targCoin, ObjectType.coin);
                }
            }

            if (yVel < -8)
                dust.Add(new Dust(x, y));

            for (int i = 0; i < dust.Count; i++) {
                Dust targDust = (Dust)dust[i];
                targDust.Update();
            }

            if (delay > 0)
                delay--;

            score += 1 * scoreMult;
        }

        public void Move(float xMove, float yMove) {
            if (xVel <= xVelMax && xVel >= -xVelMax)
                xVel += xMove;
            yVel += yMove;
        }

        public void Jump() {
            if (grounded) {
                yVel -= jumpPower;
                Game1.jumpSound.Play();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < dust.Count; i++) {
                Dust targDust = (Dust)dust[i];
                spriteBatch.Draw(Game1.dust1, new Rectangle((int)(targDust.x + (Game1.dust1.Width / 2) - (targDust.width / 2)), (int)targDust.y, (int)targDust.width, Game1.dust1.Height), Color.White * targDust.alpha);
            }
        }

        public void Death(int loss) {
            if (health - loss < 0 && delay == 0) {
                Game1.state = GameState.lose;
                Game1.hurtSound.Play();
                MediaPlayer.Stop();
            }
            else if (delay == 0) {
                Game1.hurtSound.Play();
                health -= loss;
                Game1.goal = score + (2000 * scoreMult);
                delay = maxDelay;
            }
        }
    }
}
