using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Igra
{
    class Enemy : Character
    {
        public float Speed { get; set; }
        public Vector2 Position;
        public Texture2D Texture;
        public bool Alive;
        public int Damage { get; set; }
        public bool Colided { get; set; }
        public Rectangle CollisionSpace { get; set; }

        public Enemy(Texture2D texture, Vector2 position, int damage)
        {
            Alive = true;
            this.Texture = texture;
            this.Position = position;
            Speed = 100f;
            Damage = damage;
            Colided = false;
            CollisionSpace = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
        }

        private bool ColidedWithPlayer(Player player)
        {
            /*if (((player.Position.X + 24) > Position.X && player.Position.X < (Position.X + 35))
            && (player.Position.Y + 29.5 > Position.Y - 29))
            { Colided = true; return true; }
            Colided = false;
            return false; */
            if (this.CollisionSpace.Intersects(player.CollisionSpace))
            {
                Colided = true;
                return true;
            }
            else
            {
                Colided = false;
                return false;
            }

        }
        private void DealDamage(Player player)
        {
            player.TakeDamage(this.Damage);
        }
        private void GoLeft(Texture2D texture, float time)
        {
            Texture = texture;
            Position.X -= Speed * time;
        }
        private void GoRight(Texture2D texture, float time)
        {
            Texture = texture;
            Position.X += Speed * time;
        }
        public void SeekPlayer(Player player, Texture2D left, Texture2D right, float time)
        {
            if (ColidedWithPlayer(player))
                DealDamage(player);
            if (player.Position.X < Position.X)
            {
                GoLeft(left,time);
            }
            if (player.Position.X > Position.X)
            {
                GoRight(right, time);
            }
        }
    }
}
