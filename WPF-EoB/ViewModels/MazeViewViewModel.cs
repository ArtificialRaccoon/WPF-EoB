using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_EoB.ViewModels
{
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

        public MazeViewViewModel()
        {
            UpdatePlayer(0, 0, 0);
        }

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

        private void UpdateVisionCone(bool calculateForX, short direction, short deltaYSign, int deltaY, short deltaXSign, int deltaX)
        {
            byte a = 0, b = 0; 
            int mapN1 = 0, mapN2 = 0;

            if(calculateForX)
            {
                if (CurrentDungeon.DungeonTiles[mCurrentPosition.Item2 + (deltaXSign * deltaX), mCurrentPosition.Item1 + (deltaYSign * deltaY)] < 1)
                {
                    mCurrentPosition = new Tuple<byte, byte>((byte)(mCurrentPosition.Item1 + (deltaYSign * deltaY)), (byte)(mCurrentPosition.Item2 + (deltaXSign * deltaX)));
                }

                a = mCurrentPosition.Item1;
                b = mCurrentPosition.Item2;
                mapN2 = CurrentDungeon.DungeonTiles.GetLength(0);
                mapN1 = CurrentDungeon.DungeonTiles.Length / mapN2;    
            }
            else 
            {
                if (CurrentDungeon.DungeonTiles[mCurrentPosition.Item2 + (deltaYSign * deltaY), mCurrentPosition.Item1 + (deltaXSign * deltaX)] < 1)
                {
                    mCurrentPosition = new Tuple<byte, byte>((byte)(mCurrentPosition.Item1 + (deltaXSign * deltaX)), (byte)(mCurrentPosition.Item2 + (deltaYSign * deltaY)));
                }
                a = mCurrentPosition.Item2;
                b = mCurrentPosition.Item1;
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

        private bool ConvertTileToBool(byte tileNum)
        {
            if (tileNum > 0)
                return true;
            return false;
        }

        private Tuple<byte, byte> mCurrentPosition = new Tuple<byte, byte>(1, 1);

        private Classes.Enumerations.Direction mCurrentDirection = Classes.Enumerations.Direction.South;
        public Classes.Enumerations.Direction CurrentDirection
        {
            get { return mCurrentDirection; }
            set { mCurrentDirection = value; OnPropertyChanged("CurrentDirection"); }
        }

        private Classes.DungeonMap mCurrentDungeon = new Classes.DungeonMap();
        public Classes.DungeonMap CurrentDungeon
        {
            get { return mCurrentDungeon; }
            set { mCurrentDungeon = value; }
        }

        private ViewModels.VisionConeViewModel mPlayerView = new ViewModels.VisionConeViewModel();
        public ViewModels.VisionConeViewModel PlayerView
        {
            get { return mPlayerView; }
            set { mPlayerView = value; }
        }
    }
}
