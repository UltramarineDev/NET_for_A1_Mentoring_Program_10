using StyleCop;
using StyleCop.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    [SourceAnalyzer(typeof(CsParser))]
    public class MyCustomRules : SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument csharpDocument = (CsDocument)document;
            if (csharpDocument.RootElement != null && !csharpDocument.RootElement.Generated)
            {
                csharpDocument.WalkDocument(
                    new CodeWalkerElementVisitor<object>(this.VisitElement));
            }
        }

        private bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            List<CsElement> elementsList = new List<CsElement>();

            if (!element.Generated)
            {
                if (element.ElementType == ElementType.Class && element.AccessModifier != AccessModifierType.Public)
                {
                    this.AddViolation(element, "ClassAccessModifiersMustBePublic");
                }

                if (element.ElementType == ElementType.Class)
                {
                    elementsList.AddRange(element.ChildElements);
                }

                if (!elementsList.Any(elementsListItem => 
                    elementsListItem.ElementType == ElementType.Field && 
                    (elementsListItem.Name == "Id" || elementsListItem.Name == "Name")))
                {
                    this.AddViolation(element, "ClassShouldContainFieldsIdAndName");
                }

                if (element.ElementType == ElementType.Class && !element.Attributes.Any(atr => atr.Text == "DataContract"))
                {
                    this.AddViolation(element, "ClassShouldMarkedWithDataContractAttribute");
                }
            }

            return true;
        }
    }
}
