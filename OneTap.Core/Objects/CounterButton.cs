using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OneTap.Core.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneTap.Core.Objects
{
    public class CounterButton : GameComponent
    {
        private Texture2D TextureIdle;
        private Texture2D TexturePressed;
        private SoundEffect SFX;
        public Texture2D texture;
        public Rectangle ButtonArea { get; set; }
        TouchCollection touchLocations { get; set; }
        public CounterButton(Texture2D textureIdle, Texture2D texturePressed, SoundEffect sfx, Game game) : base(game)
        {
            this.TexturePressed = texturePressed;
            this.TextureIdle = textureIdle;
            this.SFX = sfx;
            texture = TextureIdle;
        }

        public void SetRectangle(Rectangle rectangle)
        {
            ButtonArea = rectangle;
        }

        public override void Update(GameTime gameTime)
        {
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Hold;
            touchLocations = TouchPanel.GetState();
            if (InputManager.CheckTouch(ButtonArea, touchLocations))
            {
                if (texture == TextureIdle)
                {
                    texture = TexturePressed;
                    SFX.Play();
                }
            }
            else if (texture == TexturePressed)
            {
                texture = TextureIdle;
            }
        }
    }
}
