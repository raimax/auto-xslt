using System;
using System.IO;
using System.Threading;
using System.Xml.Xsl;

namespace Xslt
{
    /// <summary>
    /// Xsl to other file type converter
    /// </summary>
    public class XslTransformer
    {
        /// <summary>
        /// Transforms xsl file to specified file type
        /// </summary>
        /// <param name="xslFileName"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="outputFileName"></param>
        public static void Transform(string xslFileName, string xmlFileName, string outputFileName)
        {
            try
            {
                XslCompiledTransform xslt = new();

                while (true)
                {
                    if (IsFileReady(xslFileName))
                    {
                        xslt.Load(xslFileName);
                        xslt.Transform(xmlFileName, outputFileName);
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Checks if file is ready to be written
        /// </summary>
        /// <param name="sFilename"></param>
        /// <returns>boolean</returns>
        private static bool IsFileReady(string sFilename)
        {
            try
            {
                using FileStream inputStream = File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None);

                return inputStream.Length > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}