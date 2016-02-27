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
    public class smallShip : Ship
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

        private List<smallBullet> bulletList = new List<smallBullet>();

        public List<smallBullet> BulletList
        {
            get { return bulletList; }
        }

        private Player player;

        private Vector2 origin;

        private float rotation;

        private float shootTimer;

        private float maxDistance;

        private float speed;

        private float[] directions;
        
        public smallShip(Random random, Player player, int speed, int health)
            : base(health)
        {
            this.position = new Vector2(random.Next(-700, -400 + ShipTexSmall.Width), random.Next(-700,ShipTexBig.Height));

            this.speed = speed;

            rect.X = (int) position.X;
            rect.Y = (int) position.Y;

            this.player = player;

            maxDistance = 175;

            directions = new float[]
            {
                ((float)Math.PI / 2) * 0.875f,
                ((float)Math.PI / 2) * 1.125f,
                (float)Math.PI / 2
            };
        }

        public override void load()
        {
            rect.Width = ShipTexSmall.Width;
            rect.Height = ShipTexSmall.Height;

            origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(Vector2.Distance(position, player.Position) < maxDistance)
            {
                Vector2 direction = position - player.Position;
                direction.Normalize();

                rotation = (float)Math.Atan2(-direction.X, direction.Y) - (float)Math.PI;

                if (shootTimer > 1.0f)
                {
                    Random rand = new Random(DateTime.Now.Millisecond);

                    int number = rand.Next(1,4);

                    if(number == 1)
                    {
                        Fire();
                    }
                    else if (number == 2)
                    {
                        FireTwo();
                    }
                    else
                    {
                        FireThree();
                    }
                }
            }

            for(int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();

                if(bulletList[i].OutOfRange)
                {
                    bulletList.RemoveAt(i);
                }
            }

            foreach (smallBullet playerBullet in player.BulletList)
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
            foreach(smallBullet bullet in bulletList)
            {
                bullet.Draw(batch);
            }
            
            batch.Draw(ShipTexSmall, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }

        private bool checkCollision(Rectangle a, Rectangle b)
        {
            if (a.Intersects(b))
                return true;

            return false;
        }

        private void Fire()
        {
            
            smallBullet bullet = new smallBullet(position, rotation + directions[2], 10, 10);
            bulletList.Add(bullet);

            shootTimer = 0.0f;
        }

        private void FireTwo()
        {
            for(int i = 0; i < directions.Length -1; i++)
            {
                smallBullet bullet = new smallBullet(position, rotation + directions[i], 10, 10);
                bulletList.Add(bullet);
            }

            shootTimer = 0.0f;
        }

        private void FireThree()
        {
            foreach(float direction in directions)
            {
                smallBullet bullet = new smallBullet(position, rotation + direction, 10, 10);
                bulletList.Add(bullet);
            }
            
            shootTimer = 0.0f;
        }
    }
}
