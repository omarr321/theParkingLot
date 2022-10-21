using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is the base class for items
 * @Author Omar Radwan
 * @Version 1.0.1
 */
public class Item
{
    private int ID;
    private string Name;
    private string Desc;
    private static int currID = 0;
    //public playerInvManager playerInv;

    // Constructor
    // @Parms string Name : The name of the item
    // @Parms string Desc : The desc of the item
    public Item(string Name, string Desc){
            this.ID = currID;
            currID++;
            this.Name = Name;
            this.Desc = Desc;
    }

    // Gets name of item
    // @Return String: the name of the item
    public string getName() {
        return this.Name;
    }

    // Gets desc of item
    // @Return String: the desc of the item
    public string getDesc() {
        return this.Desc;
    }

    // Gets ID of item
    // @Return Int : the ID of the item
    public int getID() {
        return this.ID;
    }
    
    // Overrides ToString to show relvent information on one line
    // @Return String: the information about the item
    override public string ToString(){
        return "Name: " + this.Name + "\nID: " + this.ID;
    }
}