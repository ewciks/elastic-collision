using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CollisionLibrary;

namespace collision
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKeyboardState;

        Collision collision;
        bool isNextCollisionTimeKnown;
        float collisionTimer;

        // printing balls
        Texture2D ballsSpriteTextureRed, ballsSpriteTextureBlack, ballsSpriteTextureYellow;
        const float METER_TO_PIXEL_FACTOR = 2000.0f; // 1m = 2000 px, to have value in [m] you have to multiply it by METER_TO_PIXEL_FACTOR
        const float R_SCALE = 40.0f;    // for 100x100 px ball picture to have 1x1m picutre you have to multiply it by R_SCALE
        int ballsStartingPositionOption = 0;
        const int BALLS_STARTING_POSITIONS = 6;

        // printing bottom panel with data
        Texture2D options;
        Vector2 optionsPos;
        Vector2 optionsSize;
        int counter;
        float kin_energy;
        SpriteFont font;
        Vector2 fontPos;
        string nextCollision;
        int selectedBallId;
        int selectedBall;
        string optionsBallPositionsString;

        // pause printing
        bool isRunning;
        Texture2D pause;
        Vector2 pausePos;
        Vector2 pauseSize;
        Vector2 pauseStringPos;
        const string pauseString = "PAUSE (press: )\n space - resume\n tab - switch balls\n page up/down - switch ball starting positions";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Perfect elastic collisions 2D";
            this.IsMouseVisible = true;
            Mouse.WindowHandle = Window.Handle;
        }

        private void UpdateKeyboardInput()
        {
            KeyboardState newState = Keyboard.GetState();
            // Is the SPACE key down?
            if (newState.IsKeyDown(Keys.Space))
            {
                // If not down last update, key has just been pressed.
                if (!oldKeyboardState.IsKeyDown(Keys.Space))
                {
                    if (isRunning) isRunning = false;
                    else isRunning = true;
                }
            }
            else if (newState.IsKeyDown(Keys.Tab))
            {
                if (!oldKeyboardState.IsKeyDown(Keys.Tab))
                {
                    selectedBall = (selectedBall + 1) % collision.Balls.Count;
                    selectedBallId = collision.Balls[selectedBall].Id;
                }
            }
            else if (newState.IsKeyDown(Keys.PageDown))
            {
                if (!oldKeyboardState.IsKeyDown(Keys.PageDown))
                {
                    ballsStartingPositionOption = (ballsStartingPositionOption + 1) % BALLS_STARTING_POSITIONS;
                    StartNewSimulation();
                }
            }
            else if (newState.IsKeyDown(Keys.PageUp))
            {
                if (!oldKeyboardState.IsKeyDown(Keys.PageUp))
                {
                    ballsStartingPositionOption = (ballsStartingPositionOption - 1) % BALLS_STARTING_POSITIONS;
                    StartNewSimulation();
                }
            }
            /*else if (newState.IsKeyDown(Keys.Enter) && ballsStartingPositionOption == 5)    // manual input
            {
                if (!oldKeyboardState.IsKeyDown(Keys.Enter))
                {
                    collision.Balls.Add(new Ball2(Vector2.Zero, new Vector2(mouseState.X/1000.0f, mouseState.Y/1000.0f), 0.01f, 0.01f));
                }
            }*/
            // Update saved state.
            oldKeyboardState = newState;
        }

        private void CalcAndSetNextCol()
        {
            collision.GetNextBallWallCollision();
            collision.GetNextBallBallCollision();
            if (collision.NextCollisions.Count > 0)
            {
                collisionTimer = collision.NextCollisions[0].Time;
                isNextCollisionTimeKnown = true;
                // setting next collision string
                nextCollision = "";
                foreach (NextCollision nc in collision.NextCollisions)
                {
                    nextCollision += " / " + nc.ToString();
                }
            }
            else
            {
                isNextCollisionTimeKnown = false;
                nextCollision = " / unknown";
            }
        }

        #region balls starting positions
        private void BallBallHorizontalTest(List<Ball2> balls)
        {
            balls.Add(new Ball2(new Vector2(0.05f, 0), new Vector2(0.1f, 0.01f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, 0), new Vector2(0.2f, 0.01f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.1f, 0), new Vector2(0.1f, 0.1f), 0.02f, 0.02f));
            balls.Add(new Ball2(new Vector2(-0.1f, 0), new Vector2(0.35f, 0.1f), 0.01f, 0.01f));
            optionsBallPositionsString = "Horizontal collision.";
        }

        private void BallBallVerticalTest(List<Ball2> balls)
        {
            balls.Add(new Ball2(new Vector2(0.0f, 0.05f), new Vector2(0.1f, 0.01f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, -0.1f), new Vector2(0.1f, 0.15f), 0.01f, 0.01f));
            optionsBallPositionsString = "Vertical collision.";
        }

        private void BallBallInclinedTest(List<Ball2> balls)
        {
            balls.Add(new Ball2(new Vector2(0.05f, 0.05f), new Vector2(0.1f, 0.01f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-0.1f, -0.1f), new Vector2(0.19f, 0.1f), 0.01f, 0.01f));
            optionsBallPositionsString = "Inclined collision.";
        }

        private void BallBallTripleColTest(List<Ball2> balls)
        {
            balls.Add(new Ball2(new Vector2(0.1f, 0.0f), new Vector2(0.01f, 0.11f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-0.1f, 0.0f), new Vector2(0.21f, 0.11f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, 0.1f), new Vector2(0.11f, 0.01f), 0.01f, 0.01f));
            optionsBallPositionsString = "Triple collision.";
        }

        private void BillardStartTest(List<Ball2> balls)
        {
            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.04f, 0.08f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.04f, 0.1f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.04f, 0.12f), 0.01f, 0.01f));

            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.06f, 0.09f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.06f, 0.11f), 0.01f, 0.01f));

            balls.Add(new Ball2(new Vector2(0.0f, 0.0f), new Vector2(0.06f + 0.02f, 0.10f), 0.01f, 0.01f));

            balls.Add(new Ball2(new Vector2(-0.4f, 0.0f), new Vector2(0.3f, 0.10f), 0.01f, 0.01f));
            optionsBallPositionsString = "Likewise billard start collision.";
        }

        private void BallsManualInput()
        {
            optionsBallPositionsString = "Manual input.";
        }

        private List<Ball2> AddBalls(int option)
        {
            List<Ball2> balls = new List<Ball2>();
            switch (option)
            {
                case 0:
                    BallBallHorizontalTest(balls);
                    break;
                case 1:
                    BallBallVerticalTest(balls);
                    break;
                case 2:
                    BallBallInclinedTest(balls);
                    break;
                case 3:
                    BallBallTripleColTest(balls);
                    break;
                case 4:
                    BillardStartTest(balls);
                    break;
                case 5:
                    BallsManualInput();
                    break;
                default:
                    BallsManualInput();
                    break;
            }
            return balls;
        }
        #endregion balls starting positions

        #region walls starting positions
        private List<Wall2> AddWallsAsRectangle()
        {
            List<Wall2> walls = new List<Wall2>();
            walls.Add(new Wall2(new Vector2(0.0f, 0.0f), WallOrientation.Left));
            walls.Add(new Wall2(new Vector2(GraphicsDevice.Viewport.Bounds.Width / METER_TO_PIXEL_FACTOR, 0.0f), WallOrientation.Right));
            walls.Add(new Wall2(new Vector2(0.0f, 0.0f), WallOrientation.Top));
            walls.Add(new Wall2(new Vector2(0.0f, 0.8f * GraphicsDevice.Viewport.Bounds.Height / METER_TO_PIXEL_FACTOR), WallOrientation.Bottom));
            return walls;
        }
        #endregion

        private void StartNewSimulation()
        {
            isNextCollisionTimeKnown = false;
            collisionTimer = 1000.0f;
            counter = 0;
            kin_energy = 0.0f;
            isRunning = false;
            selectedBall = 0;

            List<Ball2> balls = AddBalls(ballsStartingPositionOption);
            List<Wall2> walls = AddWallsAsRectangle();

            collision = new Collision(balls, walls);
            if (collision.Balls.Count > 0) selectedBallId = collision.Balls[0].Id;
            else selectedBallId = 0;
            kin_energy = collision.CalcTotalKineticEnergy();
            CalcAndSetNextCol();
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
            StartNewSimulation();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>cre
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballsSpriteTextureRed = this.Content.Load<Texture2D>("red_circle_100px");
            ballsSpriteTextureBlack = this.Content.Load<Texture2D>("black_circle_100px");
            ballsSpriteTextureYellow = this.Content.Load<Texture2D>("yellow_circle_100px");
            options = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            options.SetData<Color>(new Color[] { Color.White });
            optionsPos = new Vector2(0, (int)(0.8 * GraphicsDevice.Viewport.Bounds.Height));
            optionsSize = new Vector2(GraphicsDevice.Viewport.Bounds.Width, (int)(0.2 * GraphicsDevice.Viewport.Bounds.Height));
            font = this.Content.Load<SpriteFont>("counter");
            fontPos = new Vector2(0, (int)(0.8 * GraphicsDevice.Viewport.Bounds.Height));
            pause = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pause.SetData<Color>(new Color[] { Color.White });
            pausePos = new Vector2(0, (int)(0.35 * GraphicsDevice.Viewport.Bounds.Height));
            pauseSize = new Vector2((int)GraphicsDevice.Viewport.Bounds.Width, 6 * font.LineSpacing);
            pauseStringPos = new Vector2((int)GraphicsDevice.Viewport.Bounds.Width / 2, (int)(0.4 * GraphicsDevice.Viewport.Bounds.Height + 1.5 * font.LineSpacing)) - font.MeasureString(pauseString) / 2;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            UpdateKeyboardInput();
            if (isRunning)
            {
                // TODO: Add your update logic here
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                collisionTimer -= elapsed;
                collision.MoveBallsToTime(elapsed);
                if (isNextCollisionTimeKnown && collisionTimer <= 0)
                {
                    collision.MoveBallsToTime(collisionTimer);
                    foreach (NextCollision nc in collision.NextCollisions)
                    {
                        if ((nc.Obj1.M < Wall2.WALL_MASS && nc.Obj2.M >= Wall2.WALL_MASS) ||
                            (nc.Obj1.M >= Wall2.WALL_MASS && nc.Obj2.M < Wall2.WALL_MASS))
                        {
                            collision.CalcPostImpactVBallWall(nc);
                        }
                        else
                        {
                            collision.CalcPostImpactVBallBall(nc);
                        }
                        counter++;
                    }
                    isNextCollisionTimeKnown = false;
                    kin_energy = collision.CalcTotalKineticEnergy();
                    CalcAndSetNextCol();
                }
            }
         
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // all balls black drawing
            foreach (Ball2 b in collision.Balls)
            {
                Vector2 position = b.Coordinates * METER_TO_PIXEL_FACTOR;
                Vector2 origin = new Vector2(b.R * METER_TO_PIXEL_FACTOR, b.R * METER_TO_PIXEL_FACTOR);
                float scale = R_SCALE * b.R;
                spriteBatch.Draw(ballsSpriteTextureBlack, position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0);
                if (b.Id == selectedBallId)
                {
                    spriteBatch.Draw(ballsSpriteTextureYellow, position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0);
                }
            }
            // next collision marker - red
            foreach (NextCollision nc in collision.NextCollisions)
            {
                if (nc.Obj1.M < Wall2.WALL_MASS && nc.Obj2.M < Wall2.WALL_MASS)
                {
                    Ball2 b1 = (Ball2)nc.Obj1;
                    Ball2 b2 = (Ball2)nc.Obj2;
                    Vector2 position1 = b1.Coordinates * METER_TO_PIXEL_FACTOR;
                    Vector2 origin1 = new Vector2(b1.R * METER_TO_PIXEL_FACTOR, b1.R * METER_TO_PIXEL_FACTOR);
                    float scale1 = R_SCALE * b1.R;
                    Vector2 position2 = b2.Coordinates * METER_TO_PIXEL_FACTOR;
                    Vector2 origin2 = new Vector2(b2.R * METER_TO_PIXEL_FACTOR, b2.R * METER_TO_PIXEL_FACTOR);
                    float scale2 = R_SCALE * b2.R;
                    spriteBatch.Draw(ballsSpriteTextureRed, position1, null, Color.White, 0.0f, origin1, scale1, SpriteEffects.None, 0);
                    spriteBatch.Draw(ballsSpriteTextureRed, position2, null, Color.White, 0.0f, origin2, scale2, SpriteEffects.None, 0);
                }
            }
            //selected ball - yellow
            foreach (Ball2 b in collision.Balls)
            {
                if (b.Id == selectedBallId)
                {
                    Vector2 position = b.Coordinates * METER_TO_PIXEL_FACTOR;
                    Vector2 origin = new Vector2(b.R * METER_TO_PIXEL_FACTOR, b.R * METER_TO_PIXEL_FACTOR);
                    float scale = R_SCALE * b.R;
                    spriteBatch.Draw(ballsSpriteTextureYellow, position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(options, new Rectangle((int)optionsPos.X, (int)optionsPos.Y, (int)optionsSize.X, (int)optionsSize.Y), Color.WhiteSmoke);
            spriteBatch.DrawString(font, "Starting position: " + optionsBallPositionsString, fontPos, Color.Green);
            spriteBatch.DrawString(font, "Collisions number: " + counter.ToString(), fontPos + new Vector2(0, font.LineSpacing), Color.Green);
            spriteBatch.DrawString(font, "Total kinetic energy: " + kin_energy.ToString() + "[J]", fontPos + new Vector2(0, 2 * font.LineSpacing), Color.Green);
            spriteBatch.DrawString(font, "Next collision:" + nextCollision, fontPos + new Vector2(0, 3 * font.LineSpacing), Color.Green);
            if(collision.Balls.Count > 0) spriteBatch.DrawString(font, "Selected ball: " + collision.Balls.Find(b => b.Id == selectedBallId).ToString(), fontPos + new Vector2(0, 4 * font.LineSpacing), Color.Green);
            if (!isRunning)
            {
                spriteBatch.Draw(pause, new Rectangle((int)pausePos.X, (int)pausePos.Y, (int)pauseSize.X, (int)pauseSize.Y), Color.Gray * 0.5f);
                spriteBatch.DrawString(font, pauseString, pauseStringPos, Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
