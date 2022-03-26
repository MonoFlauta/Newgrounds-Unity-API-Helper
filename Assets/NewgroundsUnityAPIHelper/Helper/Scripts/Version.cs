using System.Collections.Generic;
using System.Linq;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    public readonly struct Version
    {
        public readonly int Mayor;
        public readonly int Minor;
        public readonly int Patch;

        public Version(int mayor, int minor, int patch)
        {
            Mayor = mayor;
            Minor = minor;
            Patch = patch;
        }

        public Version(IReadOnlyList<int> versionValues)
        {
            Mayor = versionValues[0];
            Minor = versionValues[1];
            Patch = versionValues[2];
        }

        public Version(string version)
        {
            var versionParts = version.Split('.').Select(int.Parse).ToArray();
            Mayor = versionParts[0];
            Minor = versionParts[1];
            Patch = versionParts[2];
        }

        public override string ToString() => 
            $"{Mayor}.{Minor}.{Patch}";
    }
}