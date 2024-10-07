using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggler : MonoBehaviour
{
    private RectTransform m_RectTransform;

    [SerializeField] private float lerp_speed;
    [SerializeField] private bool shown;
    [SerializeField] private Vector2 normal_pos;
    [SerializeField] private Vector2 hidden_pos;

    private float t;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void Toggle(bool value)
    {
        shown = value;

    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Lerp(t, shown ? 1 : 0, lerp_speed * Time.deltaTime);

        m_RectTransform.anchoredPosition = Vector2.Lerp(hidden_pos, normal_pos, t);
    }
}
