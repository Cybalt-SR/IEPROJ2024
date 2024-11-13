using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideshowPlayer : MonoBehaviour, IPointerClickHandler
{
    public bool isActive = false;
    private int mCurrentSlide;
    private List<Sprite> mSlides = new List<Sprite>();
    private Image mImage;

    public bool activeTester = false;
    public List<Sprite> testSlides = new List<Sprite>();
    void Start()
    {
        mImage = GetComponentInChildren<Image>();
    }


    void Update()
    {
        if (activeTester)
        {
            StartSlideshow(testSlides, 0);
            activeTester = false;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isActive)
        {
            if (mCurrentSlide < mSlides.Count-1)
            {
                mCurrentSlide++;
                mImage.sprite = mSlides[mCurrentSlide];
                Debug.Log(mCurrentSlide);
            }
            else this.GetComponent<Canvas>().enabled = false;
        }
        
    }

    public void StartSlideshow(List<Sprite> slides, int startSlide)
    {
        mSlides = slides;
        mCurrentSlide = startSlide;
        isActive = true;

        mImage.sprite = mSlides[mCurrentSlide];

        this.GetComponent<Canvas>().enabled = true;
    }
}
