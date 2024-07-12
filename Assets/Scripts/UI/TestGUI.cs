using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Data.Progression;
using Assets.Scripts.Data.Pickup;

public class TestGUI : MonoBehaviour, IPlayerSpecificUi
{
    public string PlayerAssigned { get; set; }

    private void OnGUI()
    {
        float scaler = Screen.height * (1.0f / 480.0f);
        int font_size = 11;

        var buttonstyle = new GUIStyle("button");
        buttonstyle.fontSize = font_size * (int)scaler;
        var textstyle = new GUIStyle("label");
        textstyle.fontSize = font_size * (int)scaler;

        Vector2 border = new Vector2(30, 70) * (int)scaler;
        Vector2 placeable_pos = border;
        float height_this_line = 0;

        Rect GetNextRect(float mWidth, float mHeight)
        {
            mWidth *= scaler;
            mHeight *= scaler;

            var oldpos = placeable_pos;

            placeable_pos.x += mWidth;
            height_this_line = Mathf.Max(height_this_line, mHeight);

            return new Rect(oldpos.x, oldpos.y, mWidth, mHeight);
        }

        void MakeLine()
        {
            placeable_pos.x = border.x;
            placeable_pos.y += height_this_line;
            height_this_line = 0;
        }

        GUI.Label(GetNextRect(150, 20), "Attachment Inventory:");
        MakeLine();

        var data = IConsistentDataHolder<PlayerEquipmentData>.Data;
        var equipped = new Dictionary<Attachment.Part, Attachment>(data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned));
        var storage = new List<Attachment>(data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned));

        int storage_index = 0;
        foreach (var stored_item in storage)
        {
            GUI.Label(GetNextRect(100, 20), stored_item.name);

            if (GUI.Button(GetNextRect(50, 20), "EQUIP"))
                data.Equip(PlayerAssigned, storage_index);
            if (GUI.Button(GetNextRect(50, 20), "DESTROY"))
                data.Destroy(PlayerAssigned, storage_index);

            MakeLine();
            storage_index++;
        }

        MakeLine();
        GUI.Label(GetNextRect(150, 20), "Attachments Equipped:");

        MakeLine();
        foreach (var equipped_item in equipped)
        {
            GUI.Label(GetNextRect(200, 20), Enum.GetName(typeof(Attachment.Part), equipped_item.Key) + " => " + equipped_item.Value.name);

            if (GUI.Button(GetNextRect(50, 20), "UNEQUIP"))
                data.UnEquip(PlayerAssigned, equipped_item.Key);

            MakeLine();
        }
    }
}
