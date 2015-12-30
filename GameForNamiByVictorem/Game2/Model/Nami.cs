using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace GameForNamiFromVictorem.Model
{
    public class Nami : AbstractGameCharakter
    {
       DateTime shootTime=DateTime.Now;
        public override void Die()
        {
            this.Alive = false;
            Rectangle rect = new Rectangle(game.FrameWidth * 2, game.FrameHeight * 2, game.FrameWidth, game.FrameHeight);
            this.DrawRect(rect);
            game.IsLoose = true;
        }

        public override void Draw()
        {
            if (this.Alive)
            {
                Rectangle rect = new Rectangle(game.FrameWidth * 3, game.FrameHeight * 2, game.FrameWidth, game.FrameHeight);
                this.DrawRect(rect);              
            }
            // sBatch.Draw(this.Texture,)
        }

        public override void Move()
        {
           
            if (this.Alive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    this.Course = couse.left;
                    Vector2 v = Position;
                    v.X -= Speed;
                    Position = v;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    this.Course = couse.right;
                    Vector2 v = Position;
                    v.X += Speed;
                    Position = v;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    this.Course = couse.top;
                    Vector2 v = Position;
                    v.Y -= Speed;
                    Position = v;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    this.Course = couse.botom;
                    Vector2 v = Position;
                    v.Y += Speed;
                    Position = v;
                }
                CollideWall();
            }

        }
        public void Shoot()
        {
            if (this.Alive)
            {
              DateTime now =  DateTime.Now;
                TimeSpan TS = now - shootTime;
                if(TS.TotalMilliseconds>100)
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Fuck f = new Fuck()
                    {
                        Texture = game.Fuck,
                        Alive = true,
                        game = this.game,
                        Size = new Point(180, 321),
                        Speed = 10
                    };
                    f.Position = new Vector2((this.Position.X + this.Size.X + 3), ((this.Position.Y + this.Size.Y / 2)-f.Size.Y/2));
                    game.Chars.Add(f);
                    shootTime = DateTime.Now;
                }
            }
           
        }
    }
}
