using UnityEditor;
using UnityEngine;

namespace Unity.PixelText
{
    [CustomEditor(typeof(PixelFont))]
    public class PixelFontEditor : Editor
    {
        private string _previewText =>
            "The wizard quickly jinxed the gnomes before they vaporized.\n\n" +
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz\n0123456789\n\n" +
            (target as PixelFont).ordering;

        private static int _previewScale = 2;

        Texture2D _previewTexture;

        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            var font = target as PixelFont;

            if (font.texture != null && !font.texture.isReadable)
            {
                GUILayout.Space(EditorGUIUtility.singleLineHeight);
                GUILayout.BeginVertical(new GUIStyle(GUI.skin.box)
                {
                    padding = new RectOffset(16, 16, 16, 16)
                });

                EditorGUILayout.HelpBox("Can't read texture", MessageType.Error);

                GUILayout.Space(EditorGUIUtility.singleLineHeight * 0.5f);

                GUILayout.Label(
                    "To use a texture as a font sheet, it needs \"Read/Write enabled\" checked " +
                    "in its import settings. Click the button below to enable this setting.",
                    new GUIStyle(GUI.skin.label) { wordWrap = true });

                GUILayout.Space(EditorGUIUtility.singleLineHeight * 0.5f);

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                bool button = GUILayout.Button(
                    "Update Import Settings",
                    new GUIStyle(GUI.skin.button) { padding = new RectOffset(16, 16, 8, 8) },
                    GUILayout.ExpandWidth(false));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                if (button) UpdateTextureImportSettings(font.texture);
                GUILayout.EndVertical();
            }
        }

        public override bool HasPreviewGUI() => (target as PixelFont).isValid;
        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            if (Event.current.type != EventType.Repaint)
                return;

            var font = target as PixelFont;

            var textureWidth = Mathf.FloorToInt(r.width / _previewScale);
            var textureHeight = Mathf.FloorToInt(r.height / _previewScale);

            if (textureWidth == 0 || textureHeight == 0)
                return;

            r.width = textureWidth * _previewScale;
            r.height = textureHeight * _previewScale;

            if (_previewTexture == null ||
                _previewTexture.width != textureWidth ||
                _previewTexture.height != textureHeight)
            {
                _previewTexture = new Texture2D(textureWidth, textureHeight, font.texture.format, false);
                _previewTexture.filterMode = FilterMode.Point;
                font.RenderText(_previewTexture, _previewText, TextAlign.Left, VerticalAlign.Top);
            }

            GUI.DrawTexture(r, _previewTexture, ScaleMode.ScaleToFit, false);
        }

        private void UpdateTextureImportSettings(Texture2D texture)
        {
            if (texture == null)
                return;

            string path = AssetDatabase.GetAssetPath(texture);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer == null)
                return;

            importer.isReadable = true;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Point;
            importer.npotScale = TextureImporterNPOTScale.None;

            AssetDatabase.ImportAsset(path);
            AssetDatabase.Refresh();
        }
    }
}
