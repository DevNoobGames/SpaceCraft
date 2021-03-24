using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Toolbar : MonoBehaviour
{
    public ItemCrafter crafter;
    public UIItemSlot[] slots;
    public RectTransform highlight;
    public Player player;
    public int slotIndex = 0;
    public int ActiveItem;
    //public items[] itemss;

    public TextMeshProUGUI SpaceCactus;
    public TextMeshProUGUI SpaceHouse;
    public TextMeshProUGUI MarkusBlock;
    public TextMeshProUGUI SuperweaponCraft;

    public Material blockMat;

    [System.Serializable]
    public class items
    {
        public string name;
        public int number;
        public int itemInIDList;
        public Sprite image;
        public bool isWeapon;
        public bool isBlock;
        public int amount;
    }


    private void Start()
    {
        ActiveItem = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Number = i;
        }

        UpdateStack();
    }

    public void UpdateStack()
    {

        SuperweaponCraft.fontStyle = FontStyles.Normal;
        SpaceHouse.fontStyle = FontStyles.Normal;
        SpaceCactus.fontStyle = FontStyles.Normal;
        MarkusBlock.fontStyle = FontStyles.Normal;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ImageOfThing != null)
            {
                if (slots[i].amount > 0)
                {/*
                    //slots[i].Name = slots[i].name;
                    slots[i].itemInIdList = slots[i].itemInIdList;
                    slots[i].slotIcon.sprite = slots[i].ImageOfThing;
                    slots[i].slotIcon.enabled = true;
                    //slots[i].ImageOfThing = slots[i].ImageOfThing;
                    slots[i].isWeapon = slots[i].isWeapon;
                    slots[i].isBlock = slots[i].isBlock;
                    slots[i].amount = slots[i].amount;
                    slots[i].slotAmount.text = slots[i].amount.ToString();
                    slots[i].slotAmount.enabled = true;*/
                    slots[i].slotAmount.text = slots[i].amount.ToString();
                    slots[i].slotIcon.sprite = slots[i].ImageOfThing;
                    slots[i].slotIcon.enabled = true;
                    slots[i].slotAmount.enabled = true;
                }
                else
                {
                    //slots[i].inHandObject = null;
                    slots[i].itemInIdList = 0;
                    slots[i].slotIcon.enabled = false;
                    slots[i].slotAmount.enabled = false;
                }


                if (slots[i].itemInIdList == 16)
                {
                    Debug.Log("has one");
                    SpaceHouse.fontStyle = FontStyles.Strikethrough;
                }
                if (slots[i].itemInIdList == 17)
                {
                    Debug.Log("has one");
                    SpaceCactus.fontStyle = FontStyles.Strikethrough;
                }
                if (slots[i].itemInIdList == 21)
                {
                    MarkusBlock.fontStyle = FontStyles.Strikethrough;
                }
                if (slots[i].itemInIdList == 22)
                {
                    SpaceCactus.text = "Follow";
                    SpaceHouse.text = "devNoob";
                    MarkusBlock.text = "on";
                    SuperweaponCraft.text = "Youtube";

                    SuperweaponCraft.fontStyle = FontStyles.Normal;
                    SpaceHouse.fontStyle = FontStyles.Normal;
                    SpaceCactus.fontStyle = FontStyles.Normal;
                    MarkusBlock.fontStyle = FontStyles.Normal;
                }
            }
        }


        if (slots[ActiveItem].inHandObject != null && slots[ActiveItem].amount > 0)
        {
            slots[ActiveItem].inHandObject.SetActive(true);
        }
        else if (slots[ActiveItem].inHandObject != null && slots[ActiveItem].amount <= 0)
        {
            slots[ActiveItem].inHandObject.SetActive(false);
        }



        crafter.CheckNumbers();
    }

    public void AddItemToToolbar(string name, int blockID, Sprite image, bool isweapon, bool isblock, int amount, Texture2D cubeSprite, GameObject inHandObject)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemInIdList == blockID)
            {
                //Debug.Log("Already Have item");
                slots[i].amount += amount;
                UpdateStack();
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].amount == 0)
            {
                slots[i].Name = name;
                slots[i].itemInIdList = blockID;
                slots[i].ImageOfThing = image;
                slots[i].isWeapon = isweapon;
                slots[i].isBlock = isblock;
                slots[i].amount += amount;
                slots[i].cubeSprite = cubeSprite;
                slots[i].inHandObject = inHandObject;
                UpdateStack();
                return;
            }
        }

        return;
    }

    private void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {

            if (scroll > 0)
                slotIndex--;
            else
                slotIndex++;

            if (slotIndex > 8)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = 8;

            highlight.position = slots[slotIndex].slotIcon.transform.position;
            ActiveItem = slotIndex;

            foreach (UIItemSlot item in slots)
            {
                if (item.inHandObject != null)
                {
                    item.inHandObject.SetActive(false);
                }
            }
            if (slots[ActiveItem].inHandObject != null && slots[ActiveItem].amount > 0)
            {
                slots[ActiveItem].inHandObject.SetActive(true);
                if (slots[ActiveItem].isBlock)
                {
                    blockMat.mainTexture = slots[ActiveItem].cubeSprite;
                }
                if (slots[ActiveItem].itemInIdList == 22)
                {
                    player.hasSuperGun = true;
                }
            }


        }


    }

}   