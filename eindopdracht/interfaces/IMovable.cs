using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.interfaces
{
    interface IMovable
    {

        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }

        public Vector2 veloCity { get; set; }

        public bool Left { get; set; }
        public bool Right { get; set; }

    }
}
