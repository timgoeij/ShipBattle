using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace shipgame
{
    public class smallBullet : Bullet
    {
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 startPosition;

        private Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        private bool outOfRange;

        public bool OutOfRange
        {
            get { return outOfRange; }
            set { outOfRange = value; }
        }

        private float maxDistance;

        

        private float rotation;

        private float speed;
        
        public smallBullet(Vector2 position, float rotation, int speed, int damage)
            : base(damage)
        {
            this.position = position;
            this.startPosition = position;
            this.rotation = rotation;
            this.speed = speed;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            rect.Width = BulletTex.Width;
            rect.Height = BulletTex.Height;

            maxDistance = 200;
        }

        public smallBullet(Vector2 position, float maxDistance, float rotation, int speed, int damage)
            : base(damage)
        {
            this.position = position;
            this.startPosition = position;
            this.rotation = rotation;
            this.speed = speed;
            this.maxDistance = maxDistance;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            rect.Width = BulletTex.Width;
            rect.Height = BulletTex.Height;
        }

        public override void Update()
        {
            if(Vector2.Distance(startPosition, position) > maxDistance)
            {
                outOfRange = true;
            }
            
            position.X += speed * (float)Math.Cos(rotation);
            position.Y += speed * (float)Math.Sin(rotation);

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(BulletTex, position, Color.White);
        }
    }
}
