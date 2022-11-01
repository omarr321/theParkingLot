using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class controls the Crafting screen
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class CraftingController : MonoBehaviour
{
    public GameObject personalCraftingTable;
    public TMPro.TMP_Dropdown mainItem;
    public TMPro.TMP_Dropdown secondItem;
    public TMPro.TextMeshProUGUI craftedItem;
    public KeyCode openKey;
    public PlayerControlLock playerLock;
    public InvManager invMan;

    public Button craftButt;

    private Color grey = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    private Color white = new Color(1f, 1f, 1f, 1.0f);
    // Default values

    private int mainItemIndex;
    private int secondItemIndex;
    private SettingManager settingMan;
    void Start()
    {
        settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();
        updateControls();
        personalCraftingTable.SetActive(false);
        craftedItem.text = "";
        mainItemIndex = -1;
        secondItemIndex = -1;
        craftButt.enabled = false;
        craftButt.image.color = grey;
        secondItem.enabled = false;
        secondItem.image.color = grey;
        mainItem.onValueChanged.AddListener(mainItemChanged);
        secondItem.onValueChanged.AddListener(secondItemChanged);
    }

    // Check for input
    void Update()
    {
        if (Input.GetKeyDown(openKey)) {
            if(playerLock.noOwner()) {
                playerLock.lockPlayer(this);
                playerLock.disableCam(this);
                playerLock.disableMovement(this);
                personalCraftingTable.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                updateDisplay();
            }
        }
    }

    public void updateControls()
    {
        openKey = settingMan.getCrafting();
    }

    // Closes view
    public void closeView() {
        mainItem.value = 0;
        secondItem.value = 0;
        personalCraftingTable.SetActive(false);
        playerLock.enableCam(this);
        playerLock.enableMovement(this);
        playerLock.unlockPlayer(this);
        personalCraftingTable.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Updates the dropdown menu options
    private void updateDisplay()
    {
        craftedItem.text = "";
        mainItem.ClearOptions();
        mainItem.AddOptions(new List<string> {"---"});
        int i = 0;
        while(invMan.getItem(i) != null) {
            mainItem.AddOptions(new List<string> {invMan.getItem(i).getName()});
            i++;
        }

        secondItem.ClearOptions();
        secondItem.AddOptions(new List<string> {"---"});
        i = 0;
        while(invMan.getItem(i) != null) {
            secondItem.AddOptions(new List<string> {invMan.getItem(i).getName()});
            i++;
        }
    }

    // Checks for when the main item is changed
    private void mainItemChanged(int arg0)
    {
        craftedItem.text = "";
        secondItem.ClearOptions();
        secondItem.AddOptions(new List<string> {"---"});
        int i = 0;
        while(invMan.getItem(i) != null) {
            if (i != mainItem.value-1) {
                secondItem.AddOptions(new List<string> {invMan.getItem(i).getName()});
            }
            i++;
        }
        if (mainItem.value == 0) {
            secondItem.enabled = false;
            secondItem.image.color = grey;
        } else {
            secondItem.enabled = true;
            secondItem.image.color = white;
        }
        mainItemIndex = mainItem.value-1;
        secondItemChanged(0);
    }

    // Checks for when the second item is changed
    private void secondItemChanged(int arg0)
    {
        craftedItem.text = "";
        if (secondItem.value == 0) {
            craftButt.enabled = false;
            craftButt.image.color = grey;
        } else {
            craftButt.enabled = true;
            craftButt.image.color = white;
        }

        if (mainItem.value != 0) {
            if (secondItem.value >= mainItem.value) {
                secondItemIndex = secondItem.value;
            } else {
                secondItemIndex = secondItem.value-1;
            }
        }
    }

    // Will Check the crafting for recipe
    public void craftItem() {
        Item mainItem = invMan.getItem(mainItemIndex);
        Item secondItem = invMan.getItem(secondItemIndex);
        RecipeInput recIn = new RecipeInput(mainItem, secondItem, null, null);
        Item item = ItemDB.getItemFromRecipe(recIn);

        if (item == null) {
            craftedItem.text = "You did not craft anything!";
        } else {
            if (mainItemIndex > secondItemIndex) {
                invMan.removeItem(mainItemIndex);
                invMan.removeItem(secondItemIndex);
            } else {
                invMan.removeItem(secondItemIndex);
                invMan.removeItem(mainItemIndex);
            }
            invMan.addItem(item);
            updateDisplay();
            craftedItem.text = "You crafted: " + item.getName();
        }
    }
}
