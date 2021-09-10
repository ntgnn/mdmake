using CommandLine;
using System.Collections.Generic;

namespace mdmake.CLI
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input source files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('o', "output", Default = "README.md", HelpText = "Output markdown generated file.")]
        public string OutputFile { get; set; }

        [Option('h', "header", HelpText = "Header markdown file.")]
        public string HeaderFile { get; set; }
    }
}