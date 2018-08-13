namespace Juggle.Objects {
    public class Dust {
        public float x, y;
        public int lifeSpan = 50;
        public float alpha = 1;
        public float width = Game1.dust1.Width;

        public Dust(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void Update() {
            lifeSpan--;
            alpha -= 0.05f;

            y += 0.5f;
            width += 0.5f;
        }
    }
}
