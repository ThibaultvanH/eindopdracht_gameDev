using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    class BlockFactory
    {
        public Block CreateBlock(
        level.type type, int x, int y, GraphicsDevice graphics, Texture2D blocktexture)
        {
            Block newBlock = null;
            
            if (type == level.type.dirtBlock)
            {
                newBlock = new Block(x*10, y*10, graphics , blocktexture);
            }
            if (type == level.type.GrassBlock)
            {
                newBlock = new Block(x, y, graphics, blocktexture);
            }
            if (type == level.type.waterBlock)
            {
                newBlock = new Block(x, y, graphics, blocktexture);
            }
            return newBlock;
        }
    }
}
