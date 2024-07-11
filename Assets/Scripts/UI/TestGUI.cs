using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Gameplay.Manager;
using Assets.Scripts.Data.Progression;

public class TestGUI : MonoBehaviour, IPlayerSpecificUi
{
    public string PlayerAssigned { get; set; }

    private void OnGUI()
    {
        float scaler = Screen.height * (1.0f / 480.0f);
        int font_size = 13;

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

        GUI.Label(GetNextRect(150, 30), "Attachment Inventory:");
        MakeLine();

        var equipped = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_equipped_dictionary.GetOrCreate(PlayerAssigned);
        var storage = IConsistentDataHolder<PlayerEquipmentData>.Data.owner_attachments_storage_dictionary.GetOrCreate(PlayerAssigned);

        foreach (var stored_item in storage)
        {
            GUI.Label(GetNextRect(100, 30), stored_item.name);

            if (GUI.Button(GetNextRect(50, 30), "EQUIP"))
                continue;
            if (GUI.Button(GetNextRect(50, 30), "DESTROY"))
                continue;

            MakeLine();
        }
    }
}
