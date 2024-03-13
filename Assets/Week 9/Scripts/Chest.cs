using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestType { Villager, Merchant, Archer }
public class Chest : MonoBehaviour
{
    public Animator animator;
    public ChestType chestType;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Villager>(out Villager guy))
        {
            if (guy.canOpen() == chestType || chestType == ChestType.Villager) 
            {
                animator.SetBool("IsOpened", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("IsOpened", false);
    }
}
