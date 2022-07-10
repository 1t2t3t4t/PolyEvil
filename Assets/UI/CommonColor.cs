using UnityEngine;

namespace UI
{
    public static class CommonColor
    {
        public static Color BloodRed()
        {
            ColorUtility.TryParseHtmlString("#BE3333", out var outColor);
            return outColor;
        }
    }
}