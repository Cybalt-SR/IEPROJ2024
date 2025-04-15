using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearConfigScreen : MonoBehaviour
{
    [SerializeField] private GameObject Attachment_Screen;
    [SerializeField] private GameObject Secondary_Screen;

    private int currentScreenIndex = 0;
    private List<GameObject> screenList;

    private void Awake()
    {
        screenList = new List<GameObject>() { Attachment_Screen, Secondary_Screen};
    }

    private void OnEnable()
    {
        ShowScreen();
    }

    private void ShowScreen()
    {
        for (int i = 0; i < screenList.Count; i++)
            screenList[i].SetActive(currentScreenIndex == i);
    }

    public void SetScreen(int index)
    {
        index = Mathf.Clamp(index, 0, screenList.Count-1);
        currentScreenIndex= index;
        ShowScreen();
    }


}
