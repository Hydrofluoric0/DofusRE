using DofusRE.I18n;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace DofusRE.Tests
{
    public class I18nTests
    {
        [Fact]
        public void I18n_Tests()
        {
            var uuid = Guid.NewGuid();
            var output = Path.Combine(Path.GetTempPath(), $"output-{uuid}.d2i");
            File.Create(output).Close();

            // read reference file
            // then write it to an output file
            // and read again from the output file
            var refTexts = I18nReader.Read(@"./assets/i18n_fr.d2i");
            I18nWriter.Write(output, refTexts);
            var testTexts = I18nReader.Read(output);

            // compare read texts count
            Assert.True(testTexts.Count == refTexts.Count);
            
            // deep compare every elements
            for (int i = 0; i < refTexts.Count; i++)
            {
                var reference = refTexts[i];
                var test = testTexts[i];

                if (reference is I18nIndexedText)
                    Assert.Equal((int)reference.Key, (int)test.Key);
                else
                    Assert.Equal((string)reference.Key, (string)test.Key);
                
                Assert.Equal(reference.Text, test.Text);
                
                if (reference is I18nIndexedText)
                {
                    var indexedReference = (I18nIndexedText) reference;
                    var indexedTest = (I18nIndexedText) test;
                    
                    Assert.True(indexedReference.Key == indexedTest.Key);
                    Assert.True(indexedReference.Text == indexedTest.Text);
                    Assert.True(indexedReference.IsDiacritical == indexedTest.IsDiacritical);
                    Assert.True(indexedReference.UndiacriticalText == indexedTest.UndiacriticalText);
                }
            }
        }
    }
}
