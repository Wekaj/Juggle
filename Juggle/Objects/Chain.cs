namespace Juggle.Objects {
    public class Chain {
        Harpoon harpoon;
        public float x, y;
        private int position;

        public Chain(Harpoon harpoon, int position) {
            this.harpoon = harpoon;
            this.position = position;

            y = harpoon.y;
        }

        public void Update() {
            x = harpoon.x - (position * Game1.harpoonChain.Width);
        }
    }
}
