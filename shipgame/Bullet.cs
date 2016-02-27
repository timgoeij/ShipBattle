using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace shipgame
{
    public abstract class Bullet
    {
        private int damage;

        public int Damage
        {
            get { return damage; }
        }
        
        private static Texture2D bulletTex;

        public static Texture2D BulletTex
        {
            get { return bulletTex; }
        }

        private static Texture2D bigBulletTex;

        public static Texture2D BigBulletTex
        {
            get { return bigBulletTex; }
        }
        
        public Bullet(int damage)
        {
            this.damage = damage;
        }

        public abstract void Update();

        public abstract void Draw(SpriteBatch batch);

        public static void setTexture(Texture2D bullet, Texture2D bigBullet)
        {
            bulletTex = bullet;
            bigBulletTex = bigBullet;
        }
    }
}
