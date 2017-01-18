using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_EoB.ViewModels
{
    /// <summary>
    /// ViewModel for the MazeView control / state.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MazeViewViewModel : INotifyPropertyChanged
    {
        #region  OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) { handler(this, new PropertyChangedEventArgs(name)); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeViewViewModel"/> class.
        /// </summary>
        public MazeViewViewModel()
        {
            UpdatePlayer(0, 0, 0);
        }

        /// <summary>
        /// Updates the players position and rotation.  As a result, the players cone of
        /// vision will also be updated.
        /// </summary>
        /// <param name="deltaY">The delta y.</param>
        /// <param name="deltaX">The delta x.</param>
        /// <param name="rotation">The rotation.</param>
        public void UpdatePlayer(int deltaY, int deltaX, Classes.Enumerations.Direction? rotation)
        {
            if(rotation == Classes.Enumerations.Direction.East)
            {
                switch(CurrentDirection)
                {
                    case Classes.Enumerations.Direction.North:
                        CurrentDirection = Classes.Enumerations.Direction.East;
                        break;
                    case Classes.Enumerations.Direction.East:
                        CurrentDirection = Classes.Enumerations.Direction.South;
                        break;
                    case Classes.Enumerations.Direction.South:
                        CurrentDirection = Classes.Enumerations.Direction.West;
                        break;
                    case Classes.Enumerations.Direction.West:
                        CurrentDirection = Classes.Enumerations.Direction.North;
                        break;
                }
            }
            else if (rotation == Classes.Enumerations.Direction.West)
            {
                switch (CurrentDirection)
                {
                    case Classes.Enumerations.Direction.North:
                        CurrentDirection = Classes.Enumerations.Direction.West;
                        break;
                    case Classes.Enumerations.Direction.East:
                        CurrentDirection = Classes.Enumerations.Direction.North;
                        break;
                    case Classes.Enumerations.Direction.South:
                        CurrentDirection = Classes.Enumerations.Direction.East;
                        break;
                    case Classes.Enumerations.Direction.West:
                        CurrentDirection = Classes.Enumerations.Direction.South;
                        break;
                }
            }

            if (CurrentDirection == Classes.Enumerations.Direction.North)
                UpdateVisionCone(false, -1, -1, deltaY, 1, deltaX);
            else if (CurrentDirection == Classes.Enumerations.Direction.East)
                UpdateVisionCone(true, 1, 1, deltaY, -1, deltaX);
            else if (CurrentDirection == Classes.Enumerations.Direction.South)
                UpdateVisionCone(false, 1, 1, deltaY, -1, deltaX);
            else
                UpdateVisionCone(true, -1, -1, deltaY, 1, deltaX);
        }

        /// <summary>
        /// Updates the players cone of vision.
        /// </summary>
        /// <param name="calculateForX">if set to <c>true</c> [calculate for x].  Basically for handling when the player is facing EW instead of NS.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="deltaYSign">The delta y sign.</param>
        /// <param name="deltaY">The delta y.</param>
        /// <param name="deltaXSign">The delta x sign.</param>
        /// <param name="deltaX">The delta x.</param>
        private void UpdateVisionCone(bool calculateForX, short direction, short deltaYSign, int deltaY, short deltaXSign, int deltaX)
        {
            byte a = 0, b = 0; 
            int mapN1 = 0, mapN2 = 0;

            if(calculateForX)
            {
                if (CurrentDungeon.DungeonTiles[CurrentPosition.Item2 + (deltaXSign * deltaX), CurrentPosition.Item1 + (deltaYSign * deltaY)] < 1)
                {
                    CurrentPosition = new Tuple<byte, byte>((byte)(CurrentPosition.Item1 + (deltaYSign * deltaY)), (byte)(CurrentPosition.Item2 + (deltaXSign * deltaX)));
                }

                a = CurrentPosition.Item1;
                b = CurrentPosition.Item2;
                mapN2 = CurrentDungeon.DungeonTiles.GetLength(0);
                mapN1 = CurrentDungeon.DungeonTiles.Length / mapN2;    
            }
            else 
            {
                if (CurrentDungeon.DungeonTiles[CurrentPosition.Item2 + (deltaYSign * deltaY), CurrentPosition.Item1 + (deltaXSign * deltaX)] < 1)
                {
                    CurrentPosition = new Tuple<byte, byte>((byte)(CurrentPosition.Item1 + (deltaXSign * deltaX)), (byte)(CurrentPosition.Item2 + (deltaYSign * deltaY)));
                }
                a = CurrentPosition.Item2;
                b = CurrentPosition.Item1;
                mapN1 = CurrentDungeon.DungeonTiles.GetLength(0);
                mapN2 = CurrentDungeon.DungeonTiles.Length / mapN1;            
            }

            byte[] rowOne = new byte[3];
            byte[] rowTwo = new byte[3];
            byte[] rowThree = new byte[5];
            byte[] rowFour = new byte[7];
            
            for(int i = 0; i < 3; i++)
            {
                if (a >= 0 && a < mapN1 && (b + (-1 + i)) >= 0 && (b + (-1 + i)) < mapN2)
                {
                    if (calculateForX)
                        rowOne[(deltaYSign < 0 ? Math.Abs(i - 2) : i)] = CurrentDungeon.DungeonTiles[b + (-1 + i), a];
                    else
                        rowOne[(deltaYSign > 0 ? Math.Abs(i - 2) : i)] = CurrentDungeon.DungeonTiles[a, b + (-1 + i)];
                }

                if ((a + (direction * 1)) >= 0 && (a + (direction * 1)) < mapN1 && (b + (-1 + i)) >= 0 && (b + (-1 + i)) < mapN2)
                {
                    if (calculateForX)
                        rowTwo[(deltaYSign < 0 ? Math.Abs(i - 2) : i)] = CurrentDungeon.DungeonTiles[(b + (-1 + i)), a + (direction * 1)];
                    else
                        rowTwo[(deltaYSign > 0 ? Math.Abs(i - 2) : i)] = CurrentDungeon.DungeonTiles[(a + (direction * 1)), b + (-1 + i)];
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if ((a + (direction * 2)) >= 0 && (a + (direction * 2)) < mapN1 && (b + (-2 + i)) >= 0 && (b + (-2 + i)) < mapN2)
                {
                    if (calculateForX)
                        rowThree[(deltaYSign < 0 ? Math.Abs(i - 4) : i)] = CurrentDungeon.DungeonTiles[(b + (-2 + i)), a + (direction * 2)];
                    else
                        rowThree[(deltaYSign > 0 ? Math.Abs(i - 4) : i)] = CurrentDungeon.DungeonTiles[(a + (direction * 2)), b + (-2 + i)];
                }
            }

            for (int i = 0; i < 7; i++)
            {
                if ((a + (direction * 3)) >= 0 && (a + (direction * 3)) < mapN1 && (b + (-3 + i)) >= 0 && (b + (-3 + i)) < mapN2)
                {
                    if (calculateForX)
                        rowFour[(deltaYSign < 0 ? Math.Abs(i - 6) : i)] = CurrentDungeon.DungeonTiles[(b + (-3 + i)), a + (direction * 3)];
                    else
                        rowFour[(deltaYSign > 0 ? Math.Abs(i - 6) : i)] = CurrentDungeon.DungeonTiles[(a + (direction * 3)), b + (-3 + i)];
                }
            }

            PlayerView.RowOne = new Tuple<byte, byte, byte>(rowOne[0], rowOne[1], rowOne[2]);
            PlayerView.RowTwo = new Tuple<byte, byte, byte>(rowTwo[0], rowTwo[1], rowTwo[2]);
            PlayerView.RowThree = new Tuple<byte, byte, byte, byte, byte>(rowThree[0], rowThree[1], rowThree[2], rowThree[3], rowThree[4]);
            PlayerView.RowFour = new Tuple<byte, byte, byte, byte, byte, byte, byte>(rowFour[0], rowFour[1], rowFour[2], rowFour[3], rowFour[4], rowFour[5], rowFour[6]);
        }
        #endregion

        #region Properties
        private Tuple<byte, byte> mCurrentPosition = new Tuple<byte, byte>(1, 1);
        /// <summary>
        /// Gets or sets the players current position.
        /// </summary>
        /// <value>
        /// The players current position.
        /// </value>
        public Tuple<byte, byte> CurrentPosition
        {
            get { return mCurrentPosition; }
            set { mCurrentPosition = value; OnPropertyChanged("CurrentDirection"); OnPropertyChanged("CurrentPosition"); }
        }

        private Classes.Enumerations.Direction mCurrentDirection = Classes.Enumerations.Direction.South;
        /// <summary>
        /// Gets or sets the current direction the player is facing.
        /// </summary>
        /// <value>
        /// The current direction the player is facing.
        /// </value>
        public Classes.Enumerations.Direction CurrentDirection
        {
            get { return mCurrentDirection; }
            set { mCurrentDirection = value; OnPropertyChanged("CurrentDirection"); OnPropertyChanged("CurrentPosition"); }
        }

        private Classes.DungeonMap mCurrentDungeon = new Classes.DungeonMap();
        /// <summary>
        /// Gets or sets the current dungeon map.
        /// </summary>
        /// <value>
        /// The current dungeon map.
        /// </value>
        public Classes.DungeonMap CurrentDungeon
        {
            get { return mCurrentDungeon; }
            set { mCurrentDungeon = value; }
        }

        private ViewModels.VisionConeViewModel mPlayerView = new ViewModels.VisionConeViewModel();
        /// <summary>
        /// Gets or sets the players current cone of vision.
        /// </summary>
        /// <value>
        /// The players current cone of vision.
        /// </value>
        public ViewModels.VisionConeViewModel PlayerView
        {
            get { return mPlayerView; }
            set { mPlayerView = value; }
        }
        #endregion
    }
}
