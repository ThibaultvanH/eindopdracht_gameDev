using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    internal class GrassBlock : Block
    {
        
            public GrassBlock(int x, int y, GraphicsDevice graphics, Texture2D texture2D) : base(x, y, graphics , texture2D)
            {
                BoundingBox = new Rectangle(x, y, 10, 10);
                Passable = true;
                Color = Color.GreenYellow;
                Texture = new Texture2D(graphics, 1, 1);
                
            }

        
    }
}
