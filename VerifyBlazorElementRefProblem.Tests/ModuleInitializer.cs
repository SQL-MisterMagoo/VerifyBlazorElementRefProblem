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
		VerifierSettings.ScrubInlineGuids();
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
		
		HtmlPrettyPrint.All();
		
		VerifierSettings.ScrubLinesContaining("<script src=\"_framework/dotnet.");
		VerifyBunit.Initialize();
	}
}
