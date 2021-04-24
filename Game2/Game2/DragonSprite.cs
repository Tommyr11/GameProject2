using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;



namespace Game2
{
    public class DragonSprite : Game
    {
        private GamePadState gamePadState;

        private SpriteBatch spriteBatch;

        public Color color { get; set; } = Color.White;

        private KeyboardState keyboardState;

        public int dragonLives = 3;

        private Texture2D texture;

        public Vector2 position = new Vector2(50, 180);

        public bool poweredUp = false;

        private double animationTimer;

        private short animationFrame = 0;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(144,70), 144, 100);
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            if (poweredUp)
            {
                texture = content.Load<Texture2D>("flying_twin_headed_dragon-red");
            }
            else
            {
                texture = content.Load<Texture2D>("flying_dragon-red");
            }
            
            
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);


            // Apply keyboard movement
            if (position.Y > 0 && position.Y < 400 && position.X > 0 && position.X < 800)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector2(0, -1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, 2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector2(-1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(-2, 0);
                    }

                }
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }

                }
            }
            if (position.X >= 800)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector2(0, -1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, 2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector2(-1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(-2, 0);
                    }

                }
            }
            if (position.X <= 0 && position.Y > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector2(0, -1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, 2);
                    }
                }
               
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }

                }
            }
            if (position.X <= 0 && position.Y > 400)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector2(0, -1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
               

                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }

                }
            }
            if (position.X <= 0 && position.Y <= 0)
            {

                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, 2);
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }

                }
            }
            if ( position.Y <= 0 && position.X > 0)
            {

                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector2(0, 1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector2(-1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(-2, 0);
                    }

                }
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }

                }
            }
            if (position.Y >= 400 && position.X > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector2(0, -1);
                    if (poweredUp)
                    {
                        position += new Vector2(0, -2);
                    }
                }
                
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector2(-1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(-2, 0);
                    }

                }
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0);
                    if (poweredUp)
                    {
                        position += new Vector2(2, 0);
                    }
                }
            }
            

            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > .3)
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= .3;
            }
            var source = new Rectangle(animationFrame * 144, 128, 128, 144);
            
            spriteBatch.Draw(texture, position, source, color);
        }
    }
}
