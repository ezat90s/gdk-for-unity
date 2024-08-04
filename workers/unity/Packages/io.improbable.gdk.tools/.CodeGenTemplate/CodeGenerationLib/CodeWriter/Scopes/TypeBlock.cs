using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Improbable.Gdk.CodeGeneration.CodeWriter.Scopes
{
    public class TypeBlock : ScopeBlock, IAnnotatable
    {
        internal TypeBlock(string declaration, Action<TypeBlock> populate) : base(declaration)
        {
            populate(this);
        }

        public void Line(string snippet)
        {
            Add(new Text(snippet));
        }

        public void Line(Action<StringBuilder> populate)
        {
            var sb = new StringBuilder();
            populate(sb);

            Add(new Text(sb.ToString()));
        }

        public void Line(ICollection<string> snippets)
        {
            if (snippets.Any())
            {
                Add(new TextList(snippets));
            }
        }

        public void Initializer(string declaration, Func<IEnumerable<string>> populate)
        {
            Add(new InitializerBlock(declaration, populate));
        }

        public void CustomScope(Action<CustomScopeBlock> populate)
        {
            Add(new CustomScopeBlock(populate));
        }

        public void CustomScope(string declaration, Action<CustomScopeBlock> populate)
        {
            Add(new CustomScopeBlock(declaration, populate));
        }

        public void Method(string declaration, Action<MethodBlock> populate)
        {
            Add(new MethodBlock(declaration, populate));
        }

        public void Enum(EnumBlock enumBlock)
        {
            Add(enumBlock);
        }

        public void Enum(string declaration, Action<EnumBlock> populate)
        {
            Add(new EnumBlock(declaration, populate));
        }

        public void Type(TypeBlock typeBlock)
        {
            Add(typeBlock);
        }

        public void Type(string declaration, Action<TypeBlock> populate)
        {
            Add(new TypeBlock(declaration, populate));
        }

        public void Annotate(string annotation)
        {
            Annotation = annotation;
        }
    }
}
