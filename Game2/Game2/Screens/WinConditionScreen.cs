using System;
using System.Collections.Generic;
using System.Text;

namespace Game2.Screens
{
    public class WinConditionScreen : MenuScreen
    {
        public WinConditionScreen() : base("Paused")
        {
            _menuTitle = "You Won!";
            var restartGameMenuEntry = new MenuEntry("Restart Game");
            var quitGameMenuEntry = new MenuEntry("Quit Game");



            restartGameMenuEntry.Selected += Restart;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;


            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        private void Restart(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new GameplayScreen(), ControllingPlayer);
        }

        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";
            var confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        // This uses the loading screen to transition from the game back to the main menu screen.
        private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
