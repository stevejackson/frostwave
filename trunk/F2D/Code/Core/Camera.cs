using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D;
using F2D.Core;
using F2D.Graphics;

namespace F2D.Core
{
    static public class Camera
    {
        static public Rectangle rect;

        static public Vector2 Position
        {
            get { return new Vector2(rect.X, rect.Y); }
        }

        static public Vector2 Size
        {
            get { return new Vector2(rect.Width, rect.Height); }
        }

        static public void Initialize(Vector2 startPos, Vector2 startSize)
        {
            rect = new Rectangle((int)startPos.X, (int)startPos.Y, 
                                 (int)startSize.X, (int)startSize.Y);

        }       
      
        static public void Follow(Sprite targetSprite)
        {
            MoveTo(targetSprite.Position - (Size / 2));
        }

        static public void MoveTo(Vector2 destination)
        {
            Vector2 delta = (destination - Position) * Director.Scale;

            MoveDelta(delta);
        }

        static public void MoveDelta(Vector2 deltaValue)
        {
            rect.X += (int)deltaValue.X;
            rect.Y += (int)deltaValue.Y;

            if (rect.Right > Grid.MapRect.Right)
            {
                rect.X = Grid.MapRect.Right - rect.Width;
            }
            if (rect.Left < Grid.MapRect.Left)
            {
                rect.X = Grid.MapRect.Left;
            }
            if (rect.Bottom > Grid.MapRect.Bottom)
            {
                rect.Y = Grid.MapRect.Bottom - rect.Height;
            }
            if (rect.Top < Grid.MapRect.Top)
            {
                rect.Y = Grid.MapRect.Top;
            }
        }	

    }
}
