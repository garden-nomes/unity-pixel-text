using UnityEngine;
using UnityEngine.UI;

namespace Unity.PixelText
{
    public enum TextAlign { Left, Center, Right }
    public enum VerticalAlign { Top, Middle, Bottom }

    [AddComponentMenu("UI/Pixel Text")]
    [RequireComponent(typeof(CanvasRenderer))]
    public class PixelText : MaskableGraphic
    {
        [SerializeField, TextArea(3, 10)] string _text = "";
        [SerializeField] TextProperties _props = TextProperties.defaultProperties;

        public override Texture mainTexture =>
            font.texture == null ? base.mainTexture : font.texture;

        public string text
        {
            get => _text;
            set
            {
                if (value == _text)
                    return;
                _text = value;
                SetVerticesDirty();
            }
        }

        public BitmapFont font
        {
            get => _props.font;
            set
            {
                if (value == _props.font)
                    return;
                _props.font = value;
                SetVerticesDirty();
            }
        }

        public float scale
        {
            get => _props.scale;
            set
            {
                if (value == _props.scale)
                    return;
                _props.scale = value;
                SetVerticesDirty();
            }
        }

        public TextAlign horizontalAlign
        {
            get => _props.horizontalAlign;
            set
            {
                if (value == _props.horizontalAlign)
                    return;
                _props.horizontalAlign = value;
                SetVerticesDirty();
            }
        }

        public VerticalAlign verticalAlign
        {
            get => _props.verticalAlign;
            set
            {
                if (value == _props.verticalAlign)
                    return;
                _props.verticalAlign = value;
                SetVerticesDirty();
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (font == null || !font.isValid)
                return;

            vh.Clear();

            var glyphs = font.RenderText(
                text, rectTransform.rect, scale, horizontalAlign, verticalAlign, color);

            foreach (var glyph in glyphs)
            {
                var uv = glyph.uvRect;
                var dest = glyph.destinationRect;

                var bottomLeft = new UIVertex()
                {
                    position = new Vector3(dest.xMin, dest.yMin, 0f),
                    uv0 = new Vector2(uv.xMin, uv.yMin),
                    color = color
                };

                var bottomRight = new UIVertex()
                {
                    position = new Vector3(dest.xMax, dest.yMin, 0f),
                    uv0 = new Vector2(uv.xMax, uv.yMin),
                    color = color
                };

                var topRight = new UIVertex()
                {
                    position = new Vector3(dest.xMax, dest.yMax, 0f),
                    uv0 = new Vector2(uv.xMax, uv.yMax),
                    color = color
                };

                var topLeft = new UIVertex()
                {
                    position = new Vector3(dest.xMin, dest.yMax, 0f),
                    uv0 = new Vector2(uv.xMin, uv.yMax),
                    color = color
                };

                vh.AddUIVertexQuad(new UIVertex[] { bottomLeft, topLeft, topRight, bottomRight });
            }
        }
    }
}
