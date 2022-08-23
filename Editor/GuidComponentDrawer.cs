using UnityEditor;
using UnityEngine;

namespace ShackLab.Editor
{
    [CustomEditor(typeof(GuidComponent))]
    public class GuidComponentDrawer : UnityEditor.Editor
    {
        private GuidComponent guidComp;


        private void OnEnable()
        {
            guidComp = (GuidComponent)target;
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.TextField("ID", $"{guidComp.GetGuid()}");
            GUI.enabled = true;
        }
    }
}
