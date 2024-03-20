using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public Dropdown selector;
    public Villager[] characters;
    public TMP_Text textUI;
    public static CharacterControl Instance;
    public static Villager SelectedVillager { get; private set; }
    private void Start()
    {
        Instance = this;
    }
    public static void SetSelectedVillager(Villager villager)
    {
        if(SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);
        Instance.textUI.text = villager.ToString();
    }
    public void DropdownChange(int value)
    {
        // Note: First value in list is left blank. This causes a nullreference error but this is intentional and doesn't break anything
        SetSelectedVillager(characters[value]);
    }
}
