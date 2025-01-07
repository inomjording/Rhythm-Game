using Microsoft.Xna.Framework.Input;

namespace RhythmGame.Utils;

public abstract class KeyUtils
{
    public static bool IsKeyAChar(Keys key)
    {
        return key is >= Keys.A and <= Keys.Z;
    }

    public static bool IsKeyADigit(Keys key)
    {
        return key is >= Keys.D0 and <= Keys.D9 or >= Keys.NumPad0 and <= Keys.NumPad9;
    }

    public static char? GetCharFromKey(Keys key, bool shiftPressed = false, bool capsLockActive = false)
    {
        switch (key)
        {
            // Mapping for alphanumeric keys
            case >= Keys.A and <= Keys.Z:
            {
                // Calculate character
                var baseChar = (char)('a' + (key - Keys.A));
                // Determine if character should be uppercase
                var uppercase = shiftPressed ^ capsLockActive;
                return uppercase ? char.ToUpper(baseChar) : baseChar;
            }
            // Numeric keys
            case >= Keys.D0 and <= Keys.D9:
                return (char)('0' + (key - Keys.D0));
            default:
                // Special characters
                return key switch
                {
                    Keys.Space => ' ',
                    Keys.OemPeriod => '.',
                    Keys.OemComma => ',',
                    Keys.OemMinus => '-',
                    _ => null
                };
        }
    }
}