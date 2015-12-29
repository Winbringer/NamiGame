using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameForNamiFromVictorem
{
  public  class DrawPresenter
    {
        Game1 game;
        public DrawPresenter(Game1 game)
        {
            this.game = game;
        }
        public int Draw()
        {
            return 1;
        }
    }
}
