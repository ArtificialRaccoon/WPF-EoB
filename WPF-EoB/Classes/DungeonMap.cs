using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EoB.Classes
{
    public class DungeonMap
    {
        public DungeonMap()
        {
            mDungeonTiles = new byte[7,9] {
                {1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,1},
                {1,0,0,0,2,0,0,0,1},
                {1,0,0,2,1,0,0,0,1},
                {1,0,0,0,2,0,0,0,1},
                {1,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1}
            };
        }

        private byte[,] mDungeonTiles;
        public byte[,] DungeonTiles
        {
            get { return mDungeonTiles; }
            set { mDungeonTiles = value; }
        }
    }    
}
