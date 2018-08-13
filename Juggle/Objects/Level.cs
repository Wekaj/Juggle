using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Juggle.Objects {
    public enum ObjectType {
        blade,
        coin,
        spikes,
        spikes2,
        harpoon,
        background
    }

    public class Level {
        Random random = new Random();
        public ArrayList blades = new ArrayList();
        public ArrayList coins = new ArrayList();
        public ArrayList spikes = new ArrayList();
        public ArrayList spikes2 = new ArrayList();
        public ArrayList harpoons = new ArrayList();
        public ArrayList backgrounds = new ArrayList();
        public int bladeCount;
        public int bladeCountMin = 5;
        public int bladeCountMinMin = 1;
        public int bladeCountMax = 30;
        public int bladeCountMaxMin = 10;
        public int bladeCounter = 0;
        public int coinCount = 120;
        public int coinCounter = 0;
        public int spikesCount = 180;
        public int spikesCounter = 0;
        public int spikes2Count = 140;
        public int spikes2Counter = 0;
        public int harpoonCount = 360;
        public int harpoonCounter = 0;
        public int bgCount;
        public int bgCountMin = 20;
        public int bgCounter = 0;
        public static float scrollSpeed;
        public int scoreTracker = 0;
        public int levelUp = 500;
        public int controlCount = 50;
        public int controlCounter = 0;
        public int xMove, yMove;
        public Platform platform;
        public Player player;

        public Level(Platform platform, Player player) {
            this.platform = platform;
            this.player = player;

            if (Game1.gameLevel == 0) {
                scrollSpeed = 3;
                backgrounds.Add(new Background(Game1.gradientBlack, true));
            }
            if (Game1.gameLevel == 1) {
                scrollSpeed = 4;
                backgrounds.Add(new Background(Game1.gradientWhite, true));
            }
            if (Game1.gameLevel == 2) {
                scrollSpeed = 5;
            }

            bgCount = bgCountMin + random.Next(60);
            bladeCount = bladeCountMin + random.Next(bladeCountMax);
        }

        public void Update() {
            if (Game1.gameLevel == 2)
                Control();

            //Blades
            bladeCounter++;
            if (bladeCounter >= bladeCount) {
                bladeCounter = 0;
                bladeCount = bladeCountMin + random.Next(bladeCountMax);
                blades.Add(new Blade(this));
            }

            for (int i = 0; i < blades.Count; i++) {
                Blade targBlade = (Blade)blades[i];
                targBlade.Update();
            }

            //Coins
            coinCounter++;
            if (coinCounter >= coinCount) {
                coinCounter = 0;
                coins.Add(new Coin(this));
            }

            for (int i = 0; i < coins.Count; i++) {
                Coin targCoin = (Coin)coins[i];
                targCoin.Update();
            }

            //Spikes
            if (Game1.timer > 3750 || Game1.gameLevel == 2) {
                spikesCounter++;
                if (spikesCounter >= spikesCount) {
                    spikesCounter = 0;
                    spikes.Add(new Spikes(this));
                }

                for (int i = 0; i < spikes.Count; i++) {
                    Spikes targSpikes = (Spikes)spikes[i];
                    targSpikes.Update();
                }
            }

            //Spikes2
            if (Game1.gameLevel != 1) {
                spikes2Counter++;
                if (spikes2Counter >= spikes2Count) {
                    spikes2Counter = 0;
                    spikes2.Add(new Spikes2(this));
                }

                for (int i = 0; i < spikes2.Count; i++) {
                    Spikes2 targSpikes2 = (Spikes2)spikes2[i];
                    targSpikes2.Update();
                }
            }

            //Harpoons
            if (Game1.gameLevel == 1 || Game1.gameLevel == 2) {
                harpoonCounter++;
                if (harpoonCounter >= harpoonCount) {
                    harpoonCounter = 0;
                    harpoons.Add(new Harpoon(this));
                }

                for (int i = 0; i < harpoons.Count; i++) {
                    Harpoon targHarpoon = (Harpoon)harpoons[i];
                    targHarpoon.Update();
                }
            }

            //Backgrounds
            bgCounter++;
            if (bgCounter >= bgCount && Game1.gameLevel != 2) {
                int num = random.Next(3);
                Texture2D texture = Game1.hellSpike1;
                switch (Game1.gameLevel) {
                    case 0:
                    switch (num) {
                        case 0:
                        texture = Game1.hellSpike1;
                        break;
                        case 1:
                        texture = Game1.hellSpike2;
                        break;
                        case 2:
                        texture = Game1.hellSpike3;
                        break;
                    }
                    break;
                    case 1:
                    switch (num) {
                        case 0:
                        texture = Game1.cloud1;
                        break;
                        case 1:
                        texture = Game1.cloud2;
                        break;
                        case 2:
                        texture = Game1.cloud3;
                        break;
                    }
                    break;
                }
                bgCounter = 0;
                bgCount = bgCountMin + random.Next(60);
                backgrounds.Add(new Background(texture, false));
            }

            for (int i = 0; i < backgrounds.Count; i++) {
                Background targBg = (Background)backgrounds[i];
                targBg.Update(this);
            }

            scoreTracker++;

            if (scoreTracker >= levelUp) {
                scoreTracker = 0;
                if (bladeCountMin > bladeCountMinMin)
                    bladeCountMin--;
                if (bladeCountMax > bladeCountMaxMin)
                    bladeCountMax--;
                scrollSpeed += 0.5f;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < backgrounds.Count; i++) {
                Background targBg = (Background)backgrounds[i];
                spriteBatch.Draw(targBg.texture, new Vector2(targBg.x, targBg.y), Color.White);
            }

            for (int i = 0; i < coins.Count; i++) {
                Coin targCoin = (Coin)coins[i];
                spriteBatch.Draw(Game1.coinTexture, new Vector2(targCoin.x, targCoin.y), Color.White);
            }

            for (int i = 0; i < blades.Count; i++) {
                Blade targBlade = (Blade)blades[i];
                spriteBatch.Draw(Game1.bladeTexture, new Vector2(targBlade.x, targBlade.y), Color.White);
            }

            for (int i = 0; i < spikes.Count; i++) {
                Spikes targSpikes = (Spikes)spikes[i];
                spriteBatch.Draw(Game1.spikesTexture, new Vector2(targSpikes.x, targSpikes.y), Color.White);
            }

            for (int i = 0; i < spikes2.Count; i++) {
                Spikes2 targSpikes2 = (Spikes2)spikes2[i];
                spriteBatch.Draw(Game1.spikesTexture2, new Vector2(targSpikes2.x, targSpikes2.y), Color.White);
            }

            for (int i = 0; i < harpoons.Count; i++) {
                Harpoon targHarpoon = (Harpoon)harpoons[i];
                spriteBatch.Draw(Game1.harpoonHead, new Vector2(targHarpoon.x, targHarpoon.y), Color.White);

                for (int i2 = 0; i2 < targHarpoon.chains.Count; i2++) {
                    Chain targChain = (Chain)targHarpoon.chains[i2];
                    spriteBatch.Draw(Game1.harpoonChain, new Vector2(targChain.x, targChain.y), Color.White);
                }
            }
        }

        public void Remove(Object obj, ObjectType type) {
            if (type == ObjectType.blade)
                blades.Remove(obj);
            if (type == ObjectType.coin)
                coins.Remove(obj);
            if (type == ObjectType.spikes)
                spikes.Remove(obj);
            if (type == ObjectType.spikes2)
                spikes2.Remove(obj);
            if (type == ObjectType.harpoon)
                harpoons.Remove(obj);
            if (type == ObjectType.background)
                backgrounds.Remove(obj);
        }

        public void Control() {
            controlCounter++;
            if (controlCounter >= controlCount) {
                controlCounter = 0;
                xMove = 0;
                yMove = random.Next((int)(Platform.speed * 1.5f)) - random.Next((int)(Platform.speed * 1.5f));
            }

            platform.Move(xMove, yMove, player);
        }
    }
}
