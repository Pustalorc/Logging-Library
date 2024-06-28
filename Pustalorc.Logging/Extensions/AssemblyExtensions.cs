using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.Extensions;

[PublicAPI]
public static class AssemblyExtensions
{
    public static string GetAssemblyIdentity(this Assembly assembly)
    {
        var name = assembly.GetName();
        var fullName = name.Name;
        var location = assembly.Location;
        var version = name.Version.ToString();

        if (!string.IsNullOrWhiteSpace(location))
        {
            version = FileVersionInfo.GetVersionInfo(location).FileVersion;
        }
        else
        {
            var reflectedVersion = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
                .Cast<AssemblyFileVersionAttribute>()
                .Select(static attr => attr.Version)
                .FirstOrDefault();

            if (reflectedVersion != null)
                version = reflectedVersion;
        }

        return $"{fullName} v{version}";
    }
}