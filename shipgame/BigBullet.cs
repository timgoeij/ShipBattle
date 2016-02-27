using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shipgame
{
    public class BigBullet : Bullet
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

        private Vector2 origin;
        
        private Vector2 direction;
        
        private float maxDistance;

        private float rotation;

        private float speed;

        public BigBullet(Vector2 position, Vector2 direction, float maxDistance, float rotation, int speed, int damage)
            : base(damage)
        {
            this.position = position;
            this.startPosition = position;
            this.rotation = rotation;
            this.speed = speed;
            this.maxDistance = maxDistance;
            this.direction = direction;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            rect.Width = BigBulletTex.Width;
            rect.Height = BigBulletTex.Height;

            origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        public override void Update()
        {
            if (Vector2.Distance(startPosition, position) > maxDistance)
            {
                outOfRange = true;
            }

            position += direction * speed;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(BigBulletTex, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
