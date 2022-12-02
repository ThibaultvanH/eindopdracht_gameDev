using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    internal class Block
    {
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 position { get; set; }

        public Block(int x, int y, GraphicsDevice graphics, Texture2D blocktexture)
        {
            BoundingBox = new Rectangle(592*2, 645 * 1, 592, 645);
            Passable = false;
            position = new Vector2(x, y);
            Texture = blocktexture;
            Color = Color.White;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,position,BoundingBox, Color.White, 0, new Vector2(0, 0),new Vector2(0.1f),SpriteEffects.None,1);
            
           

        }


    }
}
