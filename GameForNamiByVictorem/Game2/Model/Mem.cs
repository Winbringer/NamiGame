using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameForNamiFromVictorem.Model
{
    class Mem : AbstractGameCharakter
    {
        int X;
        int Y;
        Rectangle R;
        int i = 0;
        bool activ = true;
        public Mem(Game1 game)
        {
            this.game = game;
            Random r = new Random();
            X = r.Next(0, 6);
            Y = r.Next(0, game.framesY);
            R = new Rectangle(game.FrameWidth * X, 0, game.FrameWidth, game.FrameHeight);
            this.Size = new Point(game.FrameWidth, game.FrameHeight);
            this.Position = new Vector2(game.Window.ClientBounds.Width - this.Size.X - 10, ((this.Size.Y - 1) * Y)+5);
            this.Alive = true;
        }
        public override void Die()
        {
            activ = false;
            game.Heiters += 1;

        }

        public override void Draw()
        {
            if (activ)
            {
                this.DrawRect(R);
            }
            else
            {
                ++i;
                if (i == 1)
                    R = new Rectangle(game.FrameWidth * 0, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 2)
                    R = new Rectangle(game.FrameWidth * 1, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 3)
                    R = new Rectangle(game.FrameWidth * 2, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 4)
                    R = new Rectangle(game.FrameWidth * 3, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 5)
                    R = new Rectangle(game.FrameWidth * 4, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 6)
                    R = new Rectangle(game.FrameWidth * 5, game.FrameHeight * 1, game.FrameWidth, game.FrameHeight);
                if (i == 7)
                    R = new Rectangle(game.FrameWidth * 0, game.FrameHeight * 2, game.FrameWidth, game.FrameHeight);
                if (i == 8)
                    R = new Rectangle(game.FrameWidth * 1, game.FrameHeight * 2, game.FrameWidth, game.FrameHeight);
                this.DrawRect(R);
                if(i>9)
                Alive = false;
            }

        }

        public override void Move()
        {
            if (activ)
            {
                Vector2 v = this.Position;
                v.X -= this.Speed;
                Position = v;
                if (this.CollideWall())
                {
                    this.Die();                    
                    this.game.IsLoose = true;
                }
            }

        }
    }
}
