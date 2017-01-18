using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EoB.Classes
{
    /// <summary>
    /// Class for sotring data about the current dungeon.
    /// </summary>
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

            NPCList.Add(new NPCClass("Teefa", new Tuple<byte, byte>(3, 5), 0));
        }

        private byte[,] mDungeonTiles;
        /// <summary>
        /// Gets or sets the dungeon tiles.  Values of 0 mean there is no wall at this location.
        /// Values of 1 or higher corrispond to the apropriate texture in the texture sheet.
        /// </summary>
        /// <value>
        /// The dungeon tiles.
        /// </value>
        public byte[,] DungeonTiles
        {
            get { return mDungeonTiles; }
            set { mDungeonTiles = value; }
        }

        private List<NPCClass> mNPCList = new List<NPCClass>();
        public List<NPCClass> NPCList
        {
            get { return mNPCList; }
            set { mNPCList = value; }
        }
    }    
}
