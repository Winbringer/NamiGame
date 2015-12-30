using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameForNamiFromVictorem.Model
{
    class Fuck : AbstractGameCharakter
    {
        public override void Die()
        {
            this.Alive = false;
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            if (this.Alive)
            {
            }
            throw new NotImplementedException();
        }

        public override void Move()
        {
            if (this.Alive)
            {
            }
            throw new NotImplementedException();
        }
    }
}
