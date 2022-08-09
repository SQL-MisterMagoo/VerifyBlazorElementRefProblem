using System.Runtime.CompilerServices;
using VerifyTests.AngleSharp;
using VerifyTests.Bunit;
using AngleSharp.Diffing.Extensions;
using AngleSharp.Dom;

namespace VerifyBlazorElementRefProblem.Tests;
public static class ModuleInitializer
{
	[ModuleInitializer]
	public static void Initialize()
	{
		// remove some noise from the html snapshot
		VerifierSettings.ScrubEmptyLines();
		BlazorScrubber.ScrubCommentLines();
		VerifierSettings.ScrubLinesWithReplace(s =>
		{
			var scrubbed = s.Replace("<!--!-->", "");
			if (string.IsNullOrWhiteSpace(scrubbed))
			{
				return null;
			}

			return scrubbed;
		});
		
		//HtmlPrettyPrint.All();
		// Hacky hack to remove blazor:elementreference guids
		HtmlPrettyPrint.All(nodeList => BlazorElementReferenceScrubber(nodeList));
		
		VerifierSettings.ScrubLinesContaining("<script src=\"_framework/dotnet.");
		VerifyBunit.Initialize();
	}
	private static void BlazorElementReferenceScrubber(INodeList nodes)
	{
		foreach (var node in nodes)
		{
			if (node.HasChildNodes)
			{
				var childNodes = node.ChildNodes;
				BlazorElementReferenceScrubber(childNodes);
			}

			if (node.TryGetAttr("blazor:elementreference", out var attribute))
			{
				attribute.Value = "elementref_1";
			}
		}

	}
}
