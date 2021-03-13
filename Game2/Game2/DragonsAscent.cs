using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Game2.StateManagement;
using Game2.Screens;

namespace Game2
{
    public class DragonsAscent : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        public DragonsAscent()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            //_screenManager.AddScreen(new GameplayScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);    // The real drawing happens inside the ScreenManager component
        }
    }
}
        /*
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private bool fireball = true;
        System.Timers.Timer t;
        private KeyboardState keyboardState;
        private PowerupSprite powerup;
        private SpellSprite[] spells;
        private SpellSprite testSpell;
        private DragonSprite dragonSprite;
        private FireballSprite fireballSprite;
        private SpriteFont spriteFont;
        private int coinsLeft;
        private SoundEffect coinPickup;
        private Song backgroundMusic;
        private bool poweredUp = false;


        public DragonsAscent()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            t = new System.Timers.Timer();
            t.Start();
            t.Interval = 1000;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            spells = new SpellSprite[]
            {
                new SpellSprite(),
                new SpellSprite(),
                new SpellSprite()
            };
            powerup = new PowerupSprite();
            dragonSprite = new DragonSprite();
            fireballSprite = new FireballSprite();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            int count = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach(SpellSprite spell in spells)
            {
                spell.LoadContent(Content,count);
                count++;
            }
            powerup.LoadContent(Content);
            dragonSprite.LoadContent(Content);
            fireballSprite.LoadContent(Content);
            if (dragonSprite.poweredUp)
            {
                backgroundMusic = Content.Load<Song>("DragonsAscentTheme");
            }
            else
            {
                backgroundMusic = Content.Load<Song>("DragonsMenu");
            }
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();


            dragonSprite.Update(gameTime);
            fireballSprite.Update(gameTime);
            powerup.Update(gameTime);
            // TODO: Add your update logic here
            foreach (SpellSprite spell in spells)
            {
                spell.Update(gameTime);
                
                if (spell.Bounds.CollidesWith(dragonSprite.Bounds))
                {
                    spell.collision = true;
                    
                    dragonSprite.dragonLives--;
                    
                    
                }
            }
            if (powerup.Bounds.CollidesWith(dragonSprite.Bounds))
            {
                powerup.collision = true;
                if (!poweredUp)
                {
                    dragonSprite.poweredUp = true;
                    LoadContent();
                }
                poweredUp = true;
                
            }





            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Maroon);
            spriteBatch.Begin();
            foreach (SpellSprite spell in spells)
            {
                spell.Draw(gameTime,spriteBatch);
                
            }
            if(fireball)
            {
                
                fireballSprite.position.X = dragonSprite.position.X + 10;
                fireballSprite.position.Y = dragonSprite.position.Y;
                
                
                fireballSprite.Draw(gameTime, spriteBatch);
                if (fireballSprite.position.X > 700) fireball = false;
            }
            powerup.Draw(gameTime, spriteBatch);
            dragonSprite.Draw(gameTime,spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}*/
