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

    public enum GameState
    {
    Menu,
    level1,
    level2,
    gameover   
    }

    internal class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static List<Rectangle> grassup = new List<Rectangle>();
        public static List<Rectangle> grassdown = new List<Rectangle>();
        public static List<Rectangle> grassright = new List<Rectangle>();
        public static List<Rectangle> grassleft = new List<Rectangle>();
        public static List<Block> blocks = new List<Block>();

        private Texture2D texture;
        private Texture2D bloktexture;
        private Texture2D Blocktexture;
        private Texture2D BG;
        private Texture2D greenmantexture;


        int[,] curentlevel = level.level1;
        public static GameState Gamestate = GameState.Menu;
        
        BlockFactory blockFactory = new BlockFactory();
        Hero hero;
        greenman greenman1;
        Button button;
   

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            loadBG();
            loadMenu();
            loadGame();

            base.Initialize();

        }

        private void loadBG()
        {
            BG = Content.Load<Texture2D>("BG");
        }

        private void loadMenu()
        {
            button = new Button(Content.Load<Texture2D>("Control/wooden"), Content.Load<SpriteFont>("font/newfont"))
            {
                
                Text = "Start Game",
            }; 
            button.Position = new Vector2((GraphicsDevice.Viewport.Width - button.texture.Width) / 2, 150);
        }

        private void loadGame()
        {
            loadTextures();
            InitializeGameObjects();
        }

        private void loadTextures()
        {
            texture = Content.Load<Texture2D>("hero");
            bloktexture = new Texture2D(GraphicsDevice, 1, 1);
            bloktexture.SetData(new Color[] { Color.DarkSlateGray });
            Blocktexture = Content.Load<Texture2D>("Tile-svg3");
            
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

            switch (Gamestate)
            {
                case GameState.Menu:
                    button?.Update(gameTime);

                    break;


                case GameState.level1:
                case GameState.level2:
                    if (greenman1?.health < 0)
                    {
                        greenman1 = null;
                    }
                    hero.Update(gameTime);
                    greenman1?.Update(gameTime);
                    break;


                case GameState.gameover:
                    break;
                default:
                    break;
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            _spriteBatch.Draw(BG, new Rectangle(0, -100, 1000, 750), Color.White);
            switch (Gamestate)
            {
                case GameState.Menu:
                    button.Draw(gameTime, _spriteBatch);
                    break;


                case GameState.level1:
                case GameState.level2:
                    
                    hero.Draw(_spriteBatch);
                    greenman1?.Draw(_spriteBatch);
                    foreach (Block block in blocks)
                    {
                        block?.Draw(_spriteBatch);
                    }
                    break;


                case GameState.gameover:
                    break;
                default:
                    break;
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