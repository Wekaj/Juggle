using Juggle.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

//Warning, large mess approaching from the south! 

namespace Juggle {
    public enum GameState {
        menu,
        game,
        win,
        lose,
        help,
        level
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Platform platform;
        Level level;
        KeyboardState keyboardState;
        Boss1 boss1;
        Random random = new Random();

        public static GameState state = GameState.menu;
        public static int screenWidth, screenHeight;
        public float scoreTextSizeMin = 0.75f;
        public static float scoreTextSize;
        public static int timer;
        public float overlayAlpha = 1;
        private int select = 0;
        private int selectMax = 1;
        public static int select2;
        private int selectMax2 = 2;
        private int delay = 0;
        public static int gameLevel = 0;
        int type = 0;
        private float seconds;
        private int tick = 10;
        public static int goal;

        //Assets
        public static Texture2D playerTexture;
        public static Texture2D playerJumpTexture;
        public static Texture2D playerTexture2;
        public static Texture2D playerJumpTexture2;
        public static Texture2D platformTexture;
        public static Texture2D platformTexture2;
        public static Texture2D bladeTexture;
        public static Texture2D arrowTexture;
        public static Texture2D background;
        public static Texture2D background2;
        public static Texture2D boss1Head;
        public static Texture2D boss1HeadGlow;
        public static Texture2D dust1;
        public static Texture2D coinTexture;
        public static Texture2D playerWalkingTexture;
        public static Texture2D playerWalkingTexture2;
        public static Texture2D spikesTexture;
        public static Texture2D spikesTexture2;
        public static Texture2D harpoonHead;
        public static Texture2D harpoonChain;
        public static Texture2D glow;
        public static Texture2D hellSpike1;
        public static Texture2D hellSpike2;
        public static Texture2D hellSpike3;
        public static Texture2D cloud1;
        public static Texture2D cloud2;
        public static Texture2D cloud3;
        public static Texture2D gradientBlack;
        public static Texture2D gradientWhite;
        public static Texture2D startHell;
        public static Texture2D startDream;
        public static Texture2D title;

        public static AnimatedSprite playerWalking;
        public static AnimatedSprite playerWalking2;

        public static SpriteFont font1;
        public static SpriteFont font2;

        public static SoundEffect jumpSound;
        public static SoundEffect hurtSound;
        public static SoundEffect blipSound;
        public static SoundEffect endSound;
        public static SoundEffect coinSound;
        public static SoundEffect bonusSound;
        public static SoundEffect selectSound;
        public static SoundEffect healthSound;

        public static Song gameSong1;
        public static Song gameSong2;
        public static Song gameSong3;

        //Button pressing
        private bool pressSpace = true;
        private bool pressUp = true;
        private bool pressDown = true;
        private bool pressEnter = true;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //Bill: Oh god, I don't think I can go any further...
        //Dave: Man up, Bill! Only a few hundred more lines of this mess and we're free!

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            MediaPlayer.IsRepeating = true;

            select2 = random.Next(2);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("Sprites/playerTexture");
            playerJumpTexture = Content.Load<Texture2D>("Sprites/playerJumpTexture");
            playerTexture2 = Content.Load<Texture2D>("Sprites/playerTexture2");
            playerJumpTexture2 = Content.Load<Texture2D>("Sprites/playerJumpTexture2");
            platformTexture = Content.Load<Texture2D>("Sprites/platformTexture");
            platformTexture2 = Content.Load<Texture2D>("Sprites/platformTexture2");
            bladeTexture = Content.Load<Texture2D>("Sprites/bladeTexture");
            arrowTexture = Content.Load<Texture2D>("Sprites/arrowTexture");
            background = Content.Load<Texture2D>("Sprites/background");
            background2 = Content.Load<Texture2D>("Sprites/background2");
            boss1Head = Content.Load<Texture2D>("Sprites/boss1Head");
            boss1HeadGlow = Content.Load<Texture2D>("Sprites/boss1HeadGlow");
            dust1 = Content.Load<Texture2D>("Sprites/dust1");
            coinTexture = Content.Load<Texture2D>("Sprites/coinTexture");
            playerWalkingTexture = Content.Load<Texture2D>("Atlases/playerWalking");
            playerWalkingTexture2 = Content.Load<Texture2D>("Atlases/playerWalking2");
            spikesTexture = Content.Load<Texture2D>("Sprites/spikesTexture");
            spikesTexture2 = Content.Load<Texture2D>("Sprites/spikesTexture2");
            harpoonHead = Content.Load<Texture2D>("Sprites/harpoonHead");
            harpoonChain = Content.Load<Texture2D>("Sprites/harpoonChain");
            glow = Content.Load<Texture2D>("Sprites/glow");
            hellSpike1 = Content.Load<Texture2D>("Sprites/hellSpike1");
            hellSpike2 = Content.Load<Texture2D>("Sprites/hellSpike2");
            hellSpike3 = Content.Load<Texture2D>("Sprites/hellSpike3");
            cloud1 = Content.Load<Texture2D>("Sprites/cloud1");
            cloud2 = Content.Load<Texture2D>("Sprites/cloud2");
            cloud3 = Content.Load<Texture2D>("Sprites/cloud3");
            gradientBlack = Content.Load<Texture2D>("Sprites/gradientBlack");
            gradientWhite = Content.Load<Texture2D>("Sprites/gradientWhite");
            startHell = Content.Load<Texture2D>("Sprites/startHell");
            startDream = Content.Load<Texture2D>("Sprites/startDream");
            title = Content.Load<Texture2D>("Sprites/title");

            playerWalking = new AnimatedSprite(playerWalkingTexture, 1, 4, 10);
            playerWalking2 = new AnimatedSprite(playerWalkingTexture2, 1, 4, 10);

            font1 = Content.Load<SpriteFont>("Fonts/font1");
            font2 = Content.Load<SpriteFont>("Fonts/font2");

            jumpSound = Content.Load<SoundEffect>("SoundEffects/jumpSound");
            hurtSound = Content.Load<SoundEffect>("SoundEffects/hurtSound");
            blipSound = Content.Load<SoundEffect>("SoundEffects/blipSound");
            endSound = Content.Load<SoundEffect>("SoundEffects/endSound");
            coinSound = Content.Load<SoundEffect>("SoundEffects/coinSound");
            bonusSound = Content.Load<SoundEffect>("SoundEffects/bonusSound");
            selectSound = Content.Load<SoundEffect>("SoundEffects/selectSound");
            healthSound = Content.Load<SoundEffect>("SoundEffects/healthSound");

            gameSong1 = Content.Load<Song>("Songs/gameSong1");
            gameSong2 = Content.Load<Song>("Songs/gameSong2");
            gameSong3 = Content.Load<Song>("Songs/gameSong3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();

            //Bill: No, NO! I'm not going down there!
            //Dave: Bill!
            //Jim: Let him be, Dave, this journey isn't for anyone unwilling to lose their sanity.

            //Menu
            if (state == GameState.menu) {
                if (keyboardState.IsKeyDown(Keys.Enter) && pressEnter) {
                    if (select == 0) {
                        selectSound.Play();
                        state = GameState.level;
                        delay = 1;
                    }
                    if (select == 1) {
                        selectSound.Play();
                        state = GameState.help;
                    }
                    select = 0;
                    pressEnter = false;
                }

                //Selection
                if (keyboardState.IsKeyDown(Keys.Up) && select > 0 && pressUp) {
                    select -= 1;
                    blipSound.Play();
                    pressUp = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Up) && pressUp) {
                    select = selectMax;
                    blipSound.Play();
                    pressUp = false;
                }
                if (keyboardState.IsKeyDown(Keys.Down) && select < selectMax && pressDown) {
                    select += 1;
                    blipSound.Play();
                    pressDown = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) && pressDown) {
                    select = 0;
                    blipSound.Play();
                    pressDown = false;
                }

                if (overlayAlpha < 1)
                    overlayAlpha += 0.01f;
            }

            //Game
            if (state == GameState.game) {
                if (Menu())
                    MediaPlayer.Stop();

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                seconds += elapsed;

                player.Update(platform, level);
                platform.Update(player);
                level.Update();
                if (gameLevel == 0)
                    boss1.Update(platform, player);

                if (player.grounded && (player.xVel > 0 || player.xVel < 0))
                    playerWalking.Update();

                //Platform movement
                if (gameLevel != 2) {
                    if (gameLevel == 1 || (timer < Boss1.arrivalScore || (timer > Boss1.leavingScore && timer < Boss1.arrivalScore2) || (timer > Boss1.leavingScore2 && timer < Boss1.arrivalScore3) || timer > Boss1.leavingScore3)) {
                        if (keyboardState.IsKeyDown(Keys.W))
                            platform.Move(0, -Platform.speed, player);
                        if (keyboardState.IsKeyDown(Keys.D))
                            platform.Move(Platform.speed, 0, player);
                        if (keyboardState.IsKeyDown(Keys.S))
                            platform.Move(0, Platform.speed, player);
                        if (keyboardState.IsKeyDown(Keys.A))
                            platform.Move(-Platform.speed, 0, player);
                    }
                }

                //Player movement
                if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space)) && pressSpace) {
                    player.Jump();
                    pressSpace = false;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                    player.Move(player.accel, 0);
                if (keyboardState.IsKeyDown(Keys.Down))
                    player.Move(0, player.accel);
                if (keyboardState.IsKeyDown(Keys.Left))
                    player.Move(-player.accel, 0);

                //Music
                if (gameLevel == 0) {
                    if (MediaPlayer.PlayPosition >= gameSong1.Duration - TimeSpan.FromSeconds(9))
                        FadeMusic();
                }
                if (gameLevel == 1) {
                    if (MediaPlayer.PlayPosition >= gameSong2.Duration - TimeSpan.FromSeconds(9))
                        FadeMusic();
                }
                if (gameLevel == 2) {
                    if (MediaPlayer.PlayPosition >= gameSong3.Duration)
                        Player.scoreMult++;
                }

                //Text
                if (scoreTextSize > scoreTextSizeMin)
                    scoreTextSize -= 0.05f;

                if (overlayAlpha > 0)
                    overlayAlpha -= 0.01f;

                tick--;
                if (tick <= 0)
                    tick = 10;

                if (Player.score > goal && goal > 0) {
                    goal = 0;
                    player.health += 1;
                    healthSound.Play();
                }

                timer++;
            }

            //Lose
            if (state == GameState.lose) {
                Menu();

                if (keyboardState.IsKeyDown(Keys.Enter)) {
                    selectSound.Play();
                    state = GameState.game;
                    StartGame();
                }

                if (overlayAlpha < 1)
                    overlayAlpha += 0.01f;
            }

            //Win
            if (state == GameState.win) {
                Menu();

                if (keyboardState.IsKeyDown(Keys.Enter)) {
                    selectSound.Play();
                    state = GameState.game;
                    StartGame();
                }

                if (overlayAlpha < 1)
                    overlayAlpha += 0.01f;
            }

            //Help
            if (state == GameState.help) {
                Menu();

                if (overlayAlpha < 1)
                    overlayAlpha += 0.01f;
            }

            //Level select
            if (state == GameState.level) {
                Menu();

                if (keyboardState.IsKeyDown(Keys.Enter) && delay == 0 && pressEnter) {
                    selectSound.Play();
                    state = GameState.game;

                    if (select2 == 0)
                        gameLevel = 0;
                    if (select2 == 1)
                        gameLevel = 1;
                    if (select2 == 2)
                        gameLevel = 2;

                    StartGame();
                    select = 0;
                    pressEnter = false;
                }

                //Selection
                if (keyboardState.IsKeyDown(Keys.Up) && select2 > 0 && pressUp) {
                    select2 -= 1;
                    blipSound.Play();
                    pressUp = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Up) && pressUp) {
                    select2 = selectMax2;
                    blipSound.Play();
                    pressUp = false;
                }
                if (keyboardState.IsKeyDown(Keys.Down) && select2 < selectMax2 && pressDown) {
                    select2 += 1;
                    blipSound.Play();
                    pressDown = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) && pressDown) {
                    select2 = 0;
                    blipSound.Play();
                    pressDown = false;
                }

                if (overlayAlpha < 1)
                    overlayAlpha += 0.01f;
            }

            if (delay > 0)
                delay--;

            //Keys are up?
            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Space))
                pressSpace = true;
            if (keyboardState.IsKeyUp(Keys.Up))
                pressUp = true;
            if (keyboardState.IsKeyUp(Keys.Down))
                pressDown = true;
            if (keyboardState.IsKeyUp(Keys.Enter))
                pressEnter = true;

            base.Update(gameTime);

            //Dave: Dear god, I can feel myself slowly slipping...
            //Jim: Hang in their Dave! I'll find help, just rest here!
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (state == GameState.game) {
                if (gameLevel == 0)
                    spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                if (gameLevel == 1)
                    spriteBatch.Draw(background2, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                if (gameLevel == 2)
                    spriteBatch.Draw(glow, new Vector2((int)(player.x - (glow.Width / 2) + (playerTexture.Width / 2)), (int)(player.y - (glow.Height / 2) + (playerTexture.Height / 2))), Color.White);
                if (gameLevel == 0) {
                    if ((timer > Boss1.leavingScore && timer < Boss1.arrivalScore2) || (timer > Boss1.leavingScore2 && timer < Boss1.arrivalScore3) || timer > Boss1.leavingScore3) {
                        spriteBatch.Draw(boss1Head, new Vector2((float)Math.Round(boss1.x), (float)Math.Round(boss1.y)), Color.White);
                        spriteBatch.Draw(boss1Head, new Vector2((float)Math.Round(boss1.x), (float)Math.Round(boss1.y)), Color.Black * boss1.alpha);
                    }
                    else {
                        spriteBatch.Draw(boss1HeadGlow, new Vector2((float)Math.Round(boss1.x), (float)Math.Round(boss1.y)), Color.White);
                        spriteBatch.Draw(boss1HeadGlow, new Vector2((float)Math.Round(boss1.x), (float)Math.Round(boss1.y)), Color.Black * boss1.alpha);
                    }
                }

                //Jim: *vomits violently* He-HELLO?! ANYOOONE??!!

                level.Draw(spriteBatch);
                if (player.y > -playerTexture.Height && (tick > 5 || player.delay <= 0)) {
                    if (player.health > 0) {
                        if (player.yVel < 0)
                            spriteBatch.Draw(playerJumpTexture, new Vector2((float)Math.Round(player.x), (float)Math.Round(player.y)), Color.White);
                        else if (player.grounded && (player.xVel > 0 || player.xVel < 0))
                            playerWalking.Draw(spriteBatch, player.x, player.y);
                        else
                            spriteBatch.Draw(playerTexture, new Vector2((float)Math.Round(player.x), (float)Math.Round(player.y)), Color.White);
                    }
                    else {
                        if (player.yVel < 0)
                            spriteBatch.Draw(playerJumpTexture2, new Vector2((float)Math.Round(player.x), (float)Math.Round(player.y)), Color.White);
                        else if (player.grounded && (player.xVel > 0 || player.xVel < 0))
                            playerWalking2.Draw(spriteBatch, player.x, player.y);
                        else
                            spriteBatch.Draw(playerTexture2, new Vector2((float)Math.Round(player.x), (float)Math.Round(player.y)), Color.White);
                    }
                }
                else if (player.y <= -playerTexture.Height)
                    spriteBatch.Draw(arrowTexture, new Vector2((float)Math.Round(player.x), 0), Color.White);

                if (gameLevel == 2)
                    spriteBatch.Draw(platformTexture2, new Vector2((float)Math.Round(platform.x), (float)Math.Round(platform.y)), Color.White);
                else if (gameLevel == 0) {
                    if ((timer >= Boss1.arrivalScore && timer <= Boss1.leavingScore) || (timer >= Boss1.arrivalScore2 && timer <= Boss1.leavingScore2) || (timer >= Boss1.arrivalScore3 && timer <= Boss1.leavingScore3))
                        spriteBatch.Draw(platformTexture2, new Vector2((float)Math.Round(platform.x), (float)Math.Round(platform.y)), Color.White);
                    else
                        spriteBatch.Draw(platformTexture, new Vector2((float)Math.Round(platform.x), (float)Math.Round(platform.y)), Color.White);
                }
                else
                    spriteBatch.Draw(platformTexture, new Vector2((float)Math.Round(platform.x), (float)Math.Round(platform.y)), Color.White);

                player.Draw(spriteBatch);

                string multText = "";

                if (Player.scoreMult > 1)
                    multText += Player.scoreMult + "x Multiplier!";

                spriteBatch.DrawString(font1, "Time: " + Math.Round(seconds, 2) + "s", new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font1, "Time: " + Math.Round(seconds, 2) + "s", new Vector2(8, 8), Color.White);

                string goalText = "";
                if (goal > 0) {
                    goalText += " / " + goal;
                }

                spriteBatch.DrawString(font1, "Score: " + Player.score + goalText + "\n" + multText, new Vector2(10, 42), Color.Black, 0, new Vector2(0, 0), scoreTextSize, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font1, "Score: " + Player.score + goalText + "\n" + multText, new Vector2(8, 40), Color.White, 0, new Vector2(0, 0), scoreTextSize, SpriteEffects.None, 0f);
            }

            if (select2 == 0)
                type = 0;
            if (select2 == 1)
                type = 1;
            if (select2 == 2)
                type = 2;

            if (type == 0) {
                spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * overlayAlpha);
                spriteBatch.Draw(startHell, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * overlayAlpha);
            }
            if (type == 1) {
                spriteBatch.Draw(background2, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * overlayAlpha);
                spriteBatch.Draw(startDream, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * overlayAlpha);
            }

            //Jim: M-must save Dave, must save Dave...

            if (state == GameState.menu) {
                if (select == 0)
                    spriteBatch.DrawString(font2, "> Start Game", new Vector2(32, 192), Color.White);
                else
                    spriteBatch.DrawString(font2, "Start Game", new Vector2(32, 192), Color.White);
                if (select == 1)
                    spriteBatch.DrawString(font2, "> Help", new Vector2(32, 224), Color.White);
                else
                    spriteBatch.DrawString(font2, "Help", new Vector2(32, 224), Color.White);

                spriteBatch.Draw(title, new Vector2(32, 32), Color.White);
            }

            if (state == GameState.lose) {
                spriteBatch.DrawString(font1, "You Died.\nTime: " + Math.Round(seconds, 2) + "s\nScore: " + Player.score, new Vector2(32, 32), Color.White);
                spriteBatch.DrawString(font2, "Push R to return to the menu.\nPush ENTER to retry this level.", new Vector2(32, 160), Color.White);
            }

            if (state == GameState.win) {
                spriteBatch.DrawString(font2, "You Won!\nTime: " + Math.Round(seconds, 2) + "s\nScore: " + Player.score, new Vector2(32, 32), Color.White);
                spriteBatch.DrawString(font2, "Push R to return to the menu.\nPush ENTER to retry this level.", new Vector2(32, 160), Color.White);
            }

            if (state == GameState.help) {
                spriteBatch.DrawString(font2, "Left and Right Arrow Keys = Move your character\nSpacebar / Up Arrow Key = Jump\nDown Arrow Key = Fall faster\nW, A, S and D = Move your platform\nR = Back\nEnter = Forward\n\nYou only get one platform to stand on as you guide your character through the various hazards of \neach stage. You can achieve a higher score in each run by collecting golden coins and jumping over \nobstacles. When you get hurt once, you must accumulate 2000 more points to gain back your lost \nhealth. If you fail to do so and get hurt again, you die!\n\nThis game was created by Jake Wellington (Wekaj) in under 48hrs for Ludum Dare 28, with the \ntheme: You Only Get One.", new Vector2(32, 32), Color.White);
            }

            if (state == GameState.level) {
                if (select2 == 0) {
                    spriteBatch.DrawString(font2, "> Nightmare", new Vector2(32, 32), Color.White);
                    spriteBatch.DrawString(font2, "Try to reach the end of the nightmare whilst facing its horrors.", new Vector2(32, 160), Color.White);
                }
                else
                    spriteBatch.DrawString(font2, "Nightmare", new Vector2(32, 32), Color.White);
                if (select2 == 1) {
                    spriteBatch.DrawString(font2, "> Dream", new Vector2(32, 64), Color.White);
                    spriteBatch.DrawString(font2, "Soar through the dream, dodging harpoons and falling blades.", new Vector2(32, 160), Color.White);
                }
                else
                    spriteBatch.DrawString(font2, "Dream", new Vector2(32, 64), Color.White);
                if (select2 == 2) {
                    spriteBatch.DrawString(font2, "> Insanity", new Vector2(32, 96), Color.White);
                    spriteBatch.DrawString(font2, "Withstand the insanity without any control over your platform.", new Vector2(32, 160), Color.White);
                }
                else
                    spriteBatch.DrawString(font2, "Insanity", new Vector2(32, 96), Color.White);
            }

            if (state != GameState.game && state != GameState.menu)
                spriteBatch.DrawString(font2, "< R", new Vector2(4, 4), Color.White);

            if (state != GameState.game && state != GameState.help)
                spriteBatch.DrawString(font2, "Enter >", new Vector2(screenWidth - font2.MeasureString("Enter >").X - 4, 4), Color.White);

            spriteBatch.End();

            //Dave: Too... much... *dies*

            base.Draw(gameTime);
        }

        public void StartGame() {
            player = new Player(screenWidth / 2 - playerTexture.Width / 2, screenHeight / 3 * 2);
            platform = new Platform(screenWidth / 2 - platformTexture.Width / 2, screenHeight / 3 * 2);
            level = new Level(platform, player);
            Player.score = 0;
            seconds = 0;
            scoreTextSize = scoreTextSizeMin;
            timer = 0;
            goal = 0;

            if (gameLevel == 0) {
                boss1 = new Boss1(screenWidth - boss1Head.Width, screenHeight);
                MediaPlayer.Play(gameSong1);
            }
            if (gameLevel == 1) {
                MediaPlayer.Play(gameSong2);
            }
            if (gameLevel == 2) {
                MediaPlayer.Play(gameSong3);
            }
        }

        public void FadeMusic() {
            if (MediaPlayer.Volume > 0.005f)
                MediaPlayer.Volume -= 0.005f;
            else {
                MediaPlayer.Stop();
                MediaPlayer.Volume = 1;
                endSound.Play();
                state = GameState.win;
            }
        }

        public bool Menu() {
            if (keyboardState.IsKeyDown(Keys.R)) {
                selectSound.Play();
                state = GameState.menu;
                return true;
            }
            return false;
        }
    }
}