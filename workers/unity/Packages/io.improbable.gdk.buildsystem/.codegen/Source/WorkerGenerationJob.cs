using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Improbable.Gdk.CodeGeneration.FileHandling;
using Improbable.Gdk.CodeGeneration.Jobs;
using Improbable.Gdk.CodeGeneration.Model.Details;
using Newtonsoft.Json.Linq;

namespace Improbable.Gdk.CodeGenerator
{
    public class WorkerGenerationJob : CodegenJob
    {
        private readonly List<string> workerTypesToGenerate;
        private const string WorkerTypeFlag = "+workerType";
        private const string WorkerFileName = "WorkerMenu.cs";
        private const string WorkerListFileName = "WorkerMenu.txt";
        private const string BuildSystemFileName = "Improbable.Gdk.Generated.BuildSystem.asmdef";
        private readonly string relativeOutputPath = Path.Combine("improbable", "buildsystem");
        private readonly string relativeEditorPath = Path.Combine("improbable", "buildsystem", "Editor");

        public WorkerGenerationJob(string baseOutputDir, IFileSystem fileSystem, DetailsStore detailsStore, bool force)
            : base(baseOutputDir, fileSystem, detailsStore, force)
        {
            const string jobName = nameof(WorkerGenerationJob);
            Logger.Info($"Initialising {jobName}.");

            workerTypesToGenerate = ExtractWorkerTypes(CodeGeneratorOptions.Instance.WorkerJsonDirectory);

            var outputFilePaths = new List<string>
            {
                Path.Combine(relativeEditorPath, WorkerFileName),
                Path.Combine(relativeEditorPath, WorkerListFileName),
                Path.Combine(relativeOutputPath, BuildSystemFileName)
            };

            AddOutputFiles(outputFilePaths);
            Logger.Info($"Added {outputFilePaths.Count} job output files.");

            Logger.Info($"Finished initialising {jobName}.");
        }

        protected override void RunImpl()
        {
            Logger.Info($"Generating {WorkerFileName}.");
            var workerCode = UnityWorkerMenuGenerator.Generate(workerTypesToGenerate).Format();
            AddContent(Path.Combine(relativeEditorPath, WorkerFileName), workerCode);

            Logger.Info($"Generating {BuildSystemFileName}.");
            var assemblyCode = BuildSystemAssemblyGenerator.Generate();
            AddContent(Path.Combine(relativeOutputPath, BuildSystemFileName), assemblyCode);

            Logger.Info($"Generating {WorkerListFileName}.");
            AddContent(Path.Combine(relativeEditorPath, WorkerListFileName), string.Join(Environment.NewLine, workerTypesToGenerate));
        }

        private List<string> ExtractWorkerTypes(string path)
        {
            Logger.Info($"Extracting worker types from {path}.");

            var workerTypes = new List<string>();

            var fileNames = Directory.EnumerateFiles(path, "*.json").ToList();
            Logger.Trace($"Found {fileNames.Count()} worker json files:\n - {string.Join("\n - ", fileNames)}");

            foreach (var fileName in fileNames)
            {
                Logger.Trace($"Extracting worker type from {fileName}.");
                var text = File.ReadAllText(fileName);
                if (!text.Contains(WorkerTypeFlag))
                {
                    Logger.Warn($"{fileName} does not contain the '{WorkerTypeFlag}' flag.");
                    continue;
                }

                var jsonRep = JObject.Parse(text);
                var arguments = jsonRep.SelectToken("external.default.windows.arguments");
                if (arguments == null)
                {
                    Logger.Warn($"Could not navigate to external > default > windows > arguments in {fileName}.");
                    continue;
                }

                for (var i = 0; i < arguments.Count() - 1; i++)
                {
                    if (!WorkerTypeFlag.Equals(arguments[i].ToString()))
                    {
                        continue;
                    }

                    var workerType = arguments[i + 1].ToString();
                    Logger.Trace($"Adding {workerType} to list of worker types.");
                    workerTypes.Add(workerType);
                }
            }

            Logger.Info($"Found {workerTypes.Count} worker types:\n - {string.Join("\n - ", workerTypes)}");
            return workerTypes;
        }
    }
}
