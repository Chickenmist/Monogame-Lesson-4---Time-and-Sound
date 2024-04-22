using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Lesson_4___Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bomb;

        Rectangle bombRect = new Rectangle();

        SpriteFont timeFont;

        float seconds;

        MouseState mouseState;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        bool bombExploded;

        Texture2D explosionTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            bombRect = new Rectangle(50, 50, 700, 400);

            bombExploded = false;

            seconds = 15;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bomb = Content.Load<Texture2D>("bomb");

            timeFont = Content.Load<SpriteFont>("Time");
            
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            

            explosionTexture = Content.Load<Texture2D>("ExplosionTexture");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 15f;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (explodeInstance.State == SoundState.Stopped && bombExploded)
            {
                this.Exit();
            }

            if (seconds > 0)
            {
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                explodeInstance.Play();
                bombExploded = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            if (bombExploded == false)
            {
                _spriteBatch.Draw(bomb, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else
            {
                _spriteBatch.Draw(explosionTexture, bombRect, Color.White);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}