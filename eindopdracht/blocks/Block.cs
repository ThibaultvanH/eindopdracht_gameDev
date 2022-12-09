using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace eindopdracht.blocks
{
    internal class Block
    {
        public Rectangle BoundingBox { get; set; }
        public Rectangle HitBoxup { get; set; }
        public Rectangle HitBoxdown { get; set; }
        public Rectangle HitBoxright { get; set; }
        public Rectangle HitBoxleft { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 position { get; set; }

        public Block(int x, int y, GraphicsDevice graphics, Texture2D blocktexture)
        {
            BoundingBox = new Rectangle(59 * 2, 64 * 1, 59, 64);
            HitBoxup = new Rectangle((int)position.X,(int) position.Y , 59, 10);
            HitBoxdown = new Rectangle((int)position.X,(int) position.Y + 50, 59, 10);
            HitBoxright = new Rectangle((int)position.X, (int)position.Y + 10, 10, 40);
            HitBoxleft = new Rectangle((int)position.X + 50, (int)position.Y + 10, 10, 40);
            Passable = false;
            position = new Vector2(x, y);
            Texture = blocktexture;
            Color = Color.White;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, BoundingBox, Color.White, 0, new Vector2(0, 0), new Vector2(1f), SpriteEffects.None, 1);



        }


    }
}
