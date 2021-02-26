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

        private Texture2D _texture;

        protected override void Start()
        {
            Repaint();
            base.Start();
        }

        public override Texture mainTexture => _texture;

        public string text
        {
            get => _text;
            set
            {
                if (value == _text)
                    return;
                _text = value;
                Repaint();
            }
        }

        public PixelFont font
        {
            get => _props.font;
            set
            {
                if (value == _props.font)
                    return;
                _props.font = value;
                Repaint();
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
                Repaint();
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
                Repaint();
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
                Repaint();
            }
        }

        public override void Rebuild(CanvasUpdate update)
        {
            if (update == CanvasUpdate.PreRender)
                Repaint();

            base.Rebuild(update);
        }

        private void Repaint()
        {
            if (_props.font == null)
                return;

            var width = Mathf.FloorToInt(rectTransform.rect.width * _props.scale);
            var height = Mathf.FloorToInt(rectTransform.rect.height * _props.scale);

            if (width == 0 || height == 0)
            {
                _texture = null;
                return;
            }

            if (_texture == null || _texture.width != width || _texture.height != height)
            {
                _texture = new Texture2D(width, height, _props.font.texture.format, false);
                _texture.filterMode = FilterMode.Point;
                canvasRenderer.SetTexture(_texture);
            }

            _props.font.RenderText(_texture, _text, _props.horizontalAlign, _props.verticalAlign);
        }
    }
}
