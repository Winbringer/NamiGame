using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameForNamiFromVictorem.Model
{
    class Mem : AbstractGameCharakter
    {
        int X;
        Rectangle R;

        public Mem(Game1 game)
        {
            this.game = game;
            Random r = new Random();
            X = r.Next(0, 6);
            R = new Rectangle(game.FrameWidth * X, 0, game.FrameWidth, game.FrameHeight);
            this.Size = new Point(game.FrameWidth, game.FrameHeight);
            this.Position = new Vector2(400, 400);
            this.Alive = true;
        }
        public override void Die()
        {
            this.Alive = false;
            
        }

        public override void Draw()
        {
            if (this.Alive)
            {
                this.DrawRect(R);
            }
            
        }

        public override void Move()
        {
            if (this.Alive)
            {
                if (this.CollideWall()) this.Die();
            }
           
        }
    }
}
