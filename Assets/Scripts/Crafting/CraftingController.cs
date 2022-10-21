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
    public TMPro.TextMeshProUGUI CraftedItem;
    public KeyCode openKey;
    public PlayerControlLock playerLock;
    public InvManager invMan;

    public Button craftButt;

    private Color grey = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    private Color white = new Color(1f, 1f, 1f, 1.0f);
    // Default values
    void Start()
    {
        personalCraftingTable.SetActive(false);
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
                updateDisplay();
            }
        }
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
    }

    // Updates the dropdown menu options
    private void updateDisplay()
    {
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
        secondItemChanged(0);
    }

    // Checks for when the second item is changed
    private void secondItemChanged(int arg0)
    {
        if (secondItem.value == 0) {
            craftButt.enabled = false;
            craftButt.image.color = grey;
        } else {
            craftButt.enabled = true;
            craftButt.image.color = white;
        }
    }
}
