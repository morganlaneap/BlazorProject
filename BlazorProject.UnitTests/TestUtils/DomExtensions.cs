using System;
using AngleSharp.Dom;
using Bunit;

namespace BlazorProject.UnitTests.TestUtils
{
    public static class DomExtensions
    {
        public static IElement FindByText(this IRenderedFragment renderedFragment, string textToFind)
        {
            INode SearchNodes (INodeList nodeList)
            {
                foreach (var node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        var result = SearchNodes(node.ChildNodes);
                        if (result != null) return result;
                    }
                    if (node.TextContent == textToFind) return node; 
                }
                return null;
            }
            return SearchNodes(renderedFragment.Nodes)?.ParentElement;
        }
    }
}