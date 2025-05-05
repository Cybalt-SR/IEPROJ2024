using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltipable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private ToolTip.InfoData infoData;

	public void SetInfo(ToolTip.InfoData newinfoData)
	{
		infoData = newinfoData;
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		ToolTip.ProxShowInfo(infoData, this.transform);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		ToolTip.ProxHide(this.transform);
	}

	private void OnDisable()
	{
		ToolTip.ProxHide(this.transform);
	}

	private void OnDestroy()
	{
		ToolTip.ProxHide(this.transform);
	}
}
