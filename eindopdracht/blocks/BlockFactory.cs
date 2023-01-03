using eindopdracht.levels;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.blocks
{
    class BlockFactory
    {
        public Block CreateBlock(
        level.type type, int x, int y, GraphicsDevice graphics, Texture2D blocktexture)
        {
            Block newBlock = null;

            if (type == level.type.dirtBlock)
            {
                newBlock = new Block(x, y, graphics, blocktexture);
            }
            if (type == level.type.GrassBlock)
            {
                newBlock = new GrassBlock(x, y, graphics, blocktexture);
                Game1.grassdown.Add(newBlock.HitBoxdown);
                Game1.grassup.Add(newBlock.HitBoxup);
                Game1.grassright.Add(newBlock.HitBoxright);
                Game1.grassleft.Add(newBlock.HitBoxleft);
            }
            if (type == level.type.waterBlock)
            {
                newBlock = new Block(x, y, graphics, blocktexture);
            }

            if (type == level.type.air)
            {
            }
            return newBlock;
        }
    }
}
