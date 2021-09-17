using CommandLine;
using mdmake.CLI;
using System;
using System.Collections.Generic;

namespace mdmake
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        static void RunOptions(Options opts)
        {
            try
            {
                Console.WriteLine("MDMAKE");
                var maker = new MdMaker();
                var analyzed = maker.ReadInput(opts.InputFiles);
                var extracted = maker.ExtractFiles(analyzed);
                Console.WriteLine($"--\nFiles analyzed: {analyzed.Count} | Files extracted: {extracted.Count}");
                var generated = maker.GenerateMarkdown(extracted, opts.HeaderFile);
                maker.WriteOutput(generated, opts.OutputFile);
                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred: " + ex.Message);
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {

        }
    }
}
