using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SetImageFillratio : MonoBehaviour
{
    private Image mImage;

    private void Awake()
    {
        mImage = GetComponent<Image>();
    }

    public void SetFillRatio(float ratio)
    {
        mImage.fillAmount = ratio;
    }
}
