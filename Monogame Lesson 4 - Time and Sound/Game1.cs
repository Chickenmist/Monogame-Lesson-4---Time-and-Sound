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
        Rectangle bombDisplay = new Rectangle();
        Rectangle bombButton = new Rectangle();
        Rectangle greenWire = new Rectangle();
        Rectangle redWire = new Rectangle();
        Rectangle bombRearmButton = new Rectangle();

        SpriteFont timeFont;

        float seconds;

        MouseState mouseState;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        bool bombExploded;
        bool bombDefused;

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

            bombExploded = false;
            bombDefused = false;

            seconds = 15;
            
            bombRect = new Rectangle(50, 50, 700, 400);
            bombDisplay = new Rectangle(245, 160, 217, 173);
            bombButton = new Rectangle(255, 135, 5, 10);
            greenWire = new Rectangle(491, 164, 26, 11);
            redWire = new Rectangle(491, 190, 26, 14);
            bombRearmButton = new Rectangle(486, 224, 4, 53);

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
            this.Window.Title = mouseState.X + ", " + mouseState.Y;
            if (mouseState.LeftButton == ButtonState.Pressed && bombDisplay.Contains(mouseState.Position) && bombDefused == false || mouseState.LeftButton == ButtonState.Pressed && bombButton.Contains(mouseState.Position) && bombDefused == false)
            {
                seconds = 15f;
            }
            
            if (mouseState.LeftButton == ButtonState.Pressed && greenWire.Contains(mouseState.Position))
            {
                bombDefused = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && redWire.Contains(mouseState.Position))
            {
                seconds = 0f;
                bombDefused = false;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && bombRearmButton.Contains(mouseState.Position) && bombDefused == true)
            {
                bombDefused = false;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (explodeInstance.State == SoundState.Stopped && bombExploded)
            {
                this.Exit();
            }

            if (seconds > 0 && bombDefused == false)
            {
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (seconds == 0 && bombDefused == false)
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