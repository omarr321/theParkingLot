using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is the Inventory Manager. It keeps track of the players inventory
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class CarInvManager : MonoBehaviour
{
    //currentEnd is the next empty space in the inv
    private int currentEnd;
    private int numSlots;
    private Item[] items;
    public TMPro.TextMeshProUGUI[] textSlots;
    private WorldManager worldMan;
    private SettingManager settingMan;
    public GameObject carInvPage;
    public GameObject interatePage;
    public GameObject cam;
    // Init the Item Database so it can be used by any script and set default values
    void Start()
    {
        ItemDB.reinitDatabase(this.GetComponent<PlayerManager>());
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();
        currentEnd = worldMan.getInvIndex();
        numSlots = textSlots.Length;
        items = new Item[numSlots];

        /*
        Dictionary<string, Item> tempInv = worldMan.getPlayerInv();
        
        Item tempItem = null;
        for(int i = 0; i < 20; i++) {
            tempInv.TryGetValue("inv" + i, out tempItem);
            items[i] = tempItem;
        }
        */
        updateAllInv();
        interatePage.SetActive(false);
        carInvPage.SetActive(false);
    }

    void Update() {
        LayerMask layerMask = LayerMask.GetMask("Car");
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10.0f)) {
            if (hit.transform.gameObject.tag == "Car") {
                interatePage.SetActive(true);
            } else {
                interatePage.SetActive(false);
            }
        } else {
            interatePage.SetActive(false);
        }
    }

    // Check the val of a inv slot and updates accordingly
    // @Parms int slot : Slot number to check
    private void updateInvSlot(int slot) {
        if (items[slot] == null) {
            textSlots[slot].text = "------";
        } else {
            textSlots[slot].text = items[slot].getName();
        }
    }

    // Check all inv slots and updates it accordingly
    private void updateAllInv() {
        for (int x = 0; x < numSlots; x++) {
            updateInvSlot(x);
        }
    }

    // Adds an Item to the inv
    // @Parms Item item : Item to add to the inv
    // @Return bool : Returns true is it was successful
    public bool addItem(Item item) {
        if (currentEnd == numSlots) {
            return false;
        }
        items[currentEnd] = item;
        updateInvSlot(currentEnd);
        currentEnd++;
        return true;
    }

    // Removes an item from the inv
    // @Parms int index : The index to remove items from
    // @Return bool : Returns true is it was successful
    public bool removeItem(int index) {
        if (index > currentEnd) {
            return false;
        }

        // Shifts all slot up so the empty space is removed
        for (int x = index; x < numSlots; x++) {
            shiftSlotUp(x);
        }
        currentEnd--;
        updateAllInv();
        return true;
    }

    // Removes the last item added to the inv
    // @Return bool : Returns true is it was successful
    public bool removeLastItem() {
        if (currentEnd == 0) {
            return false;
        }

        items[currentEnd-1] = null;
        updateInvSlot(currentEnd-1);
        currentEnd--;
        return true;
    }

    // Shifts a inv item up and replaces the empty slot with null
    // @Parms int index : Slot to shift up
    // @Return bool : Returns true is it was successful
    private bool shiftSlotUp(int index) {
        if (index < numSlots-1) {
            items[index] = items[index+1];
            items[index+1] = null;
        } else {
            items[index] = null;
        }
        return true;
    }

    // Gets an item from the inv
    // @Parms int index : The index of item to get
    // @Return bool : Returns true is it was successful
    public Item getItem(int index) {
        if(index < 0 || index >= numSlots) {
            return null;
        }

        return items[index];
    }

    /*
    public void saveInv() {
        Dictionary<string, Item> tempInv = new Dictionary<string, Item>();
        
        for(int i = 0; i < 20; i++) {
            tempInv.Add("inv" + i, this.items[i]);
        }
        worldMan.setPlayerInv(tempInv);
        worldMan.setInvIndex(this.currentEnd);
    }
    */
}