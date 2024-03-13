using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterControl : MonoBehaviour
{
    public TMP_Text textUI;
    public static string selection;
    public static Villager SelectedVillager { get; private set; }
    private void Start()
    {
        //If there were a way to not have a villager selected after selecting one, I would implement this differently
        selection = "No Selection";
    }
    private void Update()
    {
        textUI.text = selection;
    }
    public static void SetSelectedVillager(Villager villager)
    {
        if(SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        else
        {
            //There does not exist a deselection method to call this, but if there were, then this would help
            selection = "No Selection";
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);
    }

}
