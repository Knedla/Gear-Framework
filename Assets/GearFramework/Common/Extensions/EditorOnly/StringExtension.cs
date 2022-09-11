#if UNITY_EDITOR

using System.Linq;

namespace GearFramework.Common.EditorOnly
{
    public static class StringExtension
    {
        public static string AddSpacesBeforeCapital(this string str) => string.Concat(str.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}
#endif
