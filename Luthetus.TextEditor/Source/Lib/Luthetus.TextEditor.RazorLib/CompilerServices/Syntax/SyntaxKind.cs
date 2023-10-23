﻿namespace Luthetus.TextEditor.RazorLib.CompilerServices.Syntax;

/// <summary>
/// In order to share identical logic with C and CSharp code analysis I need to have them share the SyntaxKind enum. I don't like this because some enum members are used in one language but not the other.
/// </summary>
public enum SyntaxKind
{
    // Tokens Normal
    CommentMultiLineToken,
    CommentSingleLineToken,
    IdentifierToken,
    NumericLiteralToken,
    StringLiteralToken,
    TriviaToken,
    PreprocessorDirectiveToken,
    LibraryReferenceToken,
    PlusToken,
    PlusPlusToken,
    MinusToken,
    MinusMinusToken,
    StarToken,
    DivisionToken,
    EqualsToken,
    EqualsEqualsToken,
    QuestionMarkToken,
    QuestionMarkQuestionMarkToken,
    BangToken,
    StatementDelimiterToken,
    ArraySyntaxToken,
    AssociatedNameToken,
    AssociatedValueToken,
    OpenAssociatedGroupToken,
    CloseAssociatedGroupToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    OpenBraceToken,
    CloseBraceToken,
    OpenAngleBracketToken,
    CloseAngleBracketToken,
    OpenSquareBracketToken,
    CloseSquareBracketToken,
    DollarSignToken,
    ColonToken,
    MemberAccessToken,
    CommaToken,
    BadToken,
    EndOfFileToken,

    // Token Keywords
    AbstractTokenKeyword,
    AsTokenKeyword,
    BaseTokenKeyword,
    BoolTokenKeyword,
    BreakTokenKeyword,
    ByteTokenKeyword,
    CaseTokenKeyword,
    CatchTokenKeyword,
    CharTokenKeyword,
    CheckedTokenKeyword,
    ClassTokenKeyword,
    ConstTokenKeyword,
    ContinueTokenKeyword,
    DecimalTokenKeyword,
    DefaultTokenKeyword,
    DelegateTokenKeyword,
    DoTokenKeyword,
    DoubleTokenKeyword,
    ElseTokenKeyword,
    EnumTokenKeyword,
    EventTokenKeyword,
    ExplicitTokenKeyword,
    ExternTokenKeyword,
    FalseTokenKeyword,
    FinallyTokenKeyword,
    FixedTokenKeyword,
    FloatTokenKeyword,
    ForTokenKeyword,
    ForeachTokenKeyword,
    GotoTokenKeyword,
    IfTokenKeyword,
    ImplicitTokenKeyword,
    InTokenKeyword,
    IntTokenKeyword,
    InterfaceTokenKeyword,
    InternalTokenKeyword,
    IsTokenKeyword,
    LockTokenKeyword,
    LongTokenKeyword,
    NamespaceTokenKeyword,
    NewTokenKeyword,
    NullTokenKeyword,
    ObjectTokenKeyword,
    OperatorTokenKeyword,
    OutTokenKeyword,
    OverrideTokenKeyword,
    ParamsTokenKeyword,
    PrivateTokenKeyword,
    ProtectedTokenKeyword,
    PublicTokenKeyword,
    ReadonlyTokenKeyword,
    RefTokenKeyword,
    ReturnTokenKeyword,
    SbyteTokenKeyword,
    SealedTokenKeyword,
    ShortTokenKeyword,
    SizeofTokenKeyword,
    StackallocTokenKeyword,
    StaticTokenKeyword,
    StringTokenKeyword,
    StructTokenKeyword,
    SwitchTokenKeyword,
    ThisTokenKeyword,
    ThrowTokenKeyword,
    TrueTokenKeyword,
    TryTokenKeyword,
    TypeofTokenKeyword,
    UintTokenKeyword,
    UlongTokenKeyword,
    UncheckedTokenKeyword,
    UnsafeTokenKeyword,
    UshortTokenKeyword,
    UsingTokenKeyword,
    VirtualTokenKeyword,
    VoidTokenKeyword,
    VolatileTokenKeyword,
    WhileTokenKeyword,
    UnrecognizedTokenKeyword,

    // Token Contextual-Keywords
    AddTokenContextualKeyword,
    AndTokenContextualKeyword,
    AliasTokenContextualKeyword,
    AscendingTokenContextualKeyword,
    ArgsTokenContextualKeyword,
    AsyncTokenContextualKeyword,
    AwaitTokenContextualKeyword,
    ByTokenContextualKeyword,
    DescendingTokenContextualKeyword,
    DynamicTokenContextualKeyword,
    EqualsTokenContextualKeyword,
    FileTokenContextualKeyword,
    FromTokenContextualKeyword,
    GetTokenContextualKeyword,
    GlobalTokenContextualKeyword,
    GroupTokenContextualKeyword,
    InitTokenContextualKeyword,
    IntoTokenContextualKeyword,
    JoinTokenContextualKeyword,
    LetTokenContextualKeyword,
    ManagedTokenContextualKeyword,
    NameofTokenContextualKeyword,
    NintTokenContextualKeyword,
    NotTokenContextualKeyword,
    NotnullTokenContextualKeyword,
    NuintTokenContextualKeyword,
    OnTokenContextualKeyword,
    OrTokenContextualKeyword,
    OrderbyTokenContextualKeyword,
    PartialTokenContextualKeyword,
    RecordTokenContextualKeyword,
    RemoveTokenContextualKeyword,
    RequiredTokenContextualKeyword,
    ScopedTokenContextualKeyword,
    SelectTokenContextualKeyword,
    SetTokenContextualKeyword,
    UnmanagedTokenContextualKeyword,
    ValueTokenContextualKeyword,
    VarTokenContextualKeyword,
    WhenTokenContextualKeyword,
    WhereTokenContextualKeyword,
    WithTokenContextualKeyword,
    YieldTokenContextualKeyword,
    UnrecognizedTokenContextualKeyword,

    // Stitching
    CompilationUnit,
    CodeBlockNode,

    // Nodes
    LiteralExpressionNode,
    ParenthesizedExpressionNode,
    PreprocessorLibraryReferenceStatementNode,
    TypeDefinitionNode,
    TypeClauseNode,
    VariableDeclarationStatementNode,
    VariableReferenceNode,
    VariableAssignmentExpressionNode,
    VariableExpressionNode,
    PropertyDeclarationStatementNode,
    PropertyReferenceNode,
    PropertyAssignmentExpressionNode,
    PropertyExpressionNode,
    ConstructorDefinitionNode,
    FunctionDefinitionNode,
    FunctionArgumentsListingNode,
    FunctionArgumentEntryNode,
    FunctionParametersListingNode,
    FunctionParameterEntryNode,
    FunctionInvocationNode,
    GenericArgumentsListingNode,
    GenericArgumentEntryNode,
    GenericParametersListingNode,
    GenericParameterEntryNode,
    InheritanceStatementNode,
    AmbiguousIdentifierNode,
    IfStatementNode,
    ReturnStatementNode,
    NamespaceStatementNode,
    ObjectInitializationNode,
    UsingStatementNode,
    UnaryOperatorNode,
    UnaryExpressionNode,
    BinaryOperatorNode,
    BinaryExpressionNode,
    AttributeNode,
    NamespaceEntryNode,

    // Symbols
    TypeSymbol,
    ConstructorSymbol,
    FunctionSymbol,
    VariableSymbol,
    PropertySymbol,
    StringInterpolationSymbol,
    NamespaceSymbol,
    InjectedLanguageComponentSymbol,
}