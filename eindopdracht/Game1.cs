using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace eindopdracht
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        
        private SpriteBatch _spriteBatch;
        private Texture2D texture;
        private Rectangle wall = new Rectangle(5,250,500,25);
        Hero hero;
        public Vector2 position;
        public Texture2D bloktexture;
       
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            texture = Content.Load<Texture2D>("hero");
            bloktexture = new Texture2D(GraphicsDevice, 1, 1);
            bloktexture.SetData(new Color[] { Color.DarkSlateGray });
            InitializeGameObjects();
            
            base.Initialize();

        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, bloktexture);
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
            if (hero.blokrec.Intersects(wall))
            {             
                Debug.WriteLine("detect");
                hero.isFaling = false;
            }
            else
            {
                hero.isFaling = true;
            }
            Debug.WriteLine(hero.blokrec.Location);
             Debug.WriteLine(wall.Location);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            _spriteBatch.Draw(bloktexture, wall, Color.AliceBlue);
            _spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}