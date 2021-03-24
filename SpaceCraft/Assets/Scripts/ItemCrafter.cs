using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ItemCrafter : MonoBehaviour
{
    public UIItemSlot[] itemslots;
    public int[] itemNumbers;
    public GameObject result;

    public Sprite GunSprite;
    public GameObject GunInHandObject;

    public void CheckNumbers()
    {
        /*foreach (UIItemSlot item in itemslots)
        {
            itemNumbers[item] = item.itemInIdList;
        }*/
        for (int i = 0; i < itemslots.Length; i++)
        {
            itemNumbers[i] = itemslots[i].itemInIdList;
        }
        Combinations();
    }

    public void Combinations()
    {
        //14 = gun, 16 = house, 21 = markus, 17 is cactus
        if (itemNumbers.Contains(14) && itemNumbers.Contains(16) && itemNumbers.Contains(21) && itemNumbers.Contains(17))
        {
            UIItemSlot resultSlot = result.GetComponent<UIItemSlot>();
            //resultSlot.itemSlot = itemSlot;
            resultSlot.slotIcon.sprite = GunSprite;
            resultSlot.slotIcon.enabled = true;
            resultSlot.Name = "Mining Gun Strong";
            resultSlot.itemInIdList = 22;
            resultSlot.ImageOfThing = GunSprite;
            resultSlot.inHandObject = GunInHandObject;
            resultSlot.isWeapon = true;
            resultSlot.isBlock = false;
            resultSlot.amount = 1;
            resultSlot.slotAmount.text = resultSlot.amount.ToString();
            resultSlot.slotAmount.enabled = true;

        }
    }



}
