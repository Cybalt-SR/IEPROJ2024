using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AbilityOP
{
    public class Secondary : ScriptableObject
    {
        public string secondary_name;
        [TextArea(1, 3)] public string tool_tip;
        public Texture2D secondary_icon;

        public string shot_effect_name;
        public string secondary_ability;
    }

    [CustomEditor(typeof(Secondary))]
    public class SecondaryEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}
