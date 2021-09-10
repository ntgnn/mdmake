using System.IO;

namespace test
{
    //! ## Commented Sample
    public class CommentedClass
    {
        //! This  is a quick and dirty way of extracting comments from existing source code (with minimum effort) and turning them into a consolidated README.md file.
        private string ExtractComments(string filename)
        {
            //! 1. First, we get the file's contents.
            string all_text = File.ReadAllText(filename);

            //! 2. Get rid of \" escape sequences.
            all_text = all_text.Replace("\\\"", "");

            //! 3. Process the file, based on some rules
            string comments = "";
            while (all_text.Length > 0)
            {
                // Find the next string or comment.
                int string_pos = all_text.IndexOf("\"");
                int end_line_pos = all_text.IndexOf("//");
                int multi_line_pos = all_text.IndexOf("/*");

                // If there are none of these, we're done.
                if ((string_pos < 0) &&
                    (end_line_pos < 0) &&
                    (multi_line_pos < 0)) break;

                if (string_pos < 0) string_pos = all_text.Length;
                if (end_line_pos < 0) end_line_pos = all_text.Length;
                if (multi_line_pos < 0) multi_line_pos = all_text.Length;

                // See which comes first.
                if ((string_pos < end_line_pos) &&
                    (string_pos < multi_line_pos))
                {
                    // String.
                    // Find its end.
                    int end_pos = all_text.IndexOf("\"", string_pos + 1);

                    // Extract and discard everything up to the string.
                    if (end_pos < 0)
                    {
                        all_text = "";
                    }
                    else
                    {
                        all_text = all_text.Substring(end_pos + 1);
                    }
                }
                else if (end_line_pos < multi_line_pos)
                {
                    // End of line comment.
                    // Find its end.
                    int end_pos =
                        all_text.IndexOf("\r\n", end_line_pos + 2);

                    // Extract the comment.
                    if (end_pos < 0)
                    {
                        comments +=
                            all_text.Substring(end_line_pos) + "\r\n";
                        all_text = "";
                    }
                    else
                    {
                        comments += all_text.Substring(
                            end_line_pos, end_pos - end_line_pos) + "\r\n";
                        all_text = all_text.Substring(end_pos + 2);
                    }
                }
                else
                {
                    // Multi-line comment.
                    // Find its end.
                    int end_pos = all_text.IndexOf(
                        "*/", multi_line_pos + 2);

                    // Extract the comment.
                    if (end_pos < 0)
                    {
                        comments +=
                            all_text.Substring(multi_line_pos) + "\r\n";
                        all_text = "";
                    }
                    else
                    {
                        //! Show some important and magical code:
                        //! ```
                        comments += all_text.Substring(multi_line_pos, end_pos - multi_line_pos + 2) + "\r\n\t";
                        all_text = all_text.Substring(end_pos + 2);
                        //! ```
                    }
                }
            }

            return comments;
        }
    }
}
