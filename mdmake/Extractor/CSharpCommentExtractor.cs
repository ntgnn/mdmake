namespace mdmake.Extractor
{
    public class CSharpCommentExtractor : BaseCommentExtractor
    {
        public override string GetLineCommentStart()
        {
            return "//";
        }
    }
}
