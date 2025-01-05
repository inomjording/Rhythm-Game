using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.MenuContext.Menus;

public class SongMenu : Menu
{
    private readonly MenuFactory factory;
    private readonly MenuContext menuContext;
    public SongMenu(ContentManager content, SpriteFont font, Vector2 position, MenuFactory factory, MenuContext menuContext) : base(content, font, position)
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
        });
    }
}