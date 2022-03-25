using System.Collections.Generic;

namespace Package.Helper.Scripts
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

        public override string ToString() => 
            $"{Mayor}.{Minor}.{Patch}";
    }
}