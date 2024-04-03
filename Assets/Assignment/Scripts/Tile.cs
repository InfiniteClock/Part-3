using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum DefenseType { empty, normal, bomb, lightning}
public class Tile : MonoBehaviour
{
    public DefenseType defense;
    public GameObject normalTower;
    public GameObject bombTower;
    public GameObject lightningTower;
    public Color hover;
    public Color select;
    public float radius;
    public Transform rangeIndicator;
    private GameObject currentDefense;      // This will be changed in Feature 3
    private bool isSelected;
    SpriteRenderer spr;
    public Tower tower { get; private set; }
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        Erase();
        isSelected = false;
    }

    public void BuildDefense(DefenseType value)
    {
        if (tower != null)
        {
            Destroy(tower.gameObject);
        }
        switch (value)
        {
            case DefenseType.normal:
                tower = Instantiate(normalTower,transform.position, Quaternion.identity).GetComponent<Tower>();
                defense = DefenseType.normal;
                break;
            case DefenseType.bomb:
                tower = Instantiate(bombTower, transform.position, Quaternion.identity).GetComponent<Tower>();
                defense = DefenseType.bomb;
                break;
            case DefenseType.lightning:
                tower = Instantiate(lightningTower, transform.position, Quaternion.identity).GetComponent<Tower>();
                defense = DefenseType.lightning;
                break;
            case DefenseType.empty:
                break;
        }
    }
    public void UpgradeDefense()
    {
        if (tower != null)
        {
            tower.Upgrade();
        }
    }
    public void SelectThis(bool value)
    {
        isSelected = value;
        if (value)
        {
            spr.color = select;
            rangeIndicator.localScale = new Vector3 (radius+1, radius+1, radius+1);
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
        rangeIndicator.localScale = Vector3.zero;
    }
    public int GetCost(DefenseType type)
    {
        switch (type)
        {
            case DefenseType.normal:
                return normalTower.GetComponent<Tower>().cost;
            case DefenseType.bomb:
                return bombTower.GetComponent<Tower>().cost;
            case DefenseType.lightning:
                return lightningTower.GetComponent<Tower>().cost;
            default:
                return 0;
        }
    }
}
