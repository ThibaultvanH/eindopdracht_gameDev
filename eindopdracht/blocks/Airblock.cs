using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindopdracht.blocks
{
    internal class Airblock : Block
    {
        public Airblock(int x, int y, GraphicsDevice graphics, Texture2D blocktexture) : base(x, y, graphics, blocktexture)
        {
            BoundingBox = new Rectangle(592 * 0, 645 * 0, 592, 645);
            Passable = false;
            position = new Vector2(x, y);
            Texture = blocktexture;
            Color = Color.White;
        }
    }
}