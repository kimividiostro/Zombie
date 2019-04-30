using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Igra
{
    class Player : Character
    {
        private int health;
        /*public int Health
        {
            get { return health; }
            set
            {
                if (value >= 0 && value <= 100)
                    health = value;
            }
        } */
        public float Speed { get; set; }
        private Vector2 JumpVelocity; 
        public Vector2 Position;
        public Texture2D Texture;
        public Rectangle CollisionSpace { get; set; }
        public bool Jumping;
        public bool Alive
        {
            get
            {
                if (Health > 0) return true; else return false;
            }
            set { }
        }

        public Player(Texture2D texture, int health, Vector2 position)
        {
            Alive = true;
            this.Texture = texture;
            base.Health = health;
            this.Position = position;
            JumpVelocity = new Vector2(0, 30);
            Jumping = false;
            Speed = 250f;
            CollisionSpace = new Rectangle((int) position.X, (int) position.Y, (int) texture.Width,(int) texture.Height);
        }

        //DIRECTION FACING
        public bool IsFacingLeft()
        {
            if (Texture.Name.Equals("player"))
                return false;
            return true;
        }

        //MOVEMENT
        public void IsJumping()
        {
            Jumping = true;
        }
        public void MoveLeft(float time, Texture2D texture)
        {
            if (Jumping)
                Position.X -= Speed * (time * 1.2f);
            else
            {
                Position.X -= Speed * time;
                Texture = texture;
            }
        }
        public void MoveRight(float time, Texture2D texture)
        {
            if (Jumping)
                Position.X += Speed * (time * 1.2f);
            else
            {
                Position.X += Speed * time;
                Texture = texture;
            }
        }
        public void Jump(int ground, Vector2 gravity)
        {
            Jumping = true;
            if (Jumping)
            {
                Position -= JumpVelocity;
                JumpVelocity -= gravity;
            }
            if (Position.Y >= ground)
            {
                Jumping = false;
                JumpVelocity.X = 0;
                JumpVelocity.Y = 30;
                Position.Y = ground;
            }
        }

        //DAMAGE
        public void TakeDamage(int damage)
        {
            if ((Health - damage) <= 0)
            {
                Health = 0;
                Alive = false;
            }
            else
                Health -= damage;
        }
        public void Heal(int health)
        {
            if ((Health + health) > 100)
                Health = 100;
            else
                Health += health;
        }
    }
}
