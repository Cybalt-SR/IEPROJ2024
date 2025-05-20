using Assets.Scripts.Controller;
using Cinemachine.Utility;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ToolTip : MonoBehaviour
    {
        public static ToolTip instance;

        [Header("References")]
        [SerializeField] private GameObject main_panel;
		[SerializeField] private RectTransform backpanel;
		[SerializeField] private RectTransform parentcanvas;
		[SerializeField] private TextMeshProUGUI desc;
        [Header("Values")]
		[SerializeField] private float baseHeight = 120;
		[SerializeField] private float lineHeight = 10;
        [Header("Debug")]
        [SerializeField] private Transform target;

        [Serializable]
        public struct InfoData
        {
            public string title;
            [TextArea] public string desc;

            public List<(string statName, string value)> infolist;
        }

        private void Awake()
        {
            instance = this;
        }

        public static void ProxShowInfo(InfoData newInfo, Transform newtarget)
        {
            instance.ShowInfo(newInfo);
			instance.target = newtarget;
            instance.UpdateTick();
        }
        public static void ProxHide(Transform oldtarget)
        {
            if (instance.target != oldtarget)
                return;

			instance.HideInfo();
			instance.target = null;
        }

        public void ShowInfo(InfoData newInfo)
        {
            string newDesc = newInfo.title + System.Environment.NewLine + newInfo.desc;
            newDesc += System.Environment.NewLine;

            if (newInfo.infolist != null)
            {
                foreach (var (statName, value) in newInfo.infolist)
                {
                    newDesc += System.Environment.NewLine + statName + " : " + value;
                }
            }

            var lines = newDesc.Split(System.Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                string curLine = lines[i];
                int charsSinceLastBreak = 0;
                int charsBetweenBreak = 70;
                bool isInsideRichTextTag = false;

                for (int c = 0; c < curLine.Length; c++)
                {
                    var curChar = curLine[c];

                    if (curChar == '<')
                    {
                        isInsideRichTextTag = true;
                    }
                    else if (curChar == '>')
                    {
                        isInsideRichTextTag = false;
                    }

                    if (isInsideRichTextTag == false)
                    {
                        if (charsSinceLastBreak > charsBetweenBreak)
                        {
                            if (curChar == ' ')
                            {
                                charsSinceLastBreak = 0;
                                curLine = curLine.Remove(c, 1);
                                curLine = curLine.Insert(c, System.Environment.NewLine);
                            }
                        }

                        charsSinceLastBreak++;
                    }
                }

                lines[i] = curLine;
            }

            newDesc = "";

            foreach (var line in lines)
            {
                newDesc += line + System.Environment.NewLine;
            }

            main_panel.SetActive(true);

            backpanel.sizeDelta = new Vector2(Screen.width / 2, 1);
            desc.text = newDesc;

            desc.ForceMeshUpdate();

            Vector2 newSize = desc.GetRenderedValues(false);
            backpanel.sizeDelta = newSize + (desc.rectTransform.offsetMax.Abs() + desc.rectTransform.offsetMin.Abs());
        }

        public void HideInfo()
        {
            main_panel.SetActive(false);
        }

        private void Update()
        {
            UpdateTick();
        }

        private void UpdateTick()
        {
            if (target != null)
            {
                transform.position = target.transform.position;
            }

            Vector3 newPos = backpanel.anchoredPosition;
            Vector2 newPivot = new Vector2(0, 0);

            if (newPos.x + backpanel.rect.width > parentcanvas.rect.width)
            {
                newPivot.x = 1;
            }
            else
            {
                newPivot.x = 0;
            }

            if (newPos.y + backpanel.rect.height > parentcanvas.rect.height)
            {
                newPivot.y = 1;
            }
            else
            {
                newPivot.y = 0;
            }

            backpanel.pivot = newPivot;
        }
    }
}
