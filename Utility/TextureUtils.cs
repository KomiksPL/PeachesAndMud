

using System.Collections.Generic;
using UnityEngine;

namespace PAM.Utility
{
  public static class TextureUtils
  {
    public static Texture2D CreateRamp(Color a, Color b)
    {
      Texture2D texture2D = new Texture2D(128, 32);
      for (int x = 0; x < 128; ++x)
      {
        Color color = Color.Lerp(a, b, x / (float) sbyte.MaxValue);
        for (int y = 0; y < 32; ++y)
          texture2D.SetPixel(x, y, color);
      }
      texture2D.name = string.Format("generatedTexture-{0}", texture2D.GetInstanceID());
      texture2D.Apply();
      return texture2D;
    }

    public static Texture2D CreateRamp(Color a, Color b, params Color[] others)
    {
      Texture2D texture2D = new Texture2D(128, 32);
      List<Color> colorList = new List<Color>
      {
        a,
        b
      };
      colorList.AddRange(others);
      int num = Mathf.RoundToInt(128f / (colorList.Count - 1));
      for (int x = 0; x < 128; ++x)
      {
        Color color = Color.Lerp(colorList[0], colorList[1], x % num / (num - 1));
        if (x % num == num - 1)
          colorList.RemoveAt(0);
        for (int y = 0; y < 32; ++y)
          texture2D.SetPixel(x, y, color);
      }
      texture2D.name = string.Format("generatedTexture-{0}", texture2D.GetInstanceID());
      texture2D.Apply();
      return texture2D;
    }

    public static Texture2D CreateRamp(string hexA, string hexB)
    {
      Color color1;
      ColorUtility.TryParseHtmlString("#" + hexA.ToUpper(), out color1);
      Color color2;
      ColorUtility.TryParseHtmlString("#" + hexB.ToUpper(), out color2);
      return CreateRamp(color1, color2);
    }

    public static Texture2D CreateColorTexture(Color hex)
    {
      Texture2D texture2D = new Texture2D(128, 32);
      for (int x = 0; x < 128; ++x)
      {
        
        for (int y = 0; y < 32; ++y)
          texture2D.SetPixel(x, y, hex);
      }
      texture2D.name = string.Format("generatedTexture-{0}", texture2D.GetInstanceID());
      texture2D.Apply();
      return texture2D;
    }

    public static Texture2D CreateRamp(string hexA, string hexB, params string[] hexs)
    {
      Color color1;
      ColorUtility.TryParseHtmlString("#" + hexA.ToUpper(), out color1);
      Color color2;
      ColorUtility.TryParseHtmlString("#" + hexB.ToUpper(), out color2);
      List<Color> colorList = new List<Color>();
      foreach (string str in hexs)
      {
        Color color3;
        ColorUtility.TryParseHtmlString("#" + str.ToUpper(), out color3);
        colorList.Add(color3);
      }
      return CreateRamp(color1, color2, colorList.ToArray());
    }
  }
}