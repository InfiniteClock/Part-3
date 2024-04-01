using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum DefenseType { empty, normal, bomb, lightning}
public class Tile : MonoBehaviour
{
    public DefenseType defense;
    public Color hover;
    public Color select;
    private GameObject currentDefense;      // This will be changed in Feature 3
    private bool isSelected;
    SpriteRenderer spr;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        Erase();
        isSelected = false;
    }

    public void BuildDefense(DefenseType value)
    {
        if (defense != DefenseType.empty)
        {
            Debug.Log("Replacing defense...");
        }
        else
        {
            Debug.Log("Constructing new defense...");
        }
        Debug.Log(value.ToString()+" construction complete!");
        defense = value;
    }
    public void SelectThis(bool value)
    {
        isSelected = value;
        if (value)
        {
            spr.color = select;
        }
        else
        {
            Erase();
        }
    }
    private void OnMouseOver()
    {
        if (!isSelected)
        {
            spr.color = hover;
        }
    }
    private void OnMouseExit()
    {
        if (!isSelected)
        {
            Erase();
        }
    }
    private void OnMouseDown()
    {
        Controller.SetSelectedTile(this);
    }
    private void Erase()
    {
        Color temp = spr.color;
        temp.a = 0;
        spr.color = temp;
    }
}
