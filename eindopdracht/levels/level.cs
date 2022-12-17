using eindopdracht.blocks;
using eindopdracht.enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace eindopdracht.levels
{
    internal abstract class level
    {
        public enum type
        {
            air,
            GrassBlock,
            dirtBlock,
            waterBlock,

        }

        public int[,] Level;

        
        public Button menubutton;

        

        public Texture2D HeroTexture;
        public Texture2D BlockTexture;
        public Texture2D ButtonTexture;
        public Texture2D GreenmanTexture;
        public Texture2D ridderTexture;
        public Texture2D SpikeTexture;
        public GraphicsDevice dev;
        public BlockFactory blockFactory;
        public SpriteFont font;



        public static List<Rectangle> grassup = new List<Rectangle>();
        public static List<Rectangle> grassdown = new List<Rectangle>();
        public static List<Rectangle> grassright = new List<Rectangle>();
        public static List<Rectangle> grassleft = new List<Rectangle>();
        public static List<Block> blocks = new List<Block>();


        public level(Texture2D HeroTexture, Texture2D blockTexture, Texture2D ButtonTexture, Texture2D GreenmanTexture,Texture2D RidderTexture,Texture2D SpikeTexture , GraphicsDevice dev , SpriteFont font)
        {
            this.HeroTexture = HeroTexture;
            this.BlockTexture = blockTexture;
            this.ButtonTexture = ButtonTexture;
            this.GreenmanTexture = GreenmanTexture;
            this.ridderTexture = RidderTexture;
            this.SpikeTexture = SpikeTexture;
            this.dev = dev;
            this.font = font;
            InitializeGameObjects();
        }

        public void InitializeGameObjects()
        {
            
            menubutton = new Button(ButtonTexture);
            menubutton.Click += gomenu;
            blockFactory = new BlockFactory();
            
            
        }




        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        

        public void gomenu(object sender, System.EventArgs e)
        {
            Game1.Gamestate = GameState.Menu;
        }
        public void Gameover()
        {
            
            Game1.Gamestate = GameState.gameover;
        }

        public void CreateBlocks()
        {
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int k = 0; k < Level.GetLength(1); k++)
                {
                    blocks.Add(blockFactory.CreateBlock((level.type)Level[l, k], k * 59, l * 64, dev, BlockTexture));
                }
            }
        }

        
    }
}
