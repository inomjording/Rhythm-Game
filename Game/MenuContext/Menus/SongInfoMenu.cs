using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;
using RhythmGame.ScoreContext;

namespace RhythmGame.MenuContext.Menus;

public class SongInfoMenu : Menu
{
    public SongInfoMenu(ContentManager content,
        SoundEffectCollection soundEffects,
        FontCollection fonts,
        Vector2 position,
        ContextManager contextManager,
        MenuContext menuContext,
        string selectedSong) : base(soundEffects,
        fonts,
        position)
    {
        AddMenuItem("Start", () =>
        {
            var danceContext = new DanceContext.DanceContext(content,
                soundEffects,
                selectedSong,
                fonts,
                contextManager);
            contextManager.SetContext(danceContext);
            contextManager.LoadContent();
        });
        AddMenuItem("Score", () => 
        {
            var scoreContext = new ScoreScreenContext(soundEffects, fonts, selectedSong);
            contextManager.SetContext(scoreContext);
        });
        AddMenuItem("Back", () => menuContext.SwitchCurrentMenu("Song Menu"), Color.Red);
    }
}