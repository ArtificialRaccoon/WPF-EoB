using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_EoB.Classes
{
    public class VisionCone : INotifyPropertyChanged
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

        private Tuple<bool, bool, bool> mRowOne = new Tuple<bool, bool, bool>(false, false, false);
        public Tuple<bool, bool, bool> RowOne
        {
            get { return mRowOne; }
            set { mRowOne = value; OnPropertyChanged("RowOne"); } 
        }

        private Tuple<bool, bool, bool> mRowTwo = new Tuple<bool, bool, bool>(false, false, false);
        public Tuple<bool, bool, bool> RowTwo
        {
            get { return mRowTwo; }
            set { mRowTwo = value; OnPropertyChanged("RowTwo"); }
        }

        private Tuple<bool, bool, bool, bool, bool> mRowThree = new Tuple<bool, bool, bool, bool, bool>(false, false, false, false, false);
        public Tuple<bool, bool, bool, bool, bool> RowThree
        {
            get { return mRowThree; }
            set { mRowThree = value; OnPropertyChanged("RowThree"); }
        }

        private Tuple<bool, bool, bool, bool, bool, bool, bool> mRowFour = new Tuple<bool, bool, bool, bool, bool, bool, bool>(false, false, false, false, false, false, false);
        public Tuple<bool, bool, bool, bool, bool, bool, bool> RowFour
        {
            get { return mRowFour; }
            set { mRowFour = value; OnPropertyChanged("RowFour"); }
        }
    }
}
