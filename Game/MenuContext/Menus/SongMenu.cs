using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;

namespace RhythmGame.MenuContext.Menus;

public class SongMenu : Menu
{
    private readonly MenuFactory factory;
    private readonly MenuContext menuContext;
    public SongMenu(SoundEffectCollection soundEffects,
        FontCollection fonts,
        Vector2 position,
        MenuFactory factory,
        MenuContext menuContext) : base(soundEffects,
        fonts,
        position)
    {
        this.factory = factory;
        this.menuContext = menuContext;
        AddSongMenuItem("GET PUMPING!!!");
        AddSongMenuItem("Holy melancholy");
        AddSongMenuItem("7");
        AddMenuItem("Back", () => factory.SwitchCurrentMenu("Main Menu"), Color.Red);
    }

    private void AddSongMenuItem(string songName)
    {
        AddMenuItem(songName, () =>
        {
            var songInfoMenu = factory.CreateSongInfoMenu(songName);
            menuContext.SongInfoMenu = songInfoMenu;
            menuContext.SwitchCurrentMenu("Song Info Menu");
        }, LoadSongDescription(songName));
    }

    private static string LoadSongDescription(string songName)
    {
        var fileLocation = "DanceContext/SongTabs/" + songName + ".txt";
        try
        {
            var metadata = File.ReadLines(fileLocation).First().Split(',');
            return $"Difficulty: {metadata[2]}\nBPM: {metadata[0]}";
        }
        catch (Exception e)
        {
            // TODO: Add logs
            return "";
        }
    }
}