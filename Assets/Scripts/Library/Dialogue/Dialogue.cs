using System.Collections.Generic;
using UnityEngine;

namespace External.Dialogue
{
    [CreateAssetMenu(menuName = "Modules/Dialogue")]
    public class Dialogue
    {
        [System.Serializable]
        public struct DialogueSprite
        {
            public string id;
            public Sprite image;
        }

        [Header("Settings")]
        public Dialogue prerequisite;
        public bool repeatable;
        public bool manualTrigger;
        public bool closeAfterThis;

        [Header("Characters Here")]
        public List<string> names;
        public List<DialogueSprite> arts;

        [Header("Messages")]
        public List<Message> Messages;

        public Sprite GetImg(string id)
        {
            foreach (var item in arts)
            {
                if (item.id == id)
                {
                    return item.image;
                }
            }

            return null;
        }
    }
}