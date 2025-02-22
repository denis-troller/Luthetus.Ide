﻿using Luthetus.Common.RazorLib.Keyboards.Models;

namespace Luthetus.TextEditor.RazorLib.Characters.Models;

public static class CharacterKindHelper
{
    public static CharacterKind CharToCharacterKind(char value)
    {
        if (KeyboardKeyFacts.IsWhitespaceCharacter(value))
            return CharacterKind.Whitespace;
        if (KeyboardKeyFacts.IsPunctuationCharacter(value))
            return CharacterKind.Punctuation;
        return CharacterKind.LetterOrDigit;
    }
}