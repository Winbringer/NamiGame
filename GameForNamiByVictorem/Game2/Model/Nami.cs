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
        public override void Die()
        {
            this.Alive = false;
        }

        public override void Draw()
        {
            if (this.Alive)
            {
                Rectangle rect = new Rectangle(game.FrameWidth * 3, game.FrameHeight * 2, game.FrameWidth, game.FrameHeight);
                game.SpiteBatch.Draw(this.Texture, this.Position, rect, Color.White,0,Vector2.Zero,game.Scale,SpriteEffects.None,1);                
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
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                                   
                }
            }
           
        }
    }
}
