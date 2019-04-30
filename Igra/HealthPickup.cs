using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Igra
{
    class HealthPickup : ICollectable
    {
        private Vector2 position;
        private Texture2D texture;
        private Random rand;

        public HealthPickup(Texture2D texture, Vector2 position)
        {
            rand = new Random();
        }

        public Vector2 Position
        {
            get{return position;}
            set{position = value;}
        }

        public Texture2D Texture
        {
            get{return texture;}
            set {texture = value;}
        }

        public void Effect(Character character)
        {
            if (character.Health > 50)
                character.Health += rand.Next(10, 31);
            if (character.Health < 30)
                character.Health += rand.Next(20, 41);
        }
    }
}
