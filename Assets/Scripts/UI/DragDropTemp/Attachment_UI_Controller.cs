using Assets.Scripts.Data.Pickup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Attachment_UI_Hook))]
public class Attachment_UI_Controller : MonoBehaviour
{
    private Attachment_UI_Hook hook;
    [SerializeField] private Transform inventory_parent;
    [SerializeField] private GameObject inventoryslot_prefab;

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

			var newslot_button = newslot.GetComponent<Button>();
			var newslot_image = newslot_button.targetGraphic as Image;

			newslot_image.sprite = item.attachment_icon;
			newslot_button.onClick.AddListener(() =>
			{
				hook.Equip(i);
				Refresh();
			});
		}

		foreach (var slot in slots)
		{
			slot.img = 
		}
	}

	private void OnEnable()
	{
		Refresh();
	}
}
