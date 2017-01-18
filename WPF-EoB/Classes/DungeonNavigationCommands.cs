using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace WPF_EoB.Classes
{
    /// <summary>
    /// Commands for navigating a player in a Dungeon Map
    /// </summary>
    public class DungeonNavigationCommands
    {
        private static RoutedUICommand rotatePlayerCommand = new RoutedUICommand("RotatePlayerCommand", "RotatePlayerCommand", typeof(DungeonNavigationCommands));
        /// <summary>
        /// Command to rotate the player.
        /// </summary>
        public static RoutedUICommand RotatePlayerCommand
        {
            get { return rotatePlayerCommand; }
        }

        private static RoutedUICommand translatePlayerXCommand = new RoutedUICommand("TranslatePlayerXCommand", "TranslatePlayerXCommand", typeof(DungeonNavigationCommands));
        /// <summary>
        /// Command to translate the player along the X axis.
        /// </summary>
        public static RoutedUICommand TranslatePlayerXCommand
        {
            get { return translatePlayerXCommand; }
        }

        private static RoutedUICommand translatePlayerYCommand = new RoutedUICommand("TranslatePlayerYCommand", "TranslatePlayerYCommand", typeof(DungeonNavigationCommands));
        /// <summary>
        /// Command to translate the player along the Y axis.
        /// </summary>
        public static RoutedUICommand TranslatePlayerYCommand
        {
            get { return translatePlayerYCommand; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonNavigationCommands"/> class.
        /// </summary>
        public DungeonNavigationCommands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(TranslatePlayerXCommand, TranslatePlayerXCommand_Executed, TranslatePlayerXCommand_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(TranslatePlayerYCommand, TranslatePlayerYCommand_Executed, TranslatePlayerYCommand_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(RotatePlayerCommand, RotatePlayerCommand_Executed, RotatePlayerCommand_CanExecute));
        }

        #region RotatePlayerCommand
        internal static void RotatePlayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            ViewModels.MazeViewViewModel vmMaze = (ViewModels.MazeViewViewModel)((Window)sender).DataContext;
            string cParam = (string)e.Parameter;
            int rotation = 0;
            int.TryParse(cParam, out rotation);

            vmMaze.UpdatePlayer(0, 0, rotation < 0 ? Enumerations.Direction.West : Enumerations.Direction.East);
        }

        internal static void RotatePlayerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region TranslatePlayerXCommand
        internal static void TranslatePlayerXCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            ViewModels.MazeViewViewModel vmMaze = (ViewModels.MazeViewViewModel)((Window)sender).DataContext;
            string cParam = (string)e.Parameter;
            int xTrans = 0;
            int.TryParse(cParam, out xTrans);

            vmMaze.UpdatePlayer(0, xTrans, null);
        }

        internal static void TranslatePlayerXCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region TranslatePlayerYCommand
        internal static void TranslatePlayerYCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            ViewModels.MazeViewViewModel vmMaze = (ViewModels.MazeViewViewModel)((Window)sender).DataContext;
            string cParam = (string)e.Parameter;
            int yTrans = 0;
            int.TryParse(cParam, out yTrans);

            vmMaze.UpdatePlayer(yTrans, 0, null);
        }

        internal static void TranslatePlayerYCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
    }
}