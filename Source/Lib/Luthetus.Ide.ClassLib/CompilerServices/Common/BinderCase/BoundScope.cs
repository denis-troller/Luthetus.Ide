﻿using Luthetus.Ide.ClassLib.CompilerServices.Common.BinderCase.BoundNodes.Statements;

namespace Luthetus.Ide.ClassLib.CompilerServices.Common.BinderCase;

public sealed record BoundScope
{
    public BoundScope(
        BoundScope? parent,
        Type? scopeReturnType,
        int startingIndexInclusive,
        int? endingIndexExclusive,
        Dictionary<string, Type> typeMap,
        Dictionary<string, BoundClassDeclarationNode> classDeclarationMap,
        Dictionary<string, BoundFunctionDeclarationNode> functionDeclarationMap,
        Dictionary<string, BoundVariableDeclarationStatementNode> variableDeclarationMap)
    {
        Parent = parent;
        ScopeReturnType = scopeReturnType;
        StartingIndexInclusive = startingIndexInclusive;
        EndingIndexExclusive = endingIndexExclusive;
        TypeMap = typeMap;
        ClassDeclarationMap = classDeclarationMap;
        FunctionDeclarationMap = functionDeclarationMap;
        VariableDeclarationMap = variableDeclarationMap;
    }

    public BoundScope? Parent { get; }
    /// <summary>A <see cref="ScopeReturnType"/> with the value of "null" means refer to the <see cref="Parent"/> bound scope's <see cref="ScopeReturnType"/></summary>
    public Type? ScopeReturnType { get; }
    public int StartingIndexInclusive { get; }
    /// <summary>TODO: Remove the "internal set" hack and make a new immutable instance instead.</summary>
    public int? EndingIndexExclusive { get; internal set; }
    public Dictionary<string, Type> TypeMap { get; }
    public Dictionary<string, BoundClassDeclarationNode> ClassDeclarationMap { get; }
    public Dictionary<string, BoundFunctionDeclarationNode> FunctionDeclarationMap { get; }
    public Dictionary<string, BoundVariableDeclarationStatementNode> VariableDeclarationMap { get; }
}
