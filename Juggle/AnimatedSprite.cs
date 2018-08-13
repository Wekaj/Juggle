using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Juggle {
    public class AnimatedSprite {
        public Texture2D texture;
        public int rows, columns;
        private int curFrame, totalFrames;
        private int speed;
        private int curSpeed;

        public AnimatedSprite(Texture2D texture, int rows, int columns, int speed) {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.speed = speed;
            curFrame = 0;
            totalFrames = rows * columns;
        }

        public void Update() {
            curSpeed++;
            if (curSpeed >= speed) {
                curFrame++;
                if (curFrame >= totalFrames)
                    curFrame = 0;
                curSpeed = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float x, float y) {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)curFrame / (float)columns);
            int column = curFrame % columns;

            Rectangle sourceRect = new Rectangle(width * column, height * row, width, height);
            Rectangle destRect = new Rectangle((int)x, (int)y, width, height);

            spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
        }
    }
}
