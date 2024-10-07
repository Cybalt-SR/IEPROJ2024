using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryButtonDisabler : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    //temp
    private void Update()
    {
        button.interactable = SecondaryManager.Instance.hasEquipped;
    }
}
