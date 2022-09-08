using UnityEditor;
using UnityEngine;

namespace ShackLab.Editor
{
    [InitializeOnLoad]
    public static class GuidComponentFinder
    {
        private const string LogoDirectory = "Packages/com.marc-antoine-girard.guid-based-reference/Editor/Textures/guid-icon.png";
        private static Texture2D texture;
        
        static GuidComponentFinder()
        {
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>(LogoDirectory);
            if (texture != null)
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyItemGUI;
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemGUI;
            }
        }

        private static void OnHierarchyItemGUI(int instanceID, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null || !go.TryGetComponent(out GuidComponent _))
                return;

            float x = GUI.skin.label.CalcSize(new GUIContent(go.name)).x;
            float offsetRight = 60;

            if (selectionRect.width - x < 18)
                offsetRight += 18 - (selectionRect.width - x);

            float imageSize = selectionRect.height - 4;
            Rect r = new Rect (selectionRect)
            {
                width = imageSize,
                height = imageSize,
                x = selectionRect.width + offsetRight,
                y = selectionRect.y + 2
            };

            GUI.DrawTexture(r, texture);
        }
    }
}