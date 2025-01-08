using Microsoft.Xna.Framework.Audio;

namespace RhythmGame.Collections;

public class SoundEffectCollection(SoundEffect confirmSound, SoundEffect selectSound)
{
    private readonly SoundEffectInstance confirmSoundInstance = confirmSound.CreateInstance();
    private readonly SoundEffectInstance selectSoundInstance = selectSound.CreateInstance();
    
    public void PlayConfirmSound()
    {
        PlaySoundEffect(confirmSoundInstance);
    }

    public void PlaySelectSound()
    {
        PlaySoundEffect(selectSoundInstance);
    }

    private static void PlaySoundEffect(SoundEffectInstance soundEffect)
    {
        soundEffect.Stop();
        soundEffect.Play();
    }
}