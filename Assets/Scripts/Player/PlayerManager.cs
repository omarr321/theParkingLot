using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controll aspects of the player like health, hydro, hunger
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class PlayerManager : MonoBehaviour
{
    private List<Item> inv = new List<Item>();

    private float Health = 100.0f;
    public TMPro.TextMeshProUGUI HealthTag;
    
    private float Saturation = 100.0f;
    public TMPro.TextMeshProUGUI SaturationTag;

    private float Hydration = 100.0f;
    public TMPro.TextMeshProUGUI HydrationTag;
    public KeyCode invOpen = KeyCode.E;
    public GameObject invObject;
    private bool invActive = false;
    private bool updated = false;
    public InvInteractable invInter;
    public TMPro.TextMeshProUGUI slot0;
    public PlayerControlLock playerLock;

    private bool healthEnabled = true;
    private bool satEnabled = true;
    private bool hydroEnabled = true;
    
    // Sets default values
    void Start()
    {
        invActive = false;
        //Repeats the updateLoop method every 2.5 seconds
        InvokeRepeating("updateLoop", 0, 2.5f);
    }

    // check to see if the inv is open and sets values accordingly
    void Update() {
        if (Input.GetKeyDown(invOpen)) {
            if(playerLock.checkOwner(this)) {
                invActive = !invActive;
                updated = false;
            }
        }
        if (!updated) {
            invObject.SetActive(invActive);
            if(invActive) {
                playerLock.lockPlayer(this);
                playerLock.disableCam(this);
                playerLock.disableMovement(this);
            } else {
                invInter.chanceCurrent(slot0);
                playerLock.enableCam(this);
                playerLock.enableMovement(this);
                playerLock.unlockPlayer(this);
            }
            updated = true;
        }
    }

    // Loses saturation and hydration everytime this method is called
    // Loses Health if saturation or hydration is zero
    private void updateLoop() {
        if (this.satEnabled) {
            this.subSaturation(0.20f);
        }
        if (this.hydroEnabled) {
            this.subHydration(0.3f);
        }
        if (this.healthEnabled) {
            if (this.Saturation == 0.0f) {
                this.subHealth(1.0f);
            }
            if (this.Hydration == 0) {
                this.subHealth(2.0f);
            }
        }
        this.updateDisplay();
    }

    // Adds health to the player
    // @Parms float Health : The amount of health to add
    public void addHealth(float Health) {
        this.Health = this.Health + Health;
        this.checkHealth();
        this.updateDisplay();
    }
    // Subs health from the player
    // @Parms float Health : The amount of health to sub
    public void subHealth(float Health) {
        this.Health = this.Health - Health;
        this.checkHealth();
        this.updateDisplay();
    }

    // Sets health to value given
    // @Parms float Health : The health value
    public void setHealth(float Health) {
        this.Health = Health;
        this.checkHealth();
        this.updateDisplay();
    }

    // Makes sure that the health is beween 100 and 0 inclusive
    public void checkHealth() {
        if (this.Health > 100.0f) {
            this.Health = 100.0f;
        } else if (this.Health < 0.0f) {
            this.Health = 0.0f;
        }
    }

    // Controls if the health can decrese by natural means
    // @Parms bool val : True if health is enabled, false if otherwise
    public void setEnabledHealth(bool val) {
        this.healthEnabled = val;
    }

    // Adds saturation to the player
    // @Parms float Saturation : The amount of saturation to add
    public void addSaturation(float Saturation) {
        this.Saturation = this.Saturation + Saturation;
        this.checkSat();
        this.updateDisplay();
    }

    // Subs saturation from the player
    // @Parms float Saturation : The amount of saturation to sub
    public void subSaturation(float Saturation) {
        this.Saturation = this.Saturation - Saturation;
        this.checkSat();
        this.updateDisplay();
    }

    // Sets saturation to value given
    // @Parms float Saturation : The saturation value
    public void setSaturation(float Saturation) {
        this.Saturation = Saturation;
        this.checkSat();
        this.updateDisplay();
    }

    // Makes sure that the saturation is beween 100 and 0 inclusive
    public void checkSat() {
        if (this.Saturation > 100.0f) {
            this.Saturation = 100.0f;
        } else if (this.Saturation < 0.0f) {
            this.Saturation = 0.0f;
        }
    }

    // Controls if the saturation can decrese by natural means
    // @Parms bool val : True if saturation is enabled, false if otherwise
    public void setEnabledSat(bool val) {
        this.satEnabled = val;
    }

    // Adds hydration to the player
    // @Parms float Hydration : The amount of hydration to add
    public void addHydration(float Hydration) {
        this.Hydration = this.Hydration + Hydration;
        this.checkHydro();
        this.updateDisplay();
    }

    // Subs hydration from the player
    // @Parms float Hydration : The amount of hydration to sub
    public void subHydration(float Hydration) {
        this.Hydration = this.Hydration - Hydration;
        this.checkHydro();
        this.updateDisplay();
    }

    // Sets hydration to value given
    // @Parms float Hydration : The hydration value
    public void setHydration(float Hydration) {
        this.Hydration = Hydration;
        this.checkHydro();
        this.updateDisplay();
    }

    // Makes sure that the hydration is beween 100 and 0 inclusive
    public void checkHydro()
    {
        if (this.Hydration > 100.0f) {
            this.Hydration = 100.0f;
        } else if (this.Hydration < 0.0f) {
            this.Hydration = 0.0f;
        }
    }

    // Controls if the hydration can decrese by natural means
    // @Parms bool val : True if hydration is enabled, false if otherwise
    public void setEnabledHydro(bool val) {
        this.hydroEnabled = val;
    }

    // Updates the GUI to the current Health, Hunger, and Thirst values
    private void updateDisplay() {
        
        this.HealthTag.text = "Health: " + System.Math.Round(this.Health, 2);
        this.SaturationTag.text = "Hunger: " + System.Math.Round(this.Saturation, 2);
        this.HydrationTag.text = "Thirst: " + System.Math.Round(this.Hydration, 2);
    }
}