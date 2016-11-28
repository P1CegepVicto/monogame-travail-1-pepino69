using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_2
{
    internal class GameObject
    {
        public bool estvivant;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 vitesse;
        public Rectangle rectColision = new Rectangle();




        public Rectangle GetRect()
        {
            rectColision.X = (int)this.position.X;
            rectColision.Y = (int)this.position.Y;
            rectColision.Width = this.sprite.Width;
            rectColision.Height = this.sprite.Height;

            return rectColision;
        }
    }
}