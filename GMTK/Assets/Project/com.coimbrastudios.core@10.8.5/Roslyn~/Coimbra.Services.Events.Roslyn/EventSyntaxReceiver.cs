using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Coimbra.Services.Events.Roslyn
{
    public sealed class EventSyntaxReceiver : ISyntaxReceiver
    {
        public readonly List<TypeDeclarationSyntax> Types = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax { Parent: not TypeDeclarationSyntax } typeDeclarationSyntax and (StructDeclarationSyntax or ClassDeclarationSyntax)
             && !typeDeclarationSyntax.Modifiers.Any(SyntaxKind.AbstractKeyword)
             && typeDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword))
            {
                Types.Add(typeDeclarationSyntax);
            }
        }
    }
}
