using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.Extensions;

/// <summary>
///     Basic extensions for the Assembly type, to get necessary values.
/// </summary>
[PublicAPI]
public static class AssemblyExtensions
{
    /// <summary>
    ///     Gets the assembly's identity (name + version).
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>A string in the format NAME vVERSION</returns>
    public static string GetAssemblyIdentity(this Assembly assembly)
    {
        var name = assembly.GetAssemblyName();
        var version = assembly.GetAssemblyVersion();

        return $"{name} v{version}";
    }

    /// <summary>
    ///     Gets the assembly's name
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>The AssemblyName, or UNKNOWN if null.</returns>
    public static string GetAssemblyName(this Assembly assembly)
    {
        return assembly.GetName().Name ?? "UNKNOWN";
    }

    /// <summary>
    ///     Gets the assembly's version
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>The FileVersion, or AssemblyVersion, or UNKNOWN if null.</returns>
    public static string GetAssemblyVersion(this Assembly assembly)
    {
        var location = assembly.Location;
        var name = assembly.GetName();
        var version = name.Version?.ToString();

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

        return version ?? "UNKNOWN";
    }
}