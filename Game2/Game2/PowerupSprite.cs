using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game2
{
    public class PowerupSprite
    {
        private Texture2D texture1;

        public Texture2D Texture => texture1;

        private double animationTimer;

        private bool flipped;

        private short animationFrame = 5;
        private int count = 0;
        public bool collision = false;

        public Vector2 Position;
        System.Random random = new Random();
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public PowerupSprite()
        {

            Position = new Vector2(800, random.Next(0, 400));

        }
        public void LoadContent(ContentManager content)
        {
            texture1 = content.Load<Texture2D>("sphere_purple");
        }
        public void Update(GameTime gameTime)
        {
            if (Position.X >= 30)
            {
                Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Position.X = 800;
                Position.Y = random.Next(0, 400);
            }
            if (collision)
            {
                Position += new Vector2(0, 0);
                
            }
            bounds.X = Position.X;
            bounds.Y = Position.Y;

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (collision) return;
            
            
            
            var source = new Rectangle(animationFrame * 128, 128, 128, 128);
            spriteBatch.Draw(texture1, Position, source, Color.White, 0f, new Vector2(64, 64), (float).5, SpriteEffects.None, 0);
            
           
        }
    }
}
