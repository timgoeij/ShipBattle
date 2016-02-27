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
    public class Player : Ship
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

        private Texture2D playerTex;

        public Texture2D PlayerTex
        {
            get { return playerTex; }
            set { playerTex = value; }
        }

        private List<smallBullet> bulletList = new List<smallBullet>();

        public List<smallBullet> BulletList
        {
            get { return bulletList; }
        }

        private List<Ship> ships = new List<Ship>();

        public List<Ship> Ships
        {
            set { ships = value; }
        }

        private Vector2 origin;

        private float rotation;

        private float shootTimer;

        private float[] directions;

        private float speed;

        private int health;

        public Player(Vector2 position, int speed, int health)
            : base(health)
        {
            this.position = position;

            this.speed = speed;

            this.health = health;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            directions = new float[]
            {
                0f,
                (float)Math.PI / 4,
                (float)Math.PI / 8,
                -(float)Math.PI / 4,
                -(float)Math.PI / 8,
                (float)Math.PI,
                (float)Math.PI * 1.25f,
                (float)Math.PI * 1.125f,
                (float)Math.PI * 0.75f,
                (float)Math.PI * 0.875f
            };
        }

        public override void load()
        {
            rect.Height = playerTex.Height;
            rect.Width = playerTex.Width;

            origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            playerInput();

            shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for(int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
                
                if(bulletList[i].OutOfRange)
                {
                    bulletList.RemoveAt(i);
                }
            }

            if(health <= 0)
            {
                Reset();
            }

            foreach(Ship ship in ships)
            {
                if(ship as smallShip != null)
                {
                    smallShip small = (smallShip)ship;

                    if(checkCollision(rect, small.Rect))
                    {
                        Reset();
                        small.MaxHealth -= 50;
                    }

                    foreach(smallBullet bullet in small.BulletList)
                    {
                        if(checkCollision(rect, bullet.Rect))
                        {
                            health -= bullet.Damage;
                            bullet.OutOfRange = true;
                        }
                    }
                }
                else if(ship as BigShip != null)
                {
                    BigShip big = (BigShip)ship;

                    if(checkCollision(rect, big.Rect))
                    {
                        Reset();
                        big.MaxHealth -= 50;
                    }

                    foreach(BigBullet bullet in big.BulletList)
                    {
                        if (checkCollision(rect, bullet.Rect))
                        {
                            health -= bullet.Damage;
                            bullet.OutOfRange = true;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (smallBullet bullet in bulletList)
            {
                bullet.Draw(batch);
            }
            
            batch.Draw(playerTex, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        private bool checkCollision(Rectangle a, Rectangle b)
        {
            if (a.Intersects(b))
                return true;

            return false;
        }

        private void playerInput()
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Up))
            {
                position.Y += speed * (float)Math.Sin(rotation - Math.PI / 2);
                position.X += speed * (float)Math.Cos(rotation - Math.PI / 2);

                if (position.X <= -700)
                {
                    position.X = -700;
                }
                else if (position.X + rect.Width >= 700)
                {
                    position.X = 700 - rect.Width;
                }

                if (position.Y <= -700)
                {
                    position.Y = -700;
                }
                else if (position.Y + rect.Height >= 700)
                {
                    position.Y = 700 - rect.Height;
                }

                rect.X = (int)position.X;
                rect.Y = (int)position.Y;
            }

            if (kbs.IsKeyDown(Keys.Left))
            {
                rotation -= 0.1f;
            }
            else if (kbs.IsKeyDown(Keys.Right))
            {
                rotation += 0.1f;
            }

            if (kbs.IsKeyDown(Keys.Space))
            {
                Fire();
            }

            if (kbs.IsKeyDown(Keys.Enter))
            {
                FireBullets();
            }
        }

        private void Reset()
        {
            position = Vector2.Zero;
            health = MaxHealth;
        }

        private void Fire()
        {   
            if(shootTimer > 1.0f)
            {
                smallBullet bullet = new smallBullet(position, rotation - (float)Math.PI / 2, 10, 10);
                bulletList.Add(bullet);
                
                shootTimer = 0.0f;
            }
        }

        private void FireBullets()
        {
            if(shootTimer > 1.0f)
            {
                foreach(float direction in directions)
                {
                    smallBullet bullet = new smallBullet(position, 150, rotation - direction, 10, 10);
                    bulletList.Add(bullet);
                }

                shootTimer = 0.0f;
            }
        }
    }
}