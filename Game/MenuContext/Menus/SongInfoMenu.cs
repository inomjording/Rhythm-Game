using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.ScoreContext;

namespace RhythmGame.MenuContext.Menus;

public class SongInfoMenu : Menu
{
    public SongInfoMenu(ContentManager content, SpriteFont font, SpriteFont smallerFont, Vector2 position, ContextManager contextManager,
        MenuContext menuContext, string selectedSong) : base(content, font, smallerFont, position)
    {
        AddMenuItem("Start", () =>
        {
            var danceContext = new DanceContext.DanceContext(content, selectedSong, font, smallerFont, contextManager);
            contextManager.SetContext(danceContext);
            contextManager.LoadContent();
        });
        AddMenuItem("Score", () => 
        {
            var scoreContext = new ScoreScreenContext(content, font, smallerFont, selectedSong);
            contextManager.SetContext(scoreContext);
        });
        AddMenuItem("Back", () => menuContext.SwitchCurrentMenu("Song Menu"), Color.Red);
    }
}