using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2
{
    public class FireballSprite
    {
        private Texture2D texture;

        public Vector2 position;

        private double animationTimer;

        private short animationFrame = 0;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(144, 128), 144, 128);
        public FireballSprite()
        {
            
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("fireball");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (position.X <= 800)
            {
                position.X += position.X   * 5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            

        }
        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(this.position.X < 800)
            {
                if (animationTimer > .1)
                {
                    animationFrame++;
                    if (animationFrame > 4) animationFrame = 0;
                    animationTimer -= .1;

                }
                var source = new Rectangle(animationFrame * 500, 384, 384, 500);

                spriteBatch.Draw(texture, position, source, Color.White, 0f, new Vector2(64, 64), .5f, SpriteEffects.None, 1);
            }
            
        }
    }
}
 