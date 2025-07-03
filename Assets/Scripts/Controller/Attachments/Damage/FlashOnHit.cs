using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private Material m_Stored;
    private Material m_New;

	private void Awake()
	{
		m_Stored = gameObject.GetComponent<Renderer>().material;
		m_New = Resources.Load("Crystal Eye", typeof(Material)) as Material;
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
        this.gameObject.GetComponent<Renderer>().material = m_New;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Renderer>().material = m_Stored;
    }
}
