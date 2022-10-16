using System.Collections.Generic;
using DebuggingMod;
using UnityEngine;

namespace PAM.Utility;

public static class ColorUtils
{
    public static Color FromHex(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#" + hex.ToUpper(), out color);
        return color;
    }

    public static Color[] FromHexArray(params string[] hexas)
    {
        List<Color> colorList = new List<Color>();
        foreach (string hexa in hexas)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#" + hexa.ToUpper(), out color);
            colorList.Add(color);
        }
        return colorList.ToArray();
    }

    public static string ToHexRGB(Color color) => ColorUtility.ToHtmlStringRGB(color);

    public static string ToHexRGBA(Color color) => ColorUtility.ToHtmlStringRGB(color);
    public static Color32 AverageColorFromTexture(Sprite tex)
    {
        if (tex is null)
            return Color.black;
        var pixels = tex.texture.GetPixels32();
        int num = pixels.Length;
        float num2 = 0f;
        float num3 = 0f;
        float num4 = 0f;
        int num5 = 0;
        for (int i = 0; i < num; i++)
        {
            bool flag = pixels[i].a == 0;
            if (!flag)
            {
                num5++;
                num2 += (float)pixels[i].r;
                num3 += (float)pixels[i].g;
                num4 += (float)pixels[i].b;
            }
        }
        return new Color32((byte)(num2 / (float)num5), (byte)(num3 / (float)num5), (byte)(num4 / (float)num5), byte.MaxValue);
    }
}