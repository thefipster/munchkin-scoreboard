﻿using CommandLine;

namespace TheFipster.Munchkin.Console.StashPlugin.Verbs
{
    [Verb("dryrun", HelpText = "Add file contents to the index.")]
    public class DryrunVerb
    {
        [Option('f', "file", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
    }
}
