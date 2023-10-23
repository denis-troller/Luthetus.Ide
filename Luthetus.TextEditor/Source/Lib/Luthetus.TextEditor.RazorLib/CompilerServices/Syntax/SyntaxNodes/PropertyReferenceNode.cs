﻿using System.Collections.Immutable;
using Luthetus.TextEditor.RazorLib.CompilerServices.Syntax.SyntaxNodes.Expression;
using Luthetus.TextEditor.RazorLib.CompilerServices.Syntax.SyntaxTokens;

namespace Luthetus.TextEditor.RazorLib.CompilerServices.Syntax.SyntaxNodes;

public sealed record PropertyReferenceNode : IExpressionNode
{
    public PropertyReferenceNode(
        IdentifierToken variableIdentifierToken,
        VariableDeclarationStatementNode variableDeclarationStatementNode)
    {
        VariableIdentifierToken = variableIdentifierToken;
        VariableDeclarationStatementNode = variableDeclarationStatementNode;

        ChildBag = new ISyntax[]
        {
            VariableIdentifierToken,
            VariableDeclarationStatementNode,
        }.ToImmutableArray();
    }

    public IdentifierToken VariableIdentifierToken { get; }
    /// <summary>
    /// The <see cref="VariableDeclarationStatementNode"/> is null when the variable is undeclared
    /// </summary>
    public VariableDeclarationStatementNode VariableDeclarationStatementNode { get; }
    public TypeClauseNode TypeClauseNode => VariableDeclarationStatementNode.TypeClauseNode;

    public ImmutableArray<ISyntax> ChildBag { get; }

    public bool IsFabricated { get; init; }
    public SyntaxKind SyntaxKind => SyntaxKind.PropertyReferenceNode;
}
