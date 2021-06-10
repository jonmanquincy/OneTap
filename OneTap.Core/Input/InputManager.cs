using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneTap.Core.Input
{
    public static class InputManager
    {
        public static  bool CheckTouch(Rectangle target, TouchCollection touchCollection)
        {
            if (touchCollection.Count > 0)
            {
                foreach (var touch in touchCollection)
                {
                    if (target.Contains(touch.Position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
