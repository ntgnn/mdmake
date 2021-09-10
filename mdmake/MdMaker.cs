using mdmake.Extractor;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mdmake
{
    public class MdMaker
    {
        private CSharpCommentExtractor _csextractor;

        public MdMaker()
        {
            _csextractor = new CSharpCommentExtractor();
        }

        public HashSet<string> ReadInput(IEnumerable<string> paths)
        {
            var result = new HashSet<string>();
            foreach (string path in paths)
            {
                if (File.Exists(path))
                    ProcessFile(path, result);
                else if (Directory.Exists(path))
                    ProcessDirectory(path, result);
            }

            return result;
        }

        private void ProcessDirectory(string path, HashSet<string> result)
        {
            var files = Directory.GetFiles(path);
            foreach (string file in files)
                ProcessFile(file, result);

            string[] subdirs = Directory.GetDirectories(path);
            foreach (string subdir in subdirs)
                ProcessDirectory(subdir, result);
        }

        private void ProcessFile(string path, HashSet<string> result)
        {
            var info = new FileInfo(path);
            result.Add(info.FullName);
        }

        public Dictionary<string, string> ExtractFiles(HashSet<string> files)
        {
            var result = new Dictionary<string, string>();
            foreach (var file in files)
            {
                var extension = file.Substring(file.LastIndexOf(".")+1).ToLower();
                var comments = string.Empty;

                switch (extension)
                {
                    case "cs": comments = _csextractor.Extract(file); break;
                    case "php": comments = _csextractor.Extract(file); break;
                    default:
                        break;
                }

                if(!string.IsNullOrEmpty(comments))
                    result.Add(file, comments);
            }
            return result;
        }

        public string GenerateMarkdown(Dictionary<string, string> files, string headerPath)
        {
            var result = new StringBuilder();
            if (!string.IsNullOrEmpty(headerPath))
                result.Append(File.ReadAllText(headerPath));

            foreach (var file in files)
            {
                result.Append(file.Value);
            }

            return result.ToString();
        }

        public void WriteOutput(string content, string path)
        {
            var stream = File.CreateText(path);
            stream.Write(content);
            stream.Close();
        }
    }
}
