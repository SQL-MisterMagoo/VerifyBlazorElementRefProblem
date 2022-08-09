using Bunit;
using Counter = VerifyBlazorElementRefProblem.Pages.Counter;

namespace VerifyBlazorElementRefProblem.Tests
{
	[UsesVerify]
	public class UnitTest1 : TestContext
    {
        [Fact]
        public Task Test1()
        {
			var cut = RenderComponent<Counter>();
			return Verify(cut);
        }
    }
}