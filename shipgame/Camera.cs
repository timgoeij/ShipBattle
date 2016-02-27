using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace shipgame
{
    public class Camera
    {
        private Viewport view;
        private Vector2 center;
        private Matrix tranformMatrix;

        public Matrix Transform
        {
            get {return tranformMatrix;}
        }
        
        public Camera(Viewport view)
        {
            this.view = view;
        }
        
        public void update(GameTime gameTime, Player ship)
        {
            this.center = new Vector2(ship.Position.X + (ship.Rect.Width / 2) - (view.Width / 2), ship.Position.Y + (ship.Rect.Height / 2) - (view.Height / 2));

            if(center.X <= -700)
            {
                center.X = -700;
            }
            else if(center.X + view.Width >= 700)
            {
                center.X = 700 - view.Width;
            }
            
            if(center.Y <= -700)
            {
                center.Y = -700;
            }
            else if(center.Y + view.Height >= 700)
            {
                center.Y = 700 - view.Height;
            }

            tranformMatrix = Matrix.CreateTranslation(new Vector3(-this.center.X,-this.center.Y, 0)) * Matrix.CreateScale(new Vector3(1, 1, 0)); 
        }

    }
}
