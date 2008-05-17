using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content;

namespace F2DUnitTests
{
    [TestFixture]
    public class IntTests : TestingGrounds.Game1
    {
        private ContentManager LocalContent;

        [TestFixtureSetUp]
        public void Setup()
        {
            AssemblyUtilities.SetEntryAssembly();
            Uri codeBaseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
            LocalContent = new ContentManager(Services, System.IO.Directory.GetParent(codeBaseUri.AbsolutePath).FullName);
            Run();
        }

        [TestFixtureTearDown]
        public void Destroy()
        {

        }

        [Test]
        public void EmptyTest()
        {
            Assert.Fail();
        }

    }
}