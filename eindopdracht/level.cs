using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    internal class level
    {
        public enum type
        {
            air,
            GrassBlock,
            dirtBlock,
            waterBlock,
            
        }
        
    static public int[,] level1 = new int[,]
    {
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 1,1,1,1,1,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,1,1,2,1,0,0,0 },
    { 1,1,1,1,1,1,1,2,2,2,2,1,1,1 },
    { 2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
    };


    }
}
