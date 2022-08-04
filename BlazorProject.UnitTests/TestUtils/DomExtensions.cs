using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Bunit;

namespace BlazorProject.UnitTests.TestUtils
{
    public static class DomExtensions
    {
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
                throw new Exception($"FindByText could not find the text '{text}' in the DOM.");
            }
            return matchedElements;
        }
        
        public static IElement FindByText(this IRenderedFragment renderedFragment, string text)
        {
            var matchedElements = FindAllByText(renderedFragment, text);
            return matchedElements.Count switch
            {
                > 1 => throw new Exception($"FindByText returned more than one element.\r\n\r\n{String.Join("\r\n", matchedElements.Select(x => x.OuterHtml))}"),
                0 => throw new Exception($"FindByText could not find the text '{text}' in the DOM."),
                _ => matchedElements[0]
            };
        }
    }
}