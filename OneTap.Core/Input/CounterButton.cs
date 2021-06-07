using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneTap.Core.Input
{
    public class CounterButton
    {

        private readonly Vector2 baseScreenSize;
        private Matrix globalTransformation;
        public readonly Texture2D texture;

        public bool IsPressed = false;
        public CounterButton(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var spriteCenter = new Vector2(64, 64);

            spriteBatch.Draw(texture, new Vector2(64, baseScreenSize.Y - 64), null, Color.White, -MathHelper.PiOver2, spriteCenter, 1, SpriteEffects.None, 0);
        }

        public GamePadState GetState(GraphicsDevice graphicsDevice, TouchCollection touchState, GamePadState gpState)
        {
            //Work out what buttons are pressed based on the touchState
            Buttons buttonsPressed = 0;

            RenderTarget2D RenderTarget = new RenderTarget2D(graphicsDevice, (int)this.texture.Width, (int)this.texture.Height);

            foreach (var touch in touchState)
            {

                if (touch.State == TouchLocationState.Moved || touch.State == TouchLocationState.Pressed)
                {
                    //Scale the touch position to be in _baseScreenSize coordinates
                    Vector2 pos = touch.Position;
                    var virtualX = Convert.ToSingle(pos.X) * Convert.ToSingle(RenderTarget.Width) / Convert.ToSingle(graphicsDevice.Viewport.Width);
                    var virtualY = Convert.ToSingle(pos.Y) * Convert.ToSingle(RenderTarget.Height) / Convert.ToSingle(graphicsDevice.Viewport.Height);

                    if (pos.X < virtualX && pos.Y < virtualY)
                        buttonsPressed |= Buttons.DPadLeft;
                    else if (pos.X < 256)
                        buttonsPressed |= Buttons.DPadRight;
                    else if (pos.X >= baseScreenSize.X - 128)
                        buttonsPressed |= Buttons.A;
                }
            }



            //Combine the buttons of the real gamepad
            var gpButtons = gpState.Buttons;
            buttonsPressed |= (gpButtons.A == ButtonState.Pressed ? Buttons.A : 0);
            buttonsPressed |= (gpButtons.B == ButtonState.Pressed ? Buttons.B : 0);
            buttonsPressed |= (gpButtons.X == ButtonState.Pressed ? Buttons.X : 0);
            buttonsPressed |= (gpButtons.Y == ButtonState.Pressed ? Buttons.Y : 0);

            buttonsPressed |= (gpButtons.Start == ButtonState.Pressed ? Buttons.Start : 0);
            buttonsPressed |= (gpButtons.Back == ButtonState.Pressed ? Buttons.Back : 0);

            buttonsPressed |= gpState.IsButtonDown(Buttons.DPadDown) ? Buttons.DPadDown : 0;
            buttonsPressed |= gpState.IsButtonDown(Buttons.DPadLeft) ? Buttons.DPadLeft : 0;
            buttonsPressed |= gpState.IsButtonDown(Buttons.DPadRight) ? Buttons.DPadRight : 0;
            buttonsPressed |= gpState.IsButtonDown(Buttons.DPadUp) ? Buttons.DPadUp : 0;

            buttonsPressed |= (gpButtons.BigButton == ButtonState.Pressed ? Buttons.BigButton : 0);
            buttonsPressed |= (gpButtons.LeftShoulder == ButtonState.Pressed ? Buttons.LeftShoulder : 0);
            buttonsPressed |= (gpButtons.RightShoulder == ButtonState.Pressed ? Buttons.RightShoulder : 0);

            buttonsPressed |= (gpButtons.LeftStick == ButtonState.Pressed ? Buttons.LeftStick : 0);
            buttonsPressed |= (gpButtons.RightStick == ButtonState.Pressed ? Buttons.RightStick : 0);

            var buttons = new GamePadButtons(buttonsPressed);

            return new GamePadState(gpState.ThumbSticks, gpState.Triggers, buttons, gpState.DPad);
        }
    }
}
