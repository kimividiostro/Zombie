using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Igra
{
    class Knife : Weapon, IThrowable
    {
        #region variables
        Vector2 throwVelocity;
        Vector2 throwVelocityRight;
        private bool hitGround;
        private bool hitEnemy;
        private bool isAvailableToThrow;
        private bool isThrown;
        private float throwX, throwY, throwYRight;
        #endregion

        public Knife(Player player,Texture2D[] textures, Vector2 position)
        {
            throwX = 15;
            throwY = 10;
            throwYRight = -10;
            base.Damage = 20;
            base.Name = "Knife";
            base.Textures = textures;
            HitEnemy = false;
            HitGround = false;
            IsAvailableToThrow = true;
            ThrowVelocity = new Vector2(throwX, throwY);
            ThrowVelocityRight = new Vector2(throwX,throwYRight);
            base.Position = new Vector2(0,0);
        }

        private void ThrowLeft(Vector2 position, Vector2 gravity)
        {
            this.Position -= ThrowVelocity;
            this.ThrowVelocity -= gravity;
        }
        private void ThrowRight(Vector2 position, Vector2 gravity)
        {
            this.Position += ThrowVelocityRight;
            this.ThrowVelocityRight += gravity;
        }
        public void ResetThrowVelocity()
        {
            ThrowVelocity = new Vector2(throwX, throwY);
            ThrowVelocityRight = new Vector2(throwX, throwYRight);
            isAvailableToThrow = true;
            IsThrown = false;
            HitGround = false;
        }
        public bool EnemyIsHit(Enemy enemy)
        {
            if (Position.X > (enemy.Position.X - 17.5f) && Position.X < (enemy.Position.X + 17.5f) &&
                Position.Y > (enemy.Position.Y - 29) && Position.Y < (enemy.Position.Y + 29))
                return true;
            else
                return false;
        }

        #region IThrowable props
        public bool HitEnemy
        {
            get
            { return hitEnemy; }

            set { hitEnemy = value; }
        }

        public bool HitGround
        {
            get
            { return hitGround; }

            set { hitGround = value; }
        }

        public bool IsAvailableToThrow
        {
            get { return isAvailableToThrow; }

            set { isAvailableToThrow = value; }
        }

        public bool IsThrown
        {
            get
            { return isThrown; }

            set { isThrown = value; }
        }

        public Vector2 ThrowVelocity
        {
            get
            {   return throwVelocity;   }

            set {throwVelocity = value;}
        }

        public void Throw(bool facingLeft, Vector2 position, Vector2 gravity)
        {
            if(IsAvailableToThrow)
            {
                if (facingLeft)
                    this.ThrowLeft(position, gravity);
                else
                    this.ThrowRight(position, gravity);
            }
            if (HitGround || HitEnemy)
            {
                ResetThrowVelocity();
                IsAvailableToThrow = true;
                IsThrown = false;
            }
        }

        public Vector2 ThrowVelocityRight { get { return throwVelocityRight; } set { throwVelocityRight = value; } }
        #endregion
    }
}