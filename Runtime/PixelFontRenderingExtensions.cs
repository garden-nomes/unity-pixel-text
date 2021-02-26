using UnityEngine;

namespace Unity.PixelText
{
    public static class PixelFontRenderingExtensions
    {
        public static void RenderText(
            this PixelFont font, Texture2D dest, string text, TextAlign align, VerticalAlign valign)
        {
            var srcPixels = font.texture.GetPixels();
            var dstPixels = new Color[dest.width * dest.height];

            var lines = font.SplitIntoLines(text, dest.width);
            int height = lines.Count * font.gridHeight + (lines.Count - 1) * font.lineSpacing;

            int y;

            if (valign == VerticalAlign.Top)
                y = dest.height - font.gridHeight;
            else if (valign == VerticalAlign.Middle)
                y = (dest.height + height) / 2 - font.gridHeight;
            else
                y = height - font.gridHeight;

            foreach (var line in lines)
            {
                int x;

                if (align == TextAlign.Right)
                    x = dest.width - font.GetWidth(line);
                else if (align == TextAlign.Center)
                    x = (dest.width - font.GetWidth(line)) / 2;
                else
                    x = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    x += font.RenderCharacter(dest, srcPixels, dstPixels, line[i], x, y);

                    if (i != line.Length - 1 && line[i] != ' ' && line[i + 1] != ' ')
                        x += font.letterSpacing;
                }

                y -= font.gridHeight + font.lineSpacing;
            }

            dest.SetPixels(dstPixels);
            dest.Apply();
        }

        private static int RenderCharacter(
            this PixelFont font, Texture2D dest, Color[] srcPixels, Color[] dstPixels, char c, int x, int y)
        {
            if (c == ' ' || !font.mappings.ContainsKey(c))
                return font.spaceWidth;

            var rect = font.mappings[c];

            for (int y0 = 0; y0 < rect.height; y0++)
            {
                for (int x0 = 0; x0 < rect.width; x0++)
                {
                    if (x + x0 < 0 || x + x0 >= dest.width || y + y0 < 0 || y + y0 >= dest.height)
                        continue;

                    int srcIndex = (rect.yMin + y0) * font.texture.width + rect.xMin + x0;

                    if (srcPixels[srcIndex].a > 0.2f)
                    {
                        int dstIndex = (y + y0) * dest.width + x + x0;
                        dstPixels[dstIndex] = Color.white;
                    }
                }
            }

            return rect.width;
        }

    }
}
