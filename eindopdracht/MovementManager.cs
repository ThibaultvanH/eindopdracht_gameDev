using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    internal class MovementManager 
    {
        bool upkeyisup =true;
        public void Move(IMovable movable )
        {
            KeyboardState state = Keyboard.GetState();
            var direction = Vector2.Zero;
            
            if (state.IsKeyDown(Keys.Left) && movable.Position.X > 0)
            {
                direction.X -= 1;
                
            }
            if (state.IsKeyDown(Keys.Right) && movable.Position.X + 48 < 800)
            {
                direction.X += 1;
                
            }
            if (state.IsKeyDown(Keys.Up))
            {
                if (upkeyisup)
                {
                upkeyisup = false;
                
                direction.Y -= 10;
                }
                
                
            }
            if (state.IsKeyUp(Keys.Up))
            {

                upkeyisup = true;
                

            }

            direction *= movable.Speed;
            movable.Position += direction;

            var afstand = direction * movable.Speed;
            var toekomstigePositie = movable.Position + afstand;
            movable.Position = toekomstigePositie;
            movable.Position += afstand;
        }

    }
}
