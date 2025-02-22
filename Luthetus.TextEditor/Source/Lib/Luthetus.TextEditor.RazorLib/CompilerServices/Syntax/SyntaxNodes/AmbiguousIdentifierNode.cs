﻿using Luthetus.TextEditor.RazorLib.CompilerServices.Syntax.SyntaxTokens;
using System.Collections.Immutable;

namespace Luthetus.TextEditor.RazorLib.CompilerServices.Syntax.SyntaxNodes;

/// <summary>
/// Example usage: One finds a <see cref="IdentifierToken"/>, but must
/// continue parsing in order to know if it is a reference to a
/// function, type, variable, or etc...
/// </summary>
public sealed record AmbiguousIdentifierNode : ISyntaxNode
{
    public AmbiguousIdentifierNode(IdentifierToken identifierToken)
    {
        IdentifierToken = identifierToken;

        ChildBag = new ISyntax[]
        {
            IdentifierToken,
        }.ToImmutableArray();
    }

    public IdentifierToken IdentifierToken { get; }

    public ImmutableArray<ISyntax> ChildBag { get; }

    public bool IsFabricated { get; init; }
    public SyntaxKind SyntaxKind => SyntaxKind.AmbiguousIdentifierNode;
}
