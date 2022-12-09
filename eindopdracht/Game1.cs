using eindopdracht.blocks;
using eindopdracht.enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace eindopdracht
{
    internal class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _device;

        private SpriteBatch _spriteBatch;
        public static List<Rectangle> grassup = new List<Rectangle>();
        public static List<Rectangle> grassdown = new List<Rectangle>();
        public static List<Rectangle> grassright = new List<Rectangle>();
        public static List<Rectangle> grassleft = new List<Rectangle>();




        private Texture2D texture;
        private Texture2D bloktexture;
        private Texture2D Blocktexture;
        private Texture2D BG;
        private Texture2D greenmantexture;
        int[,] curentlevel = level.level1;
        public static List<Block> blocks = new List<Block>();
        BlockFactory blockFactory = new BlockFactory();
        Hero hero;
        greenman greenman1;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            
            loadTextures();
            InitializeGameObjects();           
            base.Initialize();

        }

        private void loadTextures()
        {
            texture = Content.Load<Texture2D>("hero");
            bloktexture = new Texture2D(GraphicsDevice, 1, 1);
            bloktexture.SetData(new Color[] { Color.DarkSlateGray });
            Blocktexture = Content.Load<Texture2D>("Tile-svg3");
            BG = Content.Load<Texture2D>("BG");
            greenmantexture = Content.Load<Texture2D>("Walk");
            
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, bloktexture);
            greenman1 = new greenman(greenmantexture, hero);
            CreateBlocks();
        }

        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (greenman1?.health < 0)
            {
                greenman1 = null;
            }
            hero.Update(gameTime);
            greenman1?.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(BG,new Rectangle(0,-100,1000,750),Color.White);
            hero.Draw(_spriteBatch);
            greenman1?.Draw(_spriteBatch);
            foreach (Block block in blocks)
            {
                block?.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < curentlevel.GetLength(0); l++)
            {
                for (int k = 0; k < curentlevel.GetLength(1); k++)
                {

                    
                    blocks.Add(blockFactory.CreateBlock((level.type)curentlevel[l, k], k* 59, l*64, GraphicsDevice, Blocktexture));
                }
            }
        }


    }
}