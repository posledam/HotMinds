using System.Globalization;
using HotMinds.Formatting;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    public class FileSize_Tests
    {
        [Test]
        public void Common_Test()
        {
            var fileSize = new FileSize(1200);
            //TestContext.WriteLine(fileSize.ToPrefix());
            //TestContext.WriteLine(fileSize.ToBinaryPrefix());
            //CultureInfo.CurrentCulture = new CultureInfo("en-US");
            //CultureInfo.CurrentUICulture = new CultureInfo("en-US");
            //TestContext.WriteLine(fileSize.ToPrefix());
            //TestContext.WriteLine(fileSize.ToBinaryPrefix());
        }
    }
}
