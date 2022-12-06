﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    abstract class person : IGameObject
    {

        public int health { get; set; }
        public bool isheadtouching { get; set; }
        public Rectangle blokrec = new Rectangle(0, 0, 50, 50);
        public Rectangle feet = new Rectangle(0, 0, 30, 5);
        public Rectangle body = new Rectangle(0, 0, 30, 5);
        public Rectangle fist = new Rectangle(0, 0, 5, 15);
        public Rectangle head = new Rectangle(0, 0, 5, 15);
        public int height = 50;
        public SpriteEffects SpriteDirection;
        public Vector2 oldpos;

        public bool isTouchingGround()
        {
            foreach (var item in Game1.grassup)
            {
                if (item.Intersects(feet))
                {
                    return true;
                }
            }

            return false;

        }
        public bool headTouchingGround()
        {
            foreach (var item in Game1.grassdown)
            {
                if (item.Intersects(head))
                {
                    return true;
                }
            }

            return false;

        }

        abstract public void Update(GameTime gameTime);

        abstract public void Draw(SpriteBatch spriteBatch);

        abstract public void Move();
        
    }
}