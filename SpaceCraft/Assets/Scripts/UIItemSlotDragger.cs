using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlotDragger : MonoBehaviour
{

    public Toolbar toolbar;
    public Image slotIcon;
    public Image slotImage;
    public Text slotAmount;

    public GameObject oldUI;

    public ItemSlot itemSlot;
    public Texture2D cubeSprite;
    public Sprite TheThingyToShow;
    public string Name;
    public GameObject inHandObject;
    public int itemInIdList;
    public bool isWeapon;
    public bool isBlock;
    public int amount;

    //Store old for transfer
    public ItemSlot tempitemSlot;
    public Texture2D tempcubeSprite;
    public Sprite tempTheThingyToShow;
    public string tempName;
    public GameObject tempinHandObject;
    public int tempitemInIdList;
    public bool tempisWeapon;
    public bool tempisBlock;
    public int tempamount;

    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            GameObject closeUI = FindClosestEnemy();
            if (Vector3.Distance(closeUI.transform.position, transform.position) < 35 && closeUI != oldUI && closeUI.name != "Result")
            {
                Debug.Log("transferring");
                UIItemSlot itemsToTransfer = closeUI.GetComponent<UIItemSlot>();

                //if new UI has items, send those to mouse,
                if (itemsToTransfer.amount > 0)
                {
                    tempitemSlot = itemsToTransfer.itemSlot;
                    tempName = itemsToTransfer.Name;
                    tempcubeSprite = itemsToTransfer.cubeSprite;
                    tempinHandObject = itemsToTransfer.inHandObject;
                    tempitemInIdList = itemsToTransfer.itemInIdList;
                    tempTheThingyToShow = itemsToTransfer.ImageOfThing;
                    tempisWeapon = itemsToTransfer.isWeapon;
                    tempisBlock = itemsToTransfer.isBlock;
                    tempamount = itemsToTransfer.amount;

                    //Transfer items to new place
                    itemsToTransfer.itemSlot = itemSlot;
                    itemsToTransfer.slotIcon.sprite = TheThingyToShow;
                    itemsToTransfer.slotIcon.enabled = true;
                    itemsToTransfer.Name = Name;
                    itemsToTransfer.cubeSprite = cubeSprite;
                    itemsToTransfer.inHandObject = inHandObject;
                    itemsToTransfer.itemInIdList = itemInIdList;
                    itemsToTransfer.ImageOfThing = TheThingyToShow;
                    itemsToTransfer.isWeapon = isWeapon;
                    itemsToTransfer.isBlock = isBlock;
                    itemsToTransfer.amount = amount;
                    itemsToTransfer.slotAmount.text = amount.ToString();
                    itemsToTransfer.slotAmount.enabled = true;

                    UIItemSlot itemsToTransferOLD = oldUI.GetComponent<UIItemSlot>();

                    //Transfer items to new place
                    itemsToTransferOLD.itemSlot = tempitemSlot;
                    itemsToTransferOLD.slotIcon.sprite = tempTheThingyToShow;
                    itemsToTransferOLD.slotIcon.enabled = true;
                    itemsToTransferOLD.Name = tempName;
                    itemsToTransferOLD.cubeSprite = tempcubeSprite;
                    itemsToTransferOLD.inHandObject = tempinHandObject;
                    itemsToTransferOLD.itemInIdList = tempitemInIdList;
                    itemsToTransferOLD.ImageOfThing = tempTheThingyToShow;
                    itemsToTransferOLD.isWeapon = tempisWeapon;
                    itemsToTransferOLD.isBlock = tempisBlock;
                    itemsToTransferOLD.amount = tempamount;
                    itemsToTransferOLD.slotAmount.text = tempamount.ToString();
                    itemsToTransferOLD.slotAmount.enabled = true;

                    itemSlot = null;
                    TheThingyToShow = null;
                    Name = null;
                    tempcubeSprite = null;
                    inHandObject = null;
                    itemInIdList = 0;
                    isWeapon = false;
                    isBlock = false;
                    amount = 0;
                    gameObject.SetActive(false);

                }


                if (itemsToTransfer.amount <= 0) //if new position is empyu
                {
                    //Transfer items to it
                    itemsToTransfer.itemSlot = itemSlot;
                    itemsToTransfer.slotIcon.sprite = TheThingyToShow;
                    itemsToTransfer.slotIcon.enabled = true;
                    itemsToTransfer.Name = Name;
                    itemsToTransfer.cubeSprite = cubeSprite;
                    itemsToTransfer.inHandObject = inHandObject;
                    itemsToTransfer.itemInIdList = itemInIdList;
                    itemsToTransfer.ImageOfThing = TheThingyToShow;
                    itemsToTransfer.isWeapon = isWeapon;
                    itemsToTransfer.isBlock = isBlock;
                    itemsToTransfer.amount = amount;
                    itemsToTransfer.slotAmount.text = amount.ToString();
                    itemsToTransfer.slotAmount.enabled = true;

                    itemSlot = null;
                    TheThingyToShow = null;
                    Name = null;
                    cubeSprite = null;
                    inHandObject = null;
                    itemInIdList = 0;
                    isWeapon = false;
                    isBlock = false;
                    amount = 0;
                    gameObject.SetActive(false);

                    //Remove from old
                    if (oldUI != null)
                    {
                        UIItemSlot RemoveFrom = oldUI.GetComponent<UIItemSlot>();
                        RemoveFrom.itemSlot = null;
                        //RemoveFrom.slotImage = null;
                        RemoveFrom.slotIcon.sprite = null;
                        RemoveFrom.slotIcon.enabled = false;
                        RemoveFrom.ImageOfThing = null;
                        RemoveFrom.Name = null;
                        RemoveFrom.cubeSprite = null;
                        RemoveFrom.inHandObject = null;
                        RemoveFrom.itemInIdList = 0;
                        RemoveFrom.isWeapon = false;
                        RemoveFrom.isBlock = false;
                        RemoveFrom.amount = 0;
                        RemoveFrom.slotAmount.text = amount.ToString();
                        RemoveFrom.slotAmount.enabled = false;
                    }
                }

                toolbar.UpdateStack();
            }
            else
            {
                //return to old ui
                //Transfer items to it
                UIItemSlot itemsToTransfer = oldUI.GetComponent<UIItemSlot>();
                itemsToTransfer.itemSlot = itemSlot;
                itemsToTransfer.slotIcon.sprite = TheThingyToShow;
                itemsToTransfer.slotIcon.enabled = true;
                itemsToTransfer.Name = Name;
                itemsToTransfer.cubeSprite = cubeSprite;
                itemsToTransfer.inHandObject = inHandObject;
                itemsToTransfer.itemInIdList = itemInIdList;
                itemsToTransfer.ImageOfThing = TheThingyToShow;
                itemsToTransfer.isWeapon = isWeapon;
                itemsToTransfer.isBlock = isBlock;
                itemsToTransfer.amount = amount;
                itemsToTransfer.slotAmount.text = amount.ToString();
                itemsToTransfer.slotAmount.enabled = true;

                //set here
                itemSlot = null;
                TheThingyToShow = null;
                Name = null;
                cubeSprite = null;
                inHandObject = null;
                itemInIdList = 0;
                isWeapon = false;
                isBlock = false;
                amount = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("UIItemSlot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}

//Addto toolbar
/*toolbar.itemss[itemsToTransfer.Number].name = Name;
toolbar.itemss[itemsToTransfer.Number].itemInIDList = itemInIdList;
toolbar.itemss[itemsToTransfer.Number].image = TheThingyToShow;
toolbar.itemss[itemsToTransfer.Number].isWeapon = isWeapon;
toolbar.itemss[itemsToTransfer.Number].isBlock = isBlock;
toolbar.itemss[itemsToTransfer.Number].amount = amount;*/



//Empty toolbar
/*toolbar.itemss[RemoveFrom.Number].name = null;
toolbar.itemss[RemoveFrom.Number].itemInIDList = 0;
toolbar.itemss[RemoveFrom.Number].image = null;
toolbar.itemss[RemoveFrom.Number].isWeapon = false;
toolbar.itemss[RemoveFrom.Number].isBlock = false;
toolbar.itemss[RemoveFrom.Number].amount = 0;*/


//replace settings from temp to new
/*itemSlot = tempitemSlot;
//slotImage = tempslotImage;
TheThingyToShow = tempTheThingyToShow;
slotIcon.sprite = TheThingyToShow;
Name = tempName;
itemInIdList = tempitemInIdList;
isWeapon = tempisWeapon;
isBlock = tempisBlock;
amount = tempamount;
oldUI = closeUI;*/




/*else //if not empty
{
    itemSlot = null;
    TheThingyToShow = null;
    name = null;
    itemInIdList = 0;
    isWeapon = false;
    isBlock = false;
    amount = 0;
    gameObject.SetActive(false);
}*/

/*
//remove from here & set inactive
itemSlot = null;
slotImage = null;
TheThingyToShow = null;
slotAmount = null;
Name = null;
itemInIdList = 0;
isWeapon = false;
isBlock = false;
amount = 0;
gameObject.SetActive(false);*/

//update toolbar