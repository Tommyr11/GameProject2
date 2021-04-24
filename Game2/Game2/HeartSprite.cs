using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2
{
    public class HeartSprite
    {
        private Texture2D texture;

        public Vector2 position;

        private double animationTimer;

        private short animationFrame = 0;


        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(144, 128), 144, 128);
        public HeartSprite()
        {

        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("HeartSprite");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

        }
        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,Vector2 temp)
        {
            var source = new Rectangle(40, 50, 180, 150);
            spriteBatch.Draw(texture,temp,source,Color.White,0f,new Vector2(0,0),.5f,SpriteEffects.None,1);
        }

    }
}

