using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shipgame
{    
    public abstract class Ship
    {
        private int maxHealth;

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        private static Texture2D shipTexSmall;

        public static Texture2D ShipTexSmall
        {
            get { return shipTexSmall; }
        }

        private static Texture2D shipTexBig;

        public static Texture2D ShipTexBig
        {
            get { return shipTexBig; }
        }
        
        public Ship(int health) 
        {
            this.maxHealth = health;
        }

        public abstract void load();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch batch);

        public static void setTexture(Texture2D smallShip, Texture2D bigShip)
        {
            shipTexSmall = smallShip;
            shipTexBig = bigShip;
        }
    }
}
