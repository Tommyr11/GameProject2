using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game2
{
    public class SpellSprite
    {
        private Texture2D texture1;

        public Texture2D Texture => texture1;

        private double animationTimer;

        private bool flipped;

        private short animationFrame = 0;
        private int count = 0;
        public bool collision;

        public Vector2 Position;
        System.Random random = new Random();
        private BoundingRectangle bounds;
        public BoundingRectangle Bounds => bounds;
        public SpellSprite()
        {
            
            Position = new Vector2(800, random.Next(0, 380));
            
        }

        public void LoadContent(ContentManager content, int type)
        {
            texture1 = content.Load<Texture2D>("fire_green");
            if(type == 1)
            {
                texture1 = content.Load<Texture2D>("fire_purple");
            }
            if(type == 2)
            {
                texture1 = content.Load<Texture2D>("fire_yellow");
            }
           
            
        }
        public void Update(GameTime gameTime)
        {
            if(Position.X >= 30)
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
                flipped = true;
                Position += new Vector2(2, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(Position.X > 800)
                {
                    collision = false;
                    flipped = false;
                    Position.Y = random.Next(0, 400);
                }
            }
            bounds.X = Position.X ;
            bounds.Y = Position.Y;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float flip = 1.5f;
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > .4)
            {
                animationFrame++;
                if (animationFrame > 5) animationFrame = 0;
                animationTimer -= .4;
            }
            if (flipped)
            {
                flip = 4.5f;
            }
            var source = new Rectangle(animationFrame * 95, 192, 95, 192);
            spriteBatch.Draw(texture1, Position, source, Color.White, flip,new Vector2(64,64),(float).5,SpriteEffects.None,0);
            count++;
            if (count == 3) count = 0;
            //spriteBatch.Draw(texture2, Position, source, Color.White);
            //spriteBatch.Draw(texture3, Position, source, Color.White);
        }
    }
}
