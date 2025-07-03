using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Flash()
    {
        StartCoroutine(ChangeMat());
    }

    private IEnumerator ChangeMat()
    {
        Material m_Stored = gameObject.GetComponent<Renderer>().material;
        Material m_New = Resources.Load("Crystal Eye", typeof(Material)) as Material;
        this.gameObject.GetComponent<Renderer>().material = m_New;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Renderer>().material = m_Stored;
    }
}
