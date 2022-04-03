using System;

namespace Xslt
{
    /// <summary>
    /// A class for closing application
    /// </summary>
    public class Exit
    {
        /// <summary>
        /// Prints message to console and exits application
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void Error(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
