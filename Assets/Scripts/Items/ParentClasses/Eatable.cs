using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is a subclass of Item for all items that are Eatable
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class Eatable : Item
{
    private float saturation;
    private float hydration;
    private float healthReg;
    private PlayerManager playerMan;
    
    // Constructor
    // @Parms string Name : The name of the item
    // @Parms string Desc : The desc of the item
    // @Parms PlayerManager playerMan : the player manager
    // @Parms float saturation : The saturation reg of the item
    // @Parms float hydration : The hydration reg of the item
    // @Parms float healthReg: The health reg of the item
    public Eatable(string saveName, string Name, string Desc, PlayerManager playerMan, float saturation, float hydration, float healthReg) : base(saveName, Name, Desc){
        this.saturation = saturation;
        this.hydration = hydration;
        this.healthReg = healthReg;
        this.playerMan = playerMan;
    }

    // Consumes the item and set the player values accordingly
    public void consume() {
        playerMan.addHealth(this.healthReg);
        playerMan.addHydration(this.hydration);
        playerMan.addSaturation(saturation);
    }

    // Returns item saturation
    // @Return float : The item saturation
    public float getSaturation() {
        return this.saturation;
    }

    // Returns item hydration
    // @Return float : The item hydration
    public float getHydration() {
        return this.hydration;
    }

    // Returns item health reg
    // @Return float : The item health reg
    public float getHealthReg() {
        return this.healthReg;
    }

    // Overrides ToString to show relvent information on one line
    // @Return String: the information about the item
    public override string ToString()
    {
        string results = "Name: " + this.getName() + "\nSaturation: " + this.getSaturation() + ", hydration: " + this.getHydration() + ", Health Reg: " + this.getHealthReg();
        return results;
    }
}
