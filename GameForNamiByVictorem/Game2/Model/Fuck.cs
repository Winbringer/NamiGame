using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameForNamiFromVictorem.Model
{
    class Fuck : AbstractGameCharakter
    {
        public override void Live()
        {
            this.Move();           
        }

      public override  void Die()
        {
            this.Alive = false;           
        }

      public  override void Draw()
        {
            if (this.Alive)
            {
                Rectangle r = new Rectangle(0, 0, 180, 321);
                this.DrawRect(r);
            }
           
        }

        protected override void Move()
        {
            if (this.Alive)
            {
                Vector2 v = Position;
                v.X += Speed;
                Position = v;
                if (this.CollideWall()) this.Die();
            }
            
        }
    }
}
