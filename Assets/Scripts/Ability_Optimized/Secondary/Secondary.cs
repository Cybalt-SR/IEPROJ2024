using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Data.Pickup;
using UnityEngine.UI;

namespace AbilityOP
{
    [CreateAssetMenu(fileName = "Secondary", menuName = "Ability Optimized/Secondary", order = 1)]
    public class Secondary : Pickup
    {
        [Header("Secondary Data")]
        public string secondary_name;
        [TextArea(1, 3)] public string tooltip;
        public Texture2D secondary_icon;

        [Header("Assets")]
        public GameObject gauge;

        [Header("Ability Info")]
        public string shot_effect_type;
        public string secondary_ability_type;
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Secondary))]
    public class SecondaryEditor: Editor
    {
        private void RenderValidator(string effect_type, string type_name)
        {
            if (GUILayout.Button($"Validate {effect_type}"))
            {
                Type t = Type.GetType(type_name);
                bool isTypeValid = t != null && t.IsSubclassOf(typeof(Ability)) && !t.IsAbstract;
                Debug.LogWarning($"[DEBUG] \"{type_name}\" is {(isTypeValid ? "" : "not ")}a valid Ability.");
            }
        }

        public override void OnInspectorGUI()
        {
            Secondary secondary = (Secondary)target;

            base.OnInspectorGUI();
            EditorGUILayout.Space(15f);
            RenderValidator("Shot Effect", secondary.shot_effect_type);
            RenderValidator("Secondary Ability", secondary.secondary_ability_type);
        }
    }
    #endif
}
