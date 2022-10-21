using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is the Inventory Manager. It keeps track of the players inventory
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class InvManager : MonoBehaviour
{
    //currentEnd is the next empty space in the inv
    private int currentEnd;
    private int numSlots;
    private Item[] items;
    public TMPro.TextMeshProUGUI[] textSlots;
    
    // Init the Item Database so it can be used by any script and set default values
    void Start()
    {
        ItemDB.initDatabase(this.GetComponent<PlayerManager>());
        currentEnd = 0;
        numSlots = textSlots.Length;
        items = new Item[numSlots];

        foreach (TMPro.TextMeshProUGUI slotText in textSlots)
        {
            slotText.text = "------";
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
}