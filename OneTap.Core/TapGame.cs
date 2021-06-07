using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OneTap.Core.Input;

namespace OneTap
{
    public class TapGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TouchCollection touchState;
        private CounterButton button;
        private GamePadState gamePadState;

        Vector2 baseScreenSize = new Vector2(1080, 1920);
        float backbufferWidth, backbufferHeight, horScaling, verScaling;
        private Matrix globalTransformation;

        private Viewport viewport;
        public TapGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.SupportedOrientations = DisplayOrientation.Portrait | DisplayOrientation.PortraitDown | DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            button = new CounterButton(Content.Load<Texture2D>("red_button"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Handle polling for our input and handling high-level input

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Color backgroundColor = new Color(51, 57, 55);
            GraphicsDevice.Clear(backgroundColor);
            viewport = _graphics.GraphicsDevice.Viewport;
            var screenCenter = new Vector2(viewport.X / 2f, viewport.Y / 2f);
            Vector2 imageCenter = new Vector2(button.texture.Width / 2f, button.texture.Height / 2f);
            Rectangle relPos = new Rectangle((int)(0.35 * viewport.Width), (int)(0.35 * viewport.Height), (int)(viewport.Width *.3), (int)(viewport.Height * .3));
            System.Diagnostics.Debug.WriteLine(relPos);

            _spriteBatch.Begin();
            _spriteBatch.Draw(button.texture, relPos, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ScalePresentationArea()
        {
            //Work out how much we need to scale our graphics to fill the screen
            backbufferWidth = GraphicsDevice.PresentationParameters.Bounds.Width;
            backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            horScaling = backbufferWidth / baseScreenSize.X;
            verScaling = backbufferHeight / baseScreenSize.Y;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            globalTransformation = Matrix.CreateScale(screenScalingFactor);
            System.Diagnostics.Debug.WriteLine("Screen Size - Width[" + GraphicsDevice.PresentationParameters.BackBufferWidth + "] Height [" + GraphicsDevice.PresentationParameters.BackBufferHeight + "]");
            System.Diagnostics.Debug.WriteLine(button.texture.Bounds);
        }
        private void HandleInput()
        {
            // get all of our input states
            touchState = TouchPanel.GetState();
            //gamePadState = button.GetState(touchState, GamePad.GetState(PlayerIndex.One));
        }
    }
}
