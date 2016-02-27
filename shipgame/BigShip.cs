using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shipgame
{
    public class BigShip : Ship
    {
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        private List<BigBullet> bulletList = new List<BigBullet>();

        public List<BigBullet> BulletList
        {
            get { return bulletList; }
        }

        private Player player;

        private Vector2 origin;
        
        private Vector2 direction;

        private float rotation;

        private float shootTimer;

        private float maxDistance;
        private float minDistance;

        private float speed;

        private float[] directions;

        public BigShip(Random random, Player player, int speed, int health)
            : base(health)
        {
            this.position = new Vector2(random.Next(400, 700 - ShipTexBig.Width), random.Next(-700, ShipTexBig.Height));

            this.speed = speed;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            this.player = player;

            maxDistance = 400;
            minDistance = 100;

            directions = new float[]
            {
                ((float)Math.PI / 2) * 0.875f,
                ((float)Math.PI / 2) * 1.125f,
                (float)Math.PI / 2
            };
        }

        public override void load()
        {
            rect.Width = ShipTexBig.Width;
            rect.Height = ShipTexBig.Width;

            origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            direction = player.Position - position;
            direction.Normalize();

            shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Vector2.Distance(position, player.Position) < maxDistance - minDistance)
            {
                rotation = (float)Math.Atan2(-direction.X, direction.Y);

                if(Vector2.Distance(position, player.Position) > minDistance + rect.Width)
                {
                    position += direction * speed;

                    rect.X = (int)position.X;
                    rect.Y = (int)position.Y;
                }

                if(shootTimer > 1.0f)
                {
                    Fire();
                }
            }
            else if(Vector2.Distance(position, player.Position) > maxDistance - minDistance &&
                Vector2.Distance(position, player.Position) < maxDistance)
            {
                rotation = (float)Math.Atan2(-direction.X, direction.Y) - (float)Math.PI / 2;

                if(shootTimer > 0.20f)
                {
                    Fire();
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();

                if (bulletList[i].OutOfRange)
                {
                    bulletList.RemoveAt(i);
                }
            }

            foreach(smallBullet playerBullet in player.BulletList)
            {
                if (checkCollision(rect, playerBullet.Rect))
                {
                    MaxHealth -= playerBullet.Damage;
                    playerBullet.OutOfRange = true;
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (BigBullet bullet in bulletList)
            {
                bullet.Draw(batch);
            }
            
            batch.Draw(ShipTexBig, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }

        private bool checkCollision(Rectangle a, Rectangle b)
        {
            if (a.Intersects(b))
                return true;

            return false;
        }

        private void Fire()
        {
            BigBullet bullet = new BigBullet(position, direction, 500, rotation, 10, 25);
            bulletList.Add(bullet);

            shootTimer = 0.0f;
        }
    }
}