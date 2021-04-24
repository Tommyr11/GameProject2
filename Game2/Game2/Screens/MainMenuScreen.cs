using System;
using System.Collections.Generic;
using System.Text;
using Game2.StateManagement;
using Microsoft.Xna.Framework;


namespace Game2.Screens
{
    public class MainMenuScreen: MenuScreen
    {
        // The main menu screen is the first thing displayed when the game starts up.

        public MainMenuScreen() : base("Main Menu")
        {
            _menuTitle = "Dragon's Ascent";
            var string1 = new MenuEntry("Play Game");
            var string2 = new MenuEntry("Options");
            var string3 = new MenuEntry("Instructions");
            var string4 = new MenuEntry("Exit");

            string1.Selected += PlayGameMenuEntrySelected;
            string2.Selected += OptionsMenuEntrySelected;
            string3.Selected += InstructionsMenuEntrySelected;
            string4.Selected += OnCancel;

            MenuEntries.Add(string1);
            MenuEntries.Add(string2);
            MenuEntries.Add(string3);
            MenuEntries.Add(string4);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen()); ;
        }


        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }
        private void InstructionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new InstructionScreen(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";
            var confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}

