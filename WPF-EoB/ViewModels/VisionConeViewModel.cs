using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_EoB.ViewModels
{
    public class VisionConeViewModel : INotifyPropertyChanged
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
     
        private Tuple<byte, byte, byte> mRowOne = new Tuple<byte, byte, byte>(0, 0, 0);
        public Tuple<byte, byte, byte> RowOne
        {
            get { return mRowOne; }
            set { mRowOne = value; OnPropertyChanged("RowOne"); } 
        }

        private Tuple<byte, byte, byte> mRowTwo = new Tuple<byte, byte, byte>(0, 0, 0);
        public Tuple<byte, byte, byte> RowTwo
        {
            get { return mRowTwo; }
            set { mRowTwo = value; OnPropertyChanged("RowTwo"); }
        }

        private Tuple<byte, byte, byte, byte, byte> mRowThree = new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 0, 0);
        public Tuple<byte, byte, byte, byte, byte> RowThree
        {
            get { return mRowThree; }
            set { mRowThree = value; OnPropertyChanged("RowThree"); }
        }

        private Tuple<byte, byte, byte, byte, byte, byte, byte> mRowFour = new Tuple<byte, byte, byte, byte, byte, byte, byte>(0, 0, 0, 0, 0, 0, 0);
        public Tuple<byte, byte, byte, byte, byte, byte, byte> RowFour
        {
            get { return mRowFour; }
            set { mRowFour = value; OnPropertyChanged("RowFour"); }
        }
    }
}
