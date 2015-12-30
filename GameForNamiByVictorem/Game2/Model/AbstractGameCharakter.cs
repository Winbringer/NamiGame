using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameForNamiFromVictorem.Model
{
    public enum couse
    {
        left,
        right,
        top,
        botom
    }
    public abstract class AbstractGameCharakter
    {
        Point size;
        Vector2 position;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
            }
        }
        public Game1 game { get; set; }
        public bool Alive { get; set; }
        public Point Size
        {
            get
            {
                return new Point((int)(size.X * game.Scale), (int)(size.Y * game.Scale));
            }
            set
            {
                this.size = value;
            }
        }
        public Vector2 Position { get { return position; } set { position = value; } }
        public couse Course { get; set; }
        public Texture2D Texture { get; set; }
        public int Speed { get; set; }
        public abstract void Draw();
        public abstract void Move();
        public abstract void Die();

        protected void DrawRect(Rectangle rect)
        {
            game.SpiteBatch.Draw(this.Texture, this.Position, rect, Color.White, 0, Vector2.Zero, game.Scale, SpriteEffects.None, 0);

        }
        protected void DrawRect(Rectangle rect, float scale)
        {
            game.SpiteBatch.Draw(this.Texture, this.Position, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

        }
        public bool Collide(AbstractGameCharakter charakter)
        {
            return this.Rectangle.Intersects(charakter.Rectangle);
        }
        bool CollideLeft()
        {

            if (Position.X < 0)
            {
                Vector2 v = Position;
                v.X = 0;
                Position = v;
                return true;
            }
            else
            {
                return false;
            }
        }
        bool CollideTop()
        {
            if (Position.Y < 0)
            {
                Vector2 v = Position;
                v.Y = 0;
                Position = v;
                return true;
            }
            else
            {
                return false;
            }
        }
        bool CollideRight()
        {
            if (Position.X > game.Window.ClientBounds.Width - this.Size.X)
            {
                Vector2 v = Position;
                v.X = game.Window.ClientBounds.Width - this.Size.X;
                Position = v;
                return true;
            }
            else
            {
                return false;
            }
        }
        bool CollideBotom()
        {
            if (Position.Y > game.Window.ClientBounds.Height - this.Size.Y)
            {
                Vector2 v = Position;
                v.Y = game.Window.ClientBounds.Height - this.Size.Y;
                Position = v;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollideWall()
        {
            return this.CollideBotom() ||
                this.CollideLeft() ||
                this.CollideRight() ||
                this.CollideTop();
        }
    }
}
