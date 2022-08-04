using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Bunit;
using static System.String;

namespace BlazorProject.UnitTests.TestUtils
{
    public static class DomExtensions
    {
        public static IElement FindById(this IRenderedFragment renderedFragment, string id)
        {
            var matchedElements = new List<IElement>();
            void SearchNodes (INodeList nodeList)
            {
                foreach (var node in nodeList)
                {
                    if (node.HasChildNodes) SearchNodes(node.ChildNodes);
                    else if (node.ParentElement != null && node.ParentElement is not IHtmlBodyElement && (node as HtmlElement)?.Id == id) matchedElements.Add(node as HtmlElement); 
                }
            }
            SearchNodes(renderedFragment.Nodes);
            return matchedElements.Count switch
            {
                0 => throw new Exception($"No element with id '{id}' could be found in the DOM."),
                > 1 => throw new Exception($"More than one element was found with the id '{id}'.\r\n\r\n{Join("\r\n", matchedElements.Select(x => x.OuterHtml))}"),
                _ => matchedElements[0]
            };
        }
        
        public static List<IElement> FindAllByText(this IRenderedFragment renderedFragment, string text)
        {
            var matchedElements = new List<IElement>();
            void SearchNodes (INodeList nodeList)
            {
                foreach (var node in nodeList)
                {
                    if (node.HasChildNodes) SearchNodes(node.ChildNodes);
                    else if (node.TextContent == text && node.ParentElement != null && node.ParentElement is not IHtmlBodyElement) matchedElements.Add(node.ParentElement); 
                }
            }
            SearchNodes(renderedFragment.Nodes);
            if (matchedElements.Count == 0)
            {
                throw new Exception($"No element could be found with '{text}' in the DOM.");
            }
            return matchedElements;
        }
        
        public static IElement FindByText(this IRenderedFragment renderedFragment, string text)
        {
            var matchedElements = FindAllByText(renderedFragment, text);
            return matchedElements.Count switch
            {
                > 1 => throw new Exception($"More than one element was found with the text '{text}'.\r\n\r\n{Join("\r\n", matchedElements.Select(x => x.OuterHtml))}"),
                _ => matchedElements[0]
            };
        }

        public static IElement FindByLabelText(this IRenderedFragment renderedFragment, string labelText)
        {
            var matchedElements = FindAllByText(renderedFragment, labelText);
            matchedElements = matchedElements.Where(x => x is IHtmlLabelElement).ToList();
            switch (matchedElements.Count)
            {
                case > 1:
                    throw new Exception($"More than one element was found with the text '{labelText}'.\r\n\r\n{Join("\r\n", matchedElements.Select(x => x.OuterHtml))}");
                case 0:
                    throw new Exception($"Could not find a label with the text '{labelText}'.");
            }
            var idFor = matchedElements[0].Attributes.GetNamedItem("for")?.Value;
            if (idFor == null)
            {
                throw new Exception($"Could not find a valid label with the text '{labelText}' in the DOM. Does it have a 'for' attribute?");
            }
            return FindById(renderedFragment, idFor);
        }
    }
}