using Game2.Particle;
using Game2.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Game2.Screens
{
    public class GameplayScreen : GameScreen, IParticleEmitter
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Vector2 _playerPosition = new Vector2(100, 100);
        private Vector2 _enemyPosition = new Vector2(100, 100);

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        private readonly Random _random = new Random();

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private bool fireball = true;
        System.Timers.Timer t;
        private KeyboardState keyboardState;
        private PowerupSprite powerup;
        private SpellSprite[] spells;
        private HeartSprite lives = new HeartSprite();
        private SpellSprite testSpell;
        private DragonSprite dragonSprite;
        private FireballSprite fireballSprite;
        private SpriteFont spriteFont;
        private int coinsLeft;
        private SoundEffect coinPickup;
        private Song backgroundMusic;
        private bool poweredUp = false;


        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);
            spells = new SpellSprite[]
            {
                new SpellSprite(),
                new SpellSprite(),
                new SpellSprite()
            };
            powerup = new PowerupSprite();
            dragonSprite = new DragonSprite();
            fireballSprite = new FireballSprite();
            
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("gamefont");

            int count = 0;
            
            foreach (SpellSprite spell in spells)
            {
                spell.LoadContent(_content, count);
                count++;
            }
            lives.LoadContent(_content);
            powerup.LoadContent(_content);
            dragonSprite.LoadContent(_content);
            fireballSprite.LoadContent(_content);
            if (dragonSprite.poweredUp)
            {
                backgroundMusic = _content.Load<Song>("DragonsAscentTheme");
            }
            else
            {
                backgroundMusic = _content.Load<Song>("DragonsMenu");
            }
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            Thread.Sleep(200);

            
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                _enemyPosition.X += (float)(_random.NextDouble() - 0.5) * randomization;
                _enemyPosition.Y += (float)(_random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                var targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - _gameFont.MeasureString("Insert Gameplay Here").X / 2,
                    200);

                _enemyPosition = Vector2.Lerp(_enemyPosition, targetPosition, 0.05f);

                // This game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
            }
            
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
                    Activate();
                }
                poweredUp = true;

            }
            Velocity = dragonSprite.position - Position;
            Position = dragonSprite.position;
            if(dragonSprite.dragonLives == 0)
            {
                //this.Deactivate();
                LoseConditionHelper();
                dragonSprite.dragonLives = 3;

            }




            // TODO: Add your update logic here


        }

        public void LoseConditionHelper()
        {
            ScreenManager.AddScreen(new LoseConditionScreen(), ControllingPlayer);
        }
        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
                
            }
            else
            {
                // Otherwise move the player position.
                var movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                var thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                _playerPosition += movement * 8f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.DarkMagenta, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Avoid the Fireballs and collect the powerup!!!", new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, $"Lives Left: {dragonSprite.dragonLives}", new Vector2(0, 50), Color.White);
            foreach (SpellSprite spell in spells)
            {
                spell.Draw(gameTime, spriteBatch);

            }
            if (fireball)
            {

                fireballSprite.position.X = dragonSprite.position.X + 10;
                fireballSprite.position.Y = dragonSprite.position.Y;


                fireballSprite.Draw(gameTime, spriteBatch);
                if (fireballSprite.position.X > 700) fireball = false;
            }
            powerup.Draw(gameTime, spriteBatch);
            dragonSprite.Draw(gameTime, spriteBatch);
            int temp = dragonSprite.dragonLives;
            Vector2 temppos = new Vector2(520, 0);
            while(temp != 0)
            {
                lives.Draw(gameTime,spriteBatch,temppos);
                temppos.X += 80;
                temp -= 1;
            }
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

    }
}
        
