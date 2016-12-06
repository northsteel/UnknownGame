using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace UnknownGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Player support
        private Texture2D playerSprite;
        private Rectangle playerDrawRect;
        private int playerMoveAmount = 3;

        // Ball support
        //private Texture2D ballSprite;
        //private Rectangle ballDrawRect;
        private Vector2 ballVelocity;
        private Sprite ball;

        // Bat support
        private Texture2D batSprite;
        private Rectangle batDrawRect;
        private int batMoveAmount = 25;

        // Robot support
        private Sprite robot;
        private float rotation;
        private Sprite debug;

        // Enemy bat support
        private Rectangle enemyBatDrawRect;
        private int enemyBatMoveAmount = 5;

        // Input support
        KeyboardState keyboard;

        // Screen support
        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        // Random support
        private Random rnd = new Random();

        // Animation
        private Animation flamemanAnimation;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            // Set window resolution
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            // Set ball's velocity
            ballVelocity = GetRandomVelocity(5);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load player, bat and ball sprites
            playerSprite = Content.Load<Texture2D>("Graphics/Player");

            ball = new Sprite(Content.Load<Texture2D>("Graphics/Ball"), new Vector2(WindowWidth / 2, WindowHeight / 2), Sprite.Pivot.TopLeft);
            batSprite = Content.Load<Texture2D>("Graphics/Bat");

            // Robot
            robot = new Sprite(Content.Load<Texture2D>("Graphics/Robot"), new Vector2(WindowWidth / 2, WindowHeight / 2), Sprite.Pivot.Center);

            // Create rectangle objects
            playerDrawRect = new Rectangle(0, 0, playerSprite.Width, playerSprite.Height);
            //ballDrawRect = new Rectangle(WindowWidth / 2, WindowHeight / 2, ballSprite.Width, ballSprite.Height);
            batDrawRect = new Rectangle(WindowWidth / 2, WindowHeight - 30, batSprite.Width, batSprite.Height);
            enemyBatDrawRect = new Rectangle(WindowWidth / 2, 30, batSprite.Width, batSprite.Height);

            debug = new Sprite(Content.Load<Texture2D>("Graphics/debug"), new Vector2(WindowWidth / 2, WindowHeight / 2), Sprite.Pivot.Center);

            flamemanAnimation = new Animation(Content.Load<Texture2D>("Graphics/FlameMan"), new Point(44, 47), new Point(352, 47), new TimeSpan(0, 0, 1));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            rotation += 3f * gameTime.ElapsedGameTime.Milliseconds / 1000f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Random rnd = new Random();
                robot.PivotPoint = (Sprite.Pivot)rnd.Next(0, 9);
            }

            // Get keyboard state
            keyboard = Keyboard.GetState();

            // Move the bat
            if (keyboard.IsKeyDown(Keys.A))
            {
                batDrawRect.X -= batMoveAmount;

                // Keep bat in the window
                if (batDrawRect.X < 0)
                {
                    batDrawRect.X = 0;
                }
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                batDrawRect.X += batMoveAmount;

                // Do not let the bat go off the righ screen boundary
                if (batDrawRect.X + batDrawRect.Width > WindowWidth)
                {
                    batDrawRect.X = WindowWidth - batDrawRect.Width;
                }
            }

            // Collision detection
            if (batDrawRect.Intersects(ball.DrawRectangle))
            {
                Rectangle rect = ball.DrawRectangle;
                rect.Y = batDrawRect.Y - ball.DrawRectangle.Height;
                ball.DrawRectangle = rect;

                ballVelocity.Y = -ballVelocity.Y;
            }

            //if (keyboard.IsKeyDown(Keys.Left) && (playerRect.X >= playerMoveAmount))
            //{
            //    playerRect.X -= playerMoveAmount;
            //}
            //if (keyboard.IsKeyDown(Keys.Right) && ((playerRect.X + playerRect.Width) <= (WindowWidth - playerMoveAmount)))
            //{
            //    playerRect.X += playerMoveAmount;
            //}

            //if (keyboard.IsKeyDown(Keys.Up) && (playerRect.Y >= playerMoveAmount))
            //{
            //    playerRect.Y -= playerMoveAmount;
            //}
            //if (keyboard.IsKeyDown(Keys.Down) && ((playerRect.Y + playerRect.Height) <= (WindowHeight - playerMoveAmount)))
            //{
            //    playerRect.Y += playerMoveAmount;
            //}

            if (keyboard.IsKeyDown(Keys.Left))
            {
                playerDrawRect.X -= playerMoveAmount;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                playerDrawRect.X += playerMoveAmount;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                playerDrawRect.Y -= playerMoveAmount;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                playerDrawRect.Y += playerMoveAmount;
            }

            if ((playerDrawRect.X + playerDrawRect.Width) < 0)
            {
                playerDrawRect.X = WindowWidth;
            }
            if (playerDrawRect.X > WindowWidth)
            {
                playerDrawRect.X = 0;
            }

            // Move ball
            ball.MoveSprite((int)ballVelocity.X, (int)ballVelocity.Y);

            // Keep ball inside the window
            if (ball.DrawRectangle.X < 0 || (ball.DrawRectangle.X + ball.DrawRectangle.Width) > WindowWidth)
            {
                ballVelocity.X = -ballVelocity.X;
            }
            if (ball.DrawRectangle.Y < 0 || (ball.DrawRectangle.Y + ball.DrawRectangle.Height) > WindowHeight)
            {
                Rectangle rect = ball.DrawRectangle;
                if (rect.Y < 0)
                {
                    rect.Y = 0;
                }
                else if (rect.Y + rect.Height > WindowHeight)
                {
                    rect.Y = WindowHeight - ball.DrawRectangle.Height;
                }

                ball.DrawRectangle = rect;

                ballVelocity.Y = -ballVelocity.Y;
            }

            UpdateEnemy();

            flamemanAnimation.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw the player, bat and the ball
            spriteBatch.Draw(playerSprite, playerDrawRect, Color.White);
            ball.Draw(spriteBatch);
            //robot.Draw(spriteBatch, rotation, Color.Red);
            robot.Draw(spriteBatch, Vector2.Zero, 0, Color.Wheat);
            spriteBatch.Draw(batSprite, batDrawRect, Color.White);

            debug.Draw(spriteBatch, new Vector2(robot.DrawRectangle.X, robot.DrawRectangle.Y));

            // Draw enemy bat
            spriteBatch.Draw(batSprite, enemyBatDrawRect, Color.White);

            // Animation
            flamemanAnimation.Draw(spriteBatch, new Vector2(200, 200), SpriteEffects.None);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetRandomVelocity(int maxSpeed)
        {
            int rndNumX = rnd.Next(-maxSpeed, maxSpeed + 1);
            int rndNumY = rnd.Next(-maxSpeed, maxSpeed + 1);

            return new Vector2(rndNumX, rndNumY);
        }

        private void UpdateEnemy()
        {
            Vector2 distance = (ball.DrawRectangle.Center - enemyBatDrawRect.Center).ToVector2();

            if (distance.Length() > 400)
            {
                if (enemyBatDrawRect.Center.X < WindowWidth / 2)
                {
                    enemyBatDrawRect.X += enemyBatMoveAmount;
                }
                if (enemyBatDrawRect.Center.X > WindowWidth / 2)
                {
                    enemyBatDrawRect.X -= enemyBatMoveAmount;
                }

                return;
            }

            if (ball.DrawRectangle.Center.X - enemyBatMoveAmount > enemyBatDrawRect.Center.X)
            {
                enemyBatDrawRect.X += enemyBatMoveAmount;
            }
            if (ball.DrawRectangle.Center.X - enemyBatMoveAmount < enemyBatDrawRect.Center.X)
            {
                enemyBatDrawRect.X -= enemyBatMoveAmount;
            }

            // Collision detection
            if (enemyBatDrawRect.Intersects(ball.DrawRectangle))
            {
                ballVelocity.Y = -ballVelocity.Y;
            }
        }
    }
}