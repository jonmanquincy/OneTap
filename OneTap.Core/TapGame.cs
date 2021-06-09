using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OneTap.Core.Objects;
using OneTap.Core.Input;
using Microsoft.Xna.Framework.Audio;

namespace OneTap
{
    public class TapGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private CounterButton button;

        private Viewport viewport;
        public TapGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.SupportedOrientations = DisplayOrientation.Portrait;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            button = new CounterButton(Content.Load<Texture2D>("red_button_logo"), Content.Load<Texture2D>("red_button_logo_pressed_2"),
                                       Content.Load<SoundEffect>("thud_analog_wave_at_betchy_jon"), this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            button.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Color backgroundColor = new Color(51, 57, 55);
            GraphicsDevice.Clear(backgroundColor);
            viewport = _graphics.GraphicsDevice.Viewport;
            Rectangle relPos = new Rectangle((int)(0.25 * viewport.Width), (int)(0.35 * viewport.Height), (int)(viewport.Width *.5), (int)(viewport.Height * .3));
            button.SetRectangle(relPos);

            _spriteBatch.Begin();
            _spriteBatch.Draw(button.texture, relPos, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
