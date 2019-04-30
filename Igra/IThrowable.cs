using Microsoft.Xna.Framework;

namespace Igra
{
    interface IThrowable
    {
        void Throw(bool facingLeft , Vector2 position, Vector2 gravity);
        Vector2 ThrowVelocity { get; set; }
        bool HitGround { get; set; }
        bool HitEnemy { get; set; }
        bool IsAvailableToThrow { get; set; }
        bool IsThrown { get; set; }
    }
}
