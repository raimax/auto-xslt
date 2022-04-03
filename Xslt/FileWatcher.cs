using System;
using System.IO;
using System.Threading;

namespace Xslt
{
    /// <summary>
    /// Watches file for changes
    /// </summary>
    public class FileWatcher
    {
        private readonly Config _config;
        private readonly FileSystemWatcher _watcher;

        public FileWatcher(Config config)
        {
            _config = config;

            try
            {
                _watcher = new FileSystemWatcher(_config.Directory);
            }
            catch (Exception ex)
            {
                Exit.Error(ex.Message);
            }
        }

        /// <summary>
        /// Watches xsl file specified in config for changes
        /// </summary>
        public void WatchFile()
        {
            Console.WriteLine("Watching file for change: " + _config.XslFileName);

            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            _watcher.Changed += OnChanged;
            _watcher.Error += OnError;
            _watcher.Filter = _config.XslFileName;
            _watcher.EnableRaisingEvents = true;

            while (true)
            {
                _watcher.WaitForChanged(WatcherChangeTypes.Changed);
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            try
            {
                _watcher.EnableRaisingEvents = false;

                SuccessEvent($"{e.Name} changed, transforming xsl");
                XslTransformer.Transform(_config.XslFileName, _config.XmlFileName, _config.OutputFileName);
                Thread.Sleep(500);
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }


        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            PrintException(e.GetException());
        }

        private static void PrintException(Exception ex)
        {
            if (ex is not null)
            {
                ErrorEvent($"File monitor error: {ex.Message}");
            }
        }

        /// <summary>
        /// Prints green text to console
        /// </summary>
        /// <param name="text">text to print</param>
        public static void SuccessEvent(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints red text to console
        /// </summary>
        /// <param name="text">text to print</param>
        public static void ErrorEvent(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
