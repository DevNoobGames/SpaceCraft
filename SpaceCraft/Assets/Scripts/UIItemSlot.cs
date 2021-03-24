using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItemSlot : MonoBehaviour, IPointerDownHandler
{
    public GameObject inHandObject;
    public Texture2D cubeSprite;

    public GameObject mouseDragger;
    public ItemCrafter crafter;
    public int Number;

    public bool isLinked = false;
    public ItemSlot itemSlot;
    public Image slotImage;
    public Image slotIcon;
    public Sprite ImageOfThing;
    public Text slotAmount;

    public string Name;
    public int itemInIdList;
    public bool isWeapon;
    public bool isBlock;
    public int amount;

    World world;

    private void Awake()
    {
        world = GameObject.Find("World").GetComponent<World>();
        crafter = GameObject.FindGameObjectWithTag("CraftMenu").GetComponent<ItemCrafter>();
    }



    public void OnPointerDown(PointerEventData eventData) // 3
    {
        if (amount > 0)
        {
            Debug.Log("has " + amount + " stuff");
            mouseDragger.SetActive(true);
            UIItemSlotDragger itemsToTransfer = mouseDragger.GetComponent<UIItemSlotDragger>();
            itemsToTransfer.oldUI = gameObject;
            itemsToTransfer.itemSlot = itemSlot;
            //itemsToTransfer.slotImage = slotImage;
            itemsToTransfer.slotIcon.sprite = ImageOfThing;
            itemsToTransfer.TheThingyToShow = ImageOfThing;
            itemsToTransfer.slotIcon.enabled = true;
            itemsToTransfer.Name = Name;
            itemsToTransfer.inHandObject = inHandObject;
            itemsToTransfer.itemInIdList = itemInIdList;
            itemsToTransfer.isWeapon = isWeapon;
            itemsToTransfer.isBlock = isBlock;
            itemsToTransfer.amount = amount;
            if (inHandObject != null)
            {
                inHandObject.SetActive(false);
            }
            if (cubeSprite != null)
            {
                itemsToTransfer.cubeSprite = cubeSprite;
            }
            slotAmount.enabled = false;
            slotIcon.enabled = false;

            if (name == "Result")
            {
                foreach (UIItemSlot slot in crafter.itemslots)
                {
                    slot.itemSlot = null;
                    //RemoveFrom.slotImage = null;
                    slot.slotIcon.sprite = null;
                    slot.slotIcon.enabled = false;
                    slot.ImageOfThing = null;
                    slot.Name = null;
                    slot.cubeSprite = null;
                    slot.inHandObject = null;
                    slot.itemInIdList = 0;
                    slot.isWeapon = false;
                    slot.isBlock = false;
                    slot.amount = 0;
                    slot.slotAmount.text = amount.ToString();
                    slot.slotAmount.enabled = false;
                }
            }
        }
        else
        {
            Debug.Log("has nada");
        }
    }




    public bool HasItem
    {

        get
        {

            if (itemSlot == null)
                return false;
            else
                return itemSlot.HasItem;

        }

    }

    public void Link(ItemSlot _itemSlot)
    {

        itemSlot = _itemSlot;
        isLinked = true;
        itemSlot.LinkUISlot(this);
        UpdateSlot();

    }

    public void UnLink()
    {

        itemSlot.unLinkUISlot();
        itemSlot = null;
        UpdateSlot();

    }

    public void UpdateSlot()
    {

        if (itemSlot != null && itemSlot.HasItem)
        {

            slotIcon.sprite = world.blocktypes[itemSlot.stack.id].icon;
            slotAmount.text = itemSlot.stack.amount.ToString();
            slotIcon.enabled = true;
            slotAmount.enabled = true;

        }
        else
            Clear();

    }

    public void Clear()
    {

        slotIcon.sprite = null;
        slotAmount.text = "";
        slotIcon.enabled = false;
        slotAmount.enabled = false;

    }

    private void OnDestroy()
    {

        if (itemSlot != null)
            itemSlot.unLinkUISlot();

    }

}

public class ItemSlot
{

    public ItemStack stack = null;
    private UIItemSlot uiItemSlot = null;

    public bool isCreative;

    public ItemSlot(UIItemSlot _uiItemSlot)
    {

        stack = null;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public ItemSlot(UIItemSlot _uiItemSlot, ItemStack _stack)
    {

        stack = _stack;
        uiItemSlot = _uiItemSlot;
        uiItemSlot.Link(this);

    }

    public void LinkUISlot(UIItemSlot uiSlot)
    {

        uiItemSlot = uiSlot;

    }

    public void unLinkUISlot()
    {

        uiItemSlot = null;

    }

    public void EmptySlot()
    {

        stack = null;
        if (uiItemSlot != null)
            uiItemSlot.UpdateSlot();

    }

    public int Take(int amt)
    {

        if (amt > stack.amount)
        {
            int _amt = stack.amount;
            EmptySlot();
            return _amt;
        }
        else if (amt < stack.amount)
        {
            stack.amount -= amt;
            uiItemSlot.UpdateSlot();
            return amt;
        }
        else
        {
            EmptySlot();
            return amt;
        }

    }

    public ItemStack TakeAll()
    {

        ItemStack handOver = new ItemStack(stack.id, stack.amount);
        EmptySlot();
        return handOver;

    }

    public void InsertStack(ItemStack _stack)
    {

        stack = _stack;
        uiItemSlot.UpdateSlot();

    }

    public bool HasItem
    {

        get
        {

            if (stack != null)
                return true;
            else
                return false;

        }

    }

}