using NFluent;
using Spark.Database.Reader;
using Xunit;

namespace Spark.Tests.Database
{
    public class TextReaderTests
    {
         private const string Text = @"My first line    
# My second line
        My third line
    My fourth line
Skip me";

        [Fact]
        public void All_Possibilities()
        {
            TextContent content = TextReader.FromString(Text)
                .SkipCommentedLines("#")
                .SkipLines(x => x.Equals("Skip me"))
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent(' ')
                .GetContent();

            Check.That(content.Lines).CountIs(3);

            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(0).Which.IsEqualTo("My first line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(1).Which.IsEqualTo("My third line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(2).Which.IsEqualTo("My fourth line");

            foreach (TextLine line in content.Lines)
            {
                Check.That(line.GetValues()).CountIs(3);
            }
        }

        [Fact]
        public void Just_Get_Content()
        {
            TextContent content = TextReader.FromString(Text)
                .GetContent();

            Check.That(content.Lines).CountIs(5);
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(0).Which.IsEqualTo("My first line    ");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(1).Which.IsEqualTo("# My second line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(2).Which.IsEqualTo("        My third line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(3).Which.IsEqualTo("    My fourth line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(4).Which.IsEqualTo("Skip me");
        }

        [Fact]
        public void Skip_Commented_Lines()
        {
            TextContent content = TextReader.FromString(Text)
                .SkipCommentedLines("#")
                .GetContent();

            Check.That(content.Lines).CountIs(4);
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(0).Which.IsEqualTo("My first line    ");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(1).Which.IsEqualTo("        My third line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(2).Which.IsEqualTo("    My fourth line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(3).Which.IsEqualTo("Skip me");
        }

        [Fact]
        public void Skip_SkipMe_Line()
        {
            TextContent content = TextReader.FromString(Text)
                .SkipLines(x => x.Equals("Skip me"))
                .GetContent();

            Check.That(content.Lines).CountIs(4);
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(0).Which.IsEqualTo("My first line    ");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(1).Which.IsEqualTo("# My second line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(2).Which.IsEqualTo("        My third line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(3).Which.IsEqualTo("    My fourth line");
        }

        [Fact]
        public void Trim_Lines()
        {
            TextContent content = TextReader.FromString(Text)
                .TrimLines()
                .GetContent();

            Check.That(content.Lines).CountIs(5);
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(0).Which.IsEqualTo("My first line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(1).Which.IsEqualTo("# My second line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(2).Which.IsEqualTo("My third line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(3).Which.IsEqualTo("My fourth line");
            Check.That(content.Lines.Extracting(x => x.AsString())).HasElementAt(4).Which.IsEqualTo("Skip me");
        }
    }
}