using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class handles the interactable of the GUI inv
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class InvInteractable : MonoBehaviour
{
    // Colors for setting text colors
    private Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color Yellow = new Color(0.96f, 0.65f, 0.14f, 1.0f);
    
    // Ref to both current and last highlighted cell and selected cell
    private TMPro.TextMeshProUGUI selectedCell;
    private TMPro.TextMeshProUGUI lastSelectedCell;
    private TMPro.TextMeshProUGUI highlightedCell;
    private TMPro.TextMeshProUGUI lastHighlightedCell;
    
    // Ref to slot zero, a zero width and hight slot
    // NOTE: This is needed for the highlight to work
    private TMPro.TextMeshProUGUI slotZ;
    
    private Item selectedItem;
    
    public InvManager invMan;
    public InvLoreController invLore;
    public TMPro.TextMeshProUGUI itemName;
    public TMPro.TextMeshProUGUI itemDesc;
    public TMPro.TextMeshProUGUI itemHealthValue;
    public TMPro.TextMeshProUGUI itemHungerValue;
    public TMPro.TextMeshProUGUI itemHydroValue;
    public UnityEngine.UI.Button consumeButton;
    public UnityEngine.UI.Button loreButton;
    // Start set default values and set slot zero
    void Start()
    {
        invLore.gameObject.SetActive(false);
        selectedCell = null;
        lastSelectedCell = null;
        highlightedCell = null;
        lastHighlightedCell = null;
        slotZ = GameObject.Find("Slot-0").GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Chancges the highlighted text to the next text and changes the color accordingly
    // @Parms TMPro.TextMeshProUGUI text : current highlighted text
    public void chanceHighlighted(TMPro.TextMeshProUGUI text)
    {
        if (highlightedCell != text) {
            lastHighlightedCell = highlightedCell;
            highlightedCell = text;
        }

        if (highlightedCell != null) {
            highlightedCell.color = Yellow;
        }

        if (lastHighlightedCell != null) {
            lastHighlightedCell.color = White;
        }
    }

    // Changes the current selected text
    // @Parms TMPro.TextMeshProUGUI text : current selected text
    public void chanceCurrent(TMPro.TextMeshProUGUI text)
    {
        if (selectedCell != text) {
            lastSelectedCell = selectedCell;
            selectedCell = text;
            selectedItem = invMan.getItem(int.Parse(text.name.Split("-")[1])-1);
            updateUI(selectedItem);
        }

        if (selectedCell != null) {
            selectedCell.fontStyle = TMPro.FontStyles.Bold;
        }

        if (lastSelectedCell != null) {
            lastSelectedCell.fontStyle = TMPro.FontStyles.Normal;
        }
    }

    // Updates the item text to display the infomation of the item provoded
    // @Parms Item item : The item information to use
    private void updateUI(Item item) {
        updateButtons(item);
        updateText(item);
        updateValues(item);
    }

    // Updates the buttons so it only shows buttons that can be used on the item
    // @Parms Item item : The item information to use
    private void updateButtons(Item item) {
        consumeButton.gameObject.SetActive(false);
        loreButton.gameObject.SetActive(false);
        if (item == null) {
            return;
        }

        if (item.GetType() == typeof(Lore)) {
            loreButton.gameObject.SetActive(true);
        } else if (item.GetType() == typeof(Eatable)){
            consumeButton.gameObject.SetActive(true);
        }
    }

    // Updates the item text to show information of the item given
    // @Parms Item item : The item information to use
    private void updateText(Item item) {
        if (item == null) {
            itemName.text = "";
            itemDesc.text = "";
        } else {
           itemName.text = item.getName();
           itemDesc.text = item.getDesc(); 
        }
    }

    // Updates the Health/Hunger/Hydro values to repersent the item information
    // @Parms Item item : The item information to use
    private void updateValues(Item item) {
            itemHealthValue.text = "";
            itemHungerValue.text = "";
            itemHydroValue.text = "";
            if (item == null) {
                return;
            }

            if (item.GetType() == typeof(Eatable)) {
                Eatable temp = (Eatable)item;
                itemHealthValue.text = "Health: " + temp.getHealthReg();
                itemHungerValue.text = "Saturation: " + temp.getSaturation();
                itemHydroValue.text = "Hydration: " + temp.getHydration();
            }
    }

    // Consumes the Item that is currently selected
    public void consumeItem()
    {
        Eatable temp = (Eatable)this.selectedItem;
        temp.consume();
        invMan.removeItem(int.Parse(selectedCell.name.Split("-")[1])-1);
        this.chanceCurrent(slotZ);
    }

    // Open the Lore Window with the current item
    public void openLore()
    {
        Lore temp = (Lore)this.selectedItem;
        invLore.gameObject.SetActive(true);
        StartCoroutine(invLore.setText(temp.getName(), temp.getLore()));
        this.chanceCurrent(slotZ);
    }
}