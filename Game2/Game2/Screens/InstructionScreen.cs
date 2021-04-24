using System;
using System.Collections.Generic;
using System.Text;

namespace Game2.Screens
{
    public class InstructionScreen : MenuScreen
    {
        public InstructionScreen() : base("Main Menu")
        {
           _menuTitle = "How to play";
            var string1 = new MenuEntry("Control your dragon");
            var stringp = new MenuEntry("as you ascend through the onslaught of oncoming spells");
            var string2 = new MenuEntry("Use the arrow keys to maneuver through the obstacles");
            var string3 = new MenuEntry("Collect the powerup for a speed boost, bonus points, and a refill on lives");
            var string4 = new MenuEntry("If you expend your lives, the game will end as a loss");
            var stringt = new MenuEntry("if you reach a score of 3000 points, the game will end as a win!");
            var string5 = new MenuEntry("Return");

            string5.Selected += ReturnSelected;

            MenuEntries.Add(string1);
            MenuEntries.Add(stringp);
            MenuEntries.Add(string2);
            MenuEntries.Add(string3);
            MenuEntries.Add(string4);
            MenuEntries.Add(stringt);
            MenuEntries.Add(string5);
        }
        private void ReturnSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);
        }

    }
}
