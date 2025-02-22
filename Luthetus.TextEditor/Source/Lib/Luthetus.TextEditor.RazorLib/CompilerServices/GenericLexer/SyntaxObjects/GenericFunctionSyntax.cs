﻿using System.Collections.Immutable;
using Luthetus.TextEditor.RazorLib.CompilerServices.GenericLexer.SyntaxEnums;
using Luthetus.TextEditor.RazorLib.Lexes.Models;

namespace Luthetus.TextEditor.RazorLib.CompilerServices.GenericLexer.SyntaxObjects;

public class GenericFunctionSyntax : IGenericSyntax
{
    public GenericFunctionSyntax(TextEditorTextSpan textSpan)
    {
        TextSpan = textSpan;
    }

    public TextEditorTextSpan TextSpan { get; }
    public ImmutableArray<IGenericSyntax> ChildBag => ImmutableArray<IGenericSyntax>.Empty;
    public GenericSyntaxKind GenericSyntaxKind => GenericSyntaxKind.Function;
}