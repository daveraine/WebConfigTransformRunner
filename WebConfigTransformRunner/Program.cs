namespace WebConfigTransformRunner
{
    using System;
    using System.IO;
    using Microsoft.Web.XmlTransform;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Wrong number of arguments");
                Console.WriteLine("wct.exe ConfigFilename TransformFilename ResultFilename");
                Environment.Exit(1);
            }
            if (!File.Exists(args[0]) && !File.Exists(args[1]))
            {
                Console.WriteLine("The config or transform file do not exist!");
                Environment.Exit(2);
            }
            using (var doc = new XmlTransformableDocument())
            {
                doc.Load(args[0]);
                using (var tranform = new XmlTransformation(args[1]))
                {
                    if (tranform.Apply(doc))
                    {
                        doc.Save(args[2]);
                    }
                    else
                    {
                        Console.WriteLine("Could not apply transform");
                        Environment.Exit(3);
                    }
                }
            }
        }
    }
}