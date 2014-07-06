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
        Collision collision;
        const float METER_FACTOR = 2000.0f; // 1m = 2000 px, to have value in [m] you have to multiply it by METER
        Texture2D ballsSpriteTextureRed, ballsSpriteTextureBlack;
        bool isNextCollisionTimeKnown = false;
        float collisionTimer = 1000.0f;
        const float R_SCALE = 10000.0f;
        int counter = 0;
        SpriteFont font;
        Vector2 fontPos;

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
            List<Ball2> balls = new List<Ball2>();
            // ball-wall and ball-ball test
            balls.Add(new Ball2(new Vector2(0, 0.1f), new Vector2(0.1f, 0.01f), 0.01f, 0.00001f));  // ball-wall vertical test
            balls.Add(new Ball2(new Vector2(0.1f, 0), new Vector2(0.1f, 0.01f), 0.01f, 0.00001f));  // ball-wall horizontal test
            balls.Add(new Ball2(new Vector2(0.1f, 0.2f), new Vector2(0.1f, 0.01f), 0.01f, 0.00001f));  // ball-wall inclined test
            
            // ball-ball horizontal test
            balls.Add(new Ball2(new Vector2(0.05f, 0), new Vector2(0.1f, 0.01f), 0.01f, 0.00001f));
            balls.Add(new Ball2(new Vector2(-0.1f, 0), new Vector2(0.4f, 0.01f), 0.01f, 0.00001f));
             
            // ball-ball vertical test
            balls.Add(new Ball2(new Vector2(0.0f, 0.05f), new Vector2(0.1f, 0.01f), 0.01f, 0.00001f));
            balls.Add(new Ball2(new Vector2(0.0f, -0.1f), new Vector2(0.1f, 0.15f), 0.01f, 0.00001f)); 
             
            List<Wall2> walls = new List<Wall2>();
            walls.Add(new Wall2(new Vector2(0, 0), WallOrientation.Left));
            walls.Add(new Wall2(new Vector2(GraphicsDevice.Viewport.Bounds.Width / METER_FACTOR, 0), WallOrientation.Right));
            walls.Add(new Wall2(new Vector2(0, 0), WallOrientation.Top));
            walls.Add(new Wall2(new Vector2(0, GraphicsDevice.Viewport.Bounds.Height / METER_FACTOR), WallOrientation.Bottom));

            collision = new Collision(balls, walls);

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
            font = this.Content.Load<SpriteFont>("counter");
            fontPos = new Vector2(0, 0);
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isNextCollisionTimeKnown)
            {
                collision.GetNextBallWallCollision();
                collision.GetNextBallBallCollision();
                collisionTimer = collision.NextCollisions[0].Time;
                isNextCollisionTimeKnown = true;
            }
            collisionTimer -= elapsed;
            if (collisionTimer <= 0)
            {
                if ((collision.NextCollisions[0].Obj1.M < Wall2.WALL_MASS && collision.NextCollisions[0].Obj2.M >= Wall2.WALL_MASS) ||
                    (collision.NextCollisions[0].Obj1.M >= Wall2.WALL_MASS && collision.NextCollisions[0].Obj2.M < Wall2.WALL_MASS))
                {
                    collision.CalcPostImpactVBallWall(collision.NextCollisions[0]);
                }
                else
                {
                    collision.CalcPostImpactVBallBall(collision.NextCollisions[0]);
                }
                isNextCollisionTimeKnown = false;
                counter++;
            }

            collision.MoveBallsToTime(elapsed);

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
            foreach (Ball2 b in collision.Balls)
            {
                if (b.Id % 2 == 0)
                {
                    spriteBatch.Draw(ballsSpriteTextureBlack, (b.Coordinates + new Vector2(b.R, b.R)) * METER_FACTOR, null, Color.White, 0.0f, Vector2.Zero, R_SCALE * b.R, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(ballsSpriteTextureRed, (b.Coordinates + new Vector2(b.R, b.R)) * METER_FACTOR, null, Color.White, 0.0f, Vector2.Zero, R_SCALE * b.R, SpriteEffects.None, 0);
                }
            }
            spriteBatch.DrawString(font, counter.ToString(), fontPos, Color.Green);
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
