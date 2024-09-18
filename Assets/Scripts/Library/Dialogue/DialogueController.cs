using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace External.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        private static DialogueController instance;
        public static DialogueController Instance => instance;

        public static Dialogue Current_Dialogue { get; private set; }
        public static List<Transform> Name_Refs { get; private set; }
        public static Message Current_message { get; private set; }
        public static bool Ended_currentDialogue { get; private set; }
        public static int MessageIndex { get; private set; }

        [Header("Config")]
        [SerializeField] private float time_till_decay = 4;
        [SerializeField] private string player_name;
        [Header("Main References")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image characterImage;
        [SerializeField] private UnityEvent OnDialogueOpen;
        [SerializeField] private UnityEvent OnDialogueClose;


        [Header("Settings")]
        [SerializeField] private float WriteTimePerChar = 0.06f;

        //writing
        private bool currentlyWriting = false;
        private int write_index = 0;
        private float TimeLast_Write = 0;

        //for spring
        private float springSpeed = 0;
        [SerializeField] private float springStartOffset = 50;
        [SerializeField] private float springDampening = 0.8f;
        [SerializeField] private float springFrequency = 0.3f;

        //For delegate
        private static Action onFinishDialogue;

        [SerializeField] private UnityEvent CameraResetPos;
        [SerializeField] private UnityEvent<Transform> CameraLookAt;

        private float time_already_done = 0;

        private void Awake()
        {
            instance = this;
            Name_Refs = new List<Transform>();
        }

        public void Click()
        {
            if (currentlyWriting)
            {
                currentlyWriting = false;

                for (_ = 0; write_index < Current_message.text.Length; write_index++)
                {
                    text.text += Current_message.text[write_index];
                }
            }
            else
            {
                OnDialogueClose.Invoke();
            }
        }

        public void Update()
        {
            if (currentlyWriting)
            {
                time_already_done = 0;
                TimeLast_Write += Time.deltaTime;

                if (TimeLast_Write > WriteTimePerChar)
                {
                    TimeLast_Write = 0;

                    if (write_index < Current_message.text.Length)
                    {
                        text.text += Current_message.text[write_index];
                        write_index++;
                    }
                    else
                    {
                        currentlyWriting = false;
                    }
                }
            }
            else
            {
                time_already_done += Time.deltaTime;

                if (time_already_done > time_till_decay && Current_message != null)
                {
                    Current_message = null;
                    OnDialogueClose.Invoke();
                }
            }

            var imagepos = characterImage.rectTransform.anchoredPosition;

            if (Mathf.Abs(imagepos.y) > 1f)
            {
                springSpeed = Mathf.Lerp(springSpeed, (0 - imagepos.y) * springDampening, springFrequency);
                characterImage.rectTransform.anchoredPosition = new Vector2(imagepos.x, imagepos.y + springSpeed);
            }
            else if (Mathf.Abs(imagepos.y) != 1)
            {
                characterImage.rectTransform.anchoredPosition = new Vector2(imagepos.x, 0);
            }
        }

        public static void LoadDialogue(Dialogue dialogue, List<Transform> nameRefs, Action onFinish)
        {
            Current_Dialogue = dialogue;
            Name_Refs.Clear();

            if (nameRefs != null)
            {
                foreach (var item in nameRefs)
                {
                    Name_Refs.Add(item);
                }
            }

            if (onFinish != null)
            {
                onFinishDialogue += onFinish;
            }

            MessageIndex = 0;
            LoadMessage(dialogue.Messages[0]);

            if (nameRefs != null && nameRefs.Count > 0)
            {
                onFinishDialogue += Instance.CameraResetPos.Invoke;
            }
        }

        public static bool LoadMessage(Message message)
        {
            //anti interrupt check
            if(Instance.time_already_done <= 0)
            {
                return false;
            }

            //cam redir

            if (Name_Refs != null && Name_Refs.Count > 0)
            {
                if (message.name_index >= 0 && message.name_index <= Name_Refs.Count)
                {
                    Instance.CameraLookAt.Invoke(Name_Refs[message.name_index]);
                }

                if (message.name_index == -1)
                {
                    Instance.CameraResetPos.Invoke();
                }
            }

            //image

            ResetImages();

            if (message.character_art != null)
            {
                Instance.characterImage.enabled = true;
                Instance.characterImage.sprite = Current_Dialogue.GetImg(message.character_art);
            }

            if (Current_message != null && message.name_index == Current_message.name_index)
            {
                Instance.characterImage.rectTransform.anchoredPosition = new Vector2(Instance.characterImage.rectTransform.anchoredPosition.x, Instance.springStartOffset);
            }

            //text
            string TextToShow = "";

            if (Current_Dialogue != null)
                TextToShow += (message.name_index == -1) ? instance.player_name : Current_Dialogue.names[message.name_index];

            Instance.write_index = 0;
            Instance.currentlyWriting = true;

            Instance.text.text = TextToShow + System.Environment.NewLine + System.Environment.NewLine;

            //choices
            
            Current_message = message;

            Instance.OnDialogueOpen.Invoke();

            return true;
        }

        public static void ResetImages()
        {
            Instance.characterImage.enabled = false;
            Instance.springSpeed = 0;
        }
    }
}
