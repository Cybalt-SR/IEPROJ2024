using Assets.Scripts.Data.Pickup;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(Attachment_UI_Hook))]
public class Attachment_UI_Controller : MonoBehaviour
{
    private Attachment_UI_Hook hook;
    [SerializeField] private Sprite default_sprite;
    [SerializeField] private Transform inventory_parent;
    [SerializeField] private GameObject inventoryslot_prefab;

	[Serializable]
	private class AttachmentUiSlot
	{
		public Button button;
		public Attachment.Part part;
	}

	[SerializeField] private List<AttachmentUiSlot> slots;

	private void Awake()
	{
		hook = GetComponent<Attachment_UI_Hook>();
	}

	void Refresh()
	{
		foreach (Transform child in inventory_parent)
		{
			if (child.gameObject == inventoryslot_prefab)
				continue;

			Destroy(child.gameObject);
		}

		var inventory = hook.GetAttachments();

		for (int i = 0; i < inventory.Length; i++)
		{
			var item = inventory[i];

			var newslot = Instantiate(inventoryslot_prefab, inventory_parent);
			newslot.SetActive(true);

			//button
			var newslot_button = newslot.GetComponent<Button>();
			var newslot_image = newslot_button.targetGraphic as Image;

			newslot_image.sprite = item.attachment_icon;
			newslot_button.onClick.RemoveAllListeners();
			var index = i; // Capture the current index

			newslot_button.onClick.AddListener(() =>
			{
				hook.Equip(index);
				Refresh();
			});

			//tooltip
			var newslot_tooltip = newslot.GetComponent<Tooltipable>();
			newslot_tooltip.SetInfo(new ToolTip.InfoData(){
				title = item.name,
				desc = item.attachment_description
			});
		}

		foreach (var slot in slots)
		{
			//icon
			var slotimg = slot.button.targetGraphic as Image;

			var equipped = hook.GetEquipped(slot.part);

			if(equipped != null)
				slotimg.sprite = equipped.attachment_icon;
			else
				slotimg.sprite = default_sprite;

			//tooltip
			var newslot_tooltip = slot.button.gameObject.GetComponent<Tooltipable>();
			ToolTip.InfoData newinfo;
			if (equipped != null)
			{
				newinfo = new ToolTip.InfoData()
				{
					title = equipped.name,
					desc = equipped.attachment_description
				};
			}
			else
			{
				newinfo = new ToolTip.InfoData()
				{
					title = "",
					desc = ""
				};
			}
			newslot_tooltip.SetInfo(newinfo);

			//Functionality
			slot.button.onClick.RemoveAllListeners();
			slot.button.onClick.AddListener(() =>
			{
				hook.UnEquip(slot.part);
				Refresh();
			});
		}
	}

	private void OnEnable()
	{
		Refresh();
	}
}
