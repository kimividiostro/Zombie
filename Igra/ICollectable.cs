using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Igra
{
    interface ICollectable
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }

        void Effect(Character character);
    }
}
