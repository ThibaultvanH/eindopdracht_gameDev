using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace eindopdracht
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _device;

        private SpriteBatch _spriteBatch;
        
        private Rectangle wall = new Rectangle(5,250,500,25);
        
        public Vector2 position;

        private Texture2D texture;
        private Texture2D bloktexture;
        private Texture2D Blocktexture;
        private Texture2D BG;
        int[,] curentlevel = level.level1;
        List<Block> blocks = new List<Block>();
        BlockFactory blockFactory = new BlockFactory();
        Hero hero;
        

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
            Blocktexture = Content.Load<Texture2D>("Tile-svg");
            BG = Content.Load<Texture2D>("BG");
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, bloktexture);
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

            // TODO: Add your update logic here
            hero.Update(gameTime);
            if (hero.feet.Intersects(wall))
            {             
                Debug.WriteLine("detect");
                hero.isFaling = false;
            }
            else
            {
                hero.isFaling = true;
                Debug.WriteLine("isfaling");
            }      
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(BG,new Rectangle(0,-100,1000,750),Color.White);
            hero.Draw(_spriteBatch);
            _spriteBatch.Draw(bloktexture, wall, Color.AliceBlue);
            foreach (Block block in blocks)
            {
                block.Draw(_spriteBatch);
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