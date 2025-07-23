using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{

    [SerializeField] private float disappears_after;

    private void Start()
    {
        InvokeControls(true);
    }

    IEnumerator DoAfter(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public void InvokeControls(bool disappearing)
    {
        gameObject.SetActive(true);
        if (disappearing)
            StartCoroutine(DoAfter(disappears_after, DisableControlsScreen));
    }

    public void DisableControlsScreen()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
