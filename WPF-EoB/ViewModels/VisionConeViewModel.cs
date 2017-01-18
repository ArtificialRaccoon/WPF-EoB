using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_EoB.ViewModels
{
    /// <summary>
    /// The players "Cone of Vision".  Consists of the row the player is currently standing, and three
    /// rows directly infront of them.  If a wall exists in a space, the apropriate Item in the apropriate
    /// tuple will be true; otherwise it will be false (signifing that there is no obstruction here).
    /// 
    /// Honestly, not the most elegant solution, but it is very easy to bind to, and has no ill effects.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
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
        /// <summary>
        /// Gets or sets the row one - the players current row.
        /// </summary>
        /// <value>
        /// Row one.
        /// </value>
        public Tuple<byte, byte, byte> RowOne
        {
            get { return mRowOne; }
            set { mRowOne = value; OnPropertyChanged("RowOne"); } 
        }

        private Tuple<byte, byte, byte> mRowTwo = new Tuple<byte, byte, byte>(0, 0, 0);
        /// <summary>
        /// Gets or sets row two.
        /// </summary>
        /// <value>
        /// Row two.
        /// </value>
        public Tuple<byte, byte, byte> RowTwo
        {
            get { return mRowTwo; }
            set { mRowTwo = value; OnPropertyChanged("RowTwo"); }
        }

        private Tuple<byte, byte, byte, byte, byte> mRowThree = new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 0, 0);
        /// <summary>
        /// Gets or sets row three.
        /// </summary>
        /// <value>
        /// Row three.
        /// </value>
        public Tuple<byte, byte, byte, byte, byte> RowThree
        {
            get { return mRowThree; }
            set { mRowThree = value; OnPropertyChanged("RowThree"); }
        }

        private Tuple<byte, byte, byte, byte, byte, byte, byte> mRowFour = new Tuple<byte, byte, byte, byte, byte, byte, byte>(0, 0, 0, 0, 0, 0, 0);
        /// <summary>
        /// Gets or sets row four.
        /// </summary>
        /// <value>
        /// Row four.
        /// </value>
        public Tuple<byte, byte, byte, byte, byte, byte, byte> RowFour
        {
            get { return mRowFour; }
            set { mRowFour = value; OnPropertyChanged("RowFour"); }
        }

        private Classes.NPCClass mVisibleNPC = new Classes.NPCClass();
        public Classes.NPCClass VisibleNPC
        {
            get { return mVisibleNPC; }
            set { mVisibleNPC = value; OnPropertyChanged("VisibleNPC"); }
        }
    }
}
