using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Igra
{
    public abstract class Weapon
    {
        public string Name { get; set; }
        public Texture2D[] Textures { get; set; }
        public int Damage { get; set; }
        public bool IsEquipped;
        public Vector2 Position { get; set; }
    }
}
