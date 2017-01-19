using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MapEditor.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
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

        public MapViewModel()
        {
            GenerateMap();
        }

        private void GenerateMap()
        {
            for (int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    Map.Add(new Classes.SimpleSquare() { Row = i, Column = j });
                }
            }
        }

        private int mSize = 15;
        public int Size
        {
            get { return mSize; }
            set
            {
                mSize = value;
                GenerateMap();
                OnPropertyChanged("Size");
                OnPropertyChanged("Map");
            }
        }

        private ObservableCollection<Classes.SimpleSquare> mMap = new ObservableCollection<Classes.SimpleSquare>();
        public ObservableCollection<Classes.SimpleSquare> Map
        {
            get { return mMap; }
        }
    }
}
