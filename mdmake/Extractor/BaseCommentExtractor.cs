using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mdmake.Extractor
{
    public abstract class BaseCommentExtractor
    {
        public const string MD_COMMENT_INDICATOR = "!";

        public const string MD_CODE_SNIPPET = "```";

        public abstract string GetLineCommentStart();

        public string Extract(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var lines = File.ReadAllLines(path);
            var result = new StringBuilder();
            var block_reading = false;

            var line_com_flag = GetLineCommentStart() + MD_COMMENT_INDICATOR;
            var rxtabs = new Regex(@"\t+", RegexOptions.Compiled);
            var rxspac = new Regex(@" +", RegexOptions.Compiled);

            foreach (string line in lines)
            {
                // Check line comment
                var line_com_spos = line.IndexOf(line_com_flag);
                if (line_com_spos >= 0)
                {
                    var extracted = line.Substring(line_com_spos + line_com_flag.Length);
                    result.AppendLine(extracted);

                    if (extracted.Contains(MD_CODE_SNIPPET))
                        block_reading = !block_reading;
                }
                else if (block_reading)
                {
                    var extracted = rxspac.Replace(rxtabs.Replace(line, string.Empty), " ");
                    result.AppendLine(extracted);
                }
            }

            return result.ToString();
        }
    }
}
