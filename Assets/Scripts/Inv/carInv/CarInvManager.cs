using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
    public PlayerControlLock playerLock;
    public TMPro.TextMeshProUGUI slotZ;
    public CarLoadManager carLoadMan;
    private string carNameTemp;
    private bool carChanged = false;

    public string lootTableName;

    // Init the Item Database so it can be used by any script and set default values
    void Start()
    {
        Database.reinitDatabase(this.GetComponent<PlayerManager>());
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();
        currentEnd = worldMan.getInvIndex();
        numSlots = textSlots.Length;
        items = new Item[numSlots];

        updateAllInv();
        interatePage.SetActive(false);
        carInvPage.SetActive(false);
    }

    void Update() {
        LayerMask layerMask = LayerMask.GetMask("Car");
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position + new Vector3(0f, .5f,0f), cam.transform.forward, out hit, 3.0f)) {
            if (hit.transform.gameObject.tag == "Car") {
                if (playerLock.noOwner()) {
                    interatePage.SetActive(true);
                    interatePage.GetComponent<TMPro.TextMeshProUGUI>().text = "Press \'" + settingMan.getInteract().ToString() + "\' to interact";
                    if (Input.GetKeyDown(settingMan.getInteract())) {
                        playerLock.lockPlayer(this);
                        playerLock.disableCam(this);
                        playerLock.disableMovement(this);
                        carInvPage.SetActive(true);
                        carInvPage.transform.GetChild(1).GetComponent<CarInvInteractable>().chanceCurrent(slotZ);
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        interatePage.SetActive(true);
                        carNameTemp = hit.transform.gameObject.name;
                        carChanged = false;
                        currentEnd = 0;
                        updatePage(carNameTemp);
                    }
                }
            } else {
                interatePage.SetActive(false);
            }
        } else {
            interatePage.SetActive(false);
        }
        if (playerLock.noOwner() || playerLock.checkOwner(this)) {
                if (interatePage.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
                playerLock.enableCam(this);
                playerLock.enableMovement(this);
                playerLock.unlockPlayer(this);
                interatePage.SetActive(false);
                carInvPage.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if(carChanged) {
                    carLoadMan.saveCar(carNameTemp, this.worldMan, this.items, this.currentEnd);
                    carChanged = false;
                }
            }
        }
    }

    private void updatePage(string carName) {
        if (carLoadMan.checkCarSave(carName)) {
            Dictionary<string, object> data = this.carLoadMan.loadCar(carName, this.worldMan);
            foreach(KeyValuePair<string, object> currData in data) {
                switch(currData.Key) {
                    case "inv-0":
                    case "inv-1":
                    case "inv-2":
                    case "inv-3":
                    case "inv-4":
                        if (currData.Value == null) {
                            items[int.Parse(currData.Key.Split("-")[1])] = null;
                        } else {
                            items[int.Parse(currData.Key.Split("-")[1])] = Database.getItem(currData.Value.ToString());
                        }
                        break;
                    case "currEnd":
                        this.currentEnd = int.Parse(currData.Value.ToString());
                        break;
                }
            }
        } else {
            string[] temp = carName.Split(" ");
            
            string[] tileCordString = temp[0].Split(new char[] {',', '(', ')'});
            int[] tileCord = new int[] {int.Parse(tileCordString[1]), int.Parse(tileCordString[2])};
            
            int carNum = int.Parse(temp[1].Split("-")[0]);

            int[] hashIn = new int[] {worldMan.getSeed(), tileCord[0], tileCord[1], carNum};
            var hash = Hash128.Compute(hashIn).ToString();

            LootTable loot = Database.getLootTable(this.lootTableName);
            loot.setSeed(hash.GetHashCode());

            int numItem = loot.getRandomItemNum();
            Debug.Log(numItem);
            for (int i = 0; i < items.Length; i++) {
                if (i < numItem) {
                    Debug.Log("Spawning item...");
                    items[i] = loot.getRandomItem();
                    this.currentEnd++;
                } else {
                    Debug.Log("Spawning null...");
                    items[i] = null;
                }
            }

            updateAllInv();
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
        //Debug.Log("Removing item at index " + index + "...");
        if (index > currentEnd) {
            return false;
        }

        // Shifts all slot up so the empty space is removed
        for (int x = index; x < numSlots; x++) {
            shiftSlotUp(x);
        }
        currentEnd--;
        updateAllInv();
        carChanged = true;
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
        carChanged = true;
        return true;
    }

    // Shifts a inv item up and replaces the empty slot with null
    // @Parms int index : Slot to shift up
    // @Return bool : Returns true is it was successful
    private bool shiftSlotUp(int index) {
        Debug.Log("Shifting slot " + index + " up...");
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