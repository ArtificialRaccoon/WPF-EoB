using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WPF_EoB.DataConverters
{
    public class MiniMapConverter : IValueConverter
    {
        private const int Width = 5;
        private const int Height = 5;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Tuple<byte, byte>))
                throw new ArgumentException("MiniMapConverter attempted to convert a value which was not in the correct format for the players current position.");
            if (!(parameter is Classes.BindingProxy))
                throw new ArgumentException("MiniMapConverter was supplied a parameter which was not of the type BindingProxy");
            if (!(((Classes.BindingProxy)parameter).Data is Classes.DungeonMap))
                throw new ArgumentException("BindingProxy was supplied a parameter which was not of the type DungeonMap");

            int drawX = 2, drawY = 2;
            bool[,] visibleMap = new bool[5, 5];
            List<Canvas> returnList = new List<Canvas>();

            Tuple<byte, byte> mCurrentPosition = ((Tuple<byte, byte>)value);
            Classes.DungeonMap currentMap = ((Classes.DungeonMap)((Classes.BindingProxy)parameter).Data);
            int mapN1 = currentMap.DungeonTiles.GetLength(0);
            int mapN2 = currentMap.DungeonTiles.Length / mapN1;   

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    int posA = mCurrentPosition.Item2 + i;
                    int posB = mCurrentPosition.Item1 + j;
                    if (((posA >= 0 && posA < mapN1) && (posB >= 0 && posB < mapN2)))
                    {
                        if (currentMap.DungeonTiles[posA, posB] > 0)
                            visibleMap[i + 2, j + 2] = true;
                        else
                            visibleMap[i + 2, j + 2] = false;
                    }
                    else { visibleMap[i + 2, j + 2] = true; }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (visibleMap[i, j])
                    {
                        Canvas blockedCanvas = new Canvas();
                        Rectangle blockedWall = new Rectangle();
                        blockedWall.Width = Width;
                        blockedWall.Height = Height;
                        blockedWall.Fill = Brushes.White;

                        Canvas.SetTop(blockedWall, drawY);
                        Canvas.SetLeft(blockedWall, drawX);

                        blockedCanvas.Children.Add(blockedWall);
                        returnList.Add(blockedCanvas);
                    }
                    drawX += 6;
                    if (drawX > 26) drawX = 2;
                }
                drawY += 6;
                if (drawY > 26) drawY = 2;
            }

            return returnList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
