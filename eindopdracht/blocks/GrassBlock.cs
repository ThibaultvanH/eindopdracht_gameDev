using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eindopdracht.blocks
{
    internal class GrassBlock : Block
    {

        public GrassBlock(int x, int y, GraphicsDevice graphics, Texture2D blocktexture) : base(x, y, graphics, blocktexture)
        {
            BoundingBox = new Rectangle(59 * 2, 64 * 0, 59, 64  );
            HitBoxup = new Rectangle((int)position.X + 4, (int)position.Y, 50, 15);
            HitBoxdown = new Rectangle((int)position.X, (int)position.Y + 50, 50, 10);
            HitBoxright = new Rectangle((int)position.X, (int)position.Y + 10, 10, 40);
            HitBoxleft = new Rectangle((int)position.X + 50, (int)position.Y + 10, 10, 40);
            Passable = false;
            position = new Vector2(x, y);
            Texture = blocktexture;
            Color = Color.White;

        }

        


    }
}
