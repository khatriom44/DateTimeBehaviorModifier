using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace DateTimeBehaviorModifier
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "DateTime Behavior Modifier"),
        ExportMetadata("Description", "Bulk update the behavior of Date/Time fields in Dataverse"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAL6SURBVFhHvVdLTxNRFP5m6BSoghRMCImRBBYkhPigmBiNf8C9Kze4U0iqS2NIXLg2CqT42ODGP+HOhY+o4CPVxCCQJiIspK0WcNopHb9zO2XRJ7dt+iUnvXPP6fnOvXPm3nMMl4CHxQ8JRN7G8XnTRnaf04bhaRoEKXxtBk4PdGD6fC+uTQQ9BSkKAbTPfEMm6QCdJkDjppEXIDSyqH85tAct2PdG1bQKwD/zFQ4V8JO8FcgwiECbCsJ8+i4BJ5ltHbmAXOmEg8WlBIyxBytudMMGfE3e8lrI5hAaDMCMbpG8zZtsJZhnX5js+X2vlXA7+3k5LA5jT06HSWnKoCro6Nfdk0oOFYSOPblrZ17WxcBxvxIZ14SmfY0AqDYlQcQRRcYG56pJsX0NioraABz0pzeBPzE++fLyN4be1EZVEZsDe/63395EJ31VQoUATHTbcdg8HuC3sPV7TwksCzmuqpqIzYE9/2szDY6l48pnORi4HS19UdzKYOonXFNWAiRT+RX0dFnqtxaK7Y1cFomuE3wrPG2LUDEAxFbzv82AEA8OawSQyMB9fNZ7aA6M6x+BIL+MIjRpifWjrgD2nBxG7q/AuPpeiYxlrh5oByBERyaXVGHhPj+nRMYyV08Q2gGMz61idnoI4Yt9eLGyg5fru2r8cGpI6XShHcD35STCF/rUeGsni8ibOFa3M7jJIESni4aS8MpYNy6PHEWad3u90A5gZLwH86+31bjTMjEZCmK0vwOzr7aVThfaASyHhxGOrGGOhAUI+a2FNaXThXYAAa5691lIle+Fz3CBY5kTnS7Kn4Qsz91HZ7yH5sC48YmXQ+ldYqp6vRgsmS89WfceGofyRZ8lILfhuxN1y3ZBrN2xx7u0XIA6EL9CXlz206/FwtQIzf9wl2K8u336768h8NOdkLJcjlFpl1oOck6R25RGUXo1teWtgrRm5BRute/So0mvhhRrMDnVGn3v5SA+xTc5Cn2hoGx7Lh2LNA0liVkvvIQ7VdKeA/8BlHBmFeiJZ8QAAAAASUVORK5CYII= "),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAARaSURBVHhe7Z3JaxRREIdfx8lMEhNNXBBFDCpGEXGLgiteBMG7JxXUi5pgFDwoooh48CRixO2i4vIfeBJvKigxxiUuGBcUdzQJRpOZZMz4Xr1602+cJky6OumZWB8UvyKT6TddVHW/1z1d46QkYgAu3u8APX23HVTx6HMcNPnHeqvjoFNA4K5HRrmffcHkEtD6ZeNAFVuXVKGXTREq4xMOIBHPEo4dfIaeEL2dfdoptWJtUr4Qy9YLOwTmsNTTr1USqyoGjR+dC2rDGUgkIwOjB5+C9lnRF1GOsejV8YiVjQJVmGzk6BDhABJxzt9rT5fw9svvtVMR0cpk0pVER4gLW6eBcgYSceadaEtnYOtHvcIQkREyPQmapHtyra0uA+UMJMIBJOKIfU+saTgyUlYYQWOtWIpxNcYZSCQzgCrzgsy+X3+ybTgYqnFNfKT1yTWzMs5AIhxAIo7Y35p9EqFglcunw3q2bjPlCK52yt2FeSCENC5nIJHgA5iUCY02eUI0y9KvB01I43IGEuEAEgkwgGpT0orkQdqYUCXzj5nXHPm/QVqu45rPGdCuB7OV/xjSNKZM4B07SUVC33j/+q0HVJG6uhY9F2fzTdBxlTHQoGjvTKAnx73iMe4mPe6kiaWgip8l+uZ5j9B33fzAGUiEA0jEZwDV24rEmHh72uJJASaishzQvnzvzjJRLF+T1i8P6EGa2a4yz3HxM8XlgsXYWHnYUWb2x084fAaQMfg7iahpg6Sq6wOoIlWUfSevs8s9yRgqK2QmDDG5juv067tsHRVTQYGU9aWCHOAMJMIBJEIqYfHutVaF+VshYcq1eqZWBZfw8OIvAzt6QVLnFoEWOs6OFvQkVVF0coMzkAgHkAgHkAgHkEhenES6+/TUYVGjOy16+aATPZeaxZWgLQ3utKOsmJ4DfBIJEQ4gkdBK2JStYvSWZtCTdTNAFQ0rx6Pn0njnB+juM29AFb8v1YJSSplLOERCy8A5x9vQE6Juub430bDCzbobbb9AY9bXjddMHw16EjNRcRYfgnyxdxaoHzgDQ4QDSCS0EnY2NqEnt3NtKXoul1v0PPD68y5QxbF1k0BnjnfLzGzHaxu5wiUcInkbwA3zxoCtn12etkSyHyyf4AwkwgEkkhfzwHqcB+6y5oEDwfPAEURBrYVN5u3htfDIgQNIJLQStuEr0v8x/jIQm/Gkzi4ELXScnQ/Rk1QO7ttjnIFEOIBEMp9Yz/VZYezks6qmHFRxa/t09AqH1effgt5+qa9+A7l2asKn1zkDiTiRA+5JJN1QcZCZCHTj46bZzeDyC3vfTC+sQWadgnsmBAQHkIhTe+pVOi+b33VrJ8Jx9cS6Gr6EOxcFg3Ohye3etu0Sd28bELt72xbu3hYIHEAiGT1USw7p7r0JM6dTcA9V7qE6lHj2kTaZqEh0cB9p7iM9hHAAiXiWsI3Xrzk8xl9zUO3f0hRiOeOumwsDivn8aw7DiRB/AdoO5WOhYHxKAAAAAElFTkSuQmCC"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class DateTimeBehaviorModifierPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new DateTimeModifierControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public DateTimeBehaviorModifierPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}