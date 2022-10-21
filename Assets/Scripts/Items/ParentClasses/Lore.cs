using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is a subclass of Item for all items that are Lore
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class Lore : Item
{
    private string lore;
    
    // Constructor
    // @Parms string Name : The name of the item
    // @Parms string Desc : The desc of the item
    // @Parms string Lore : The Lore of the item, it can be opened into a window
    public Lore(string Name, string Desc, string Lore) : base(Name, Desc)
    {
        this.lore = Lore;
    }

    // Constructor
    // @Parms string Name : The name of the item
    // @Parms string Desc : The desc of the item
    // @Parms string Lore : The Lore of the item seprated into pages, it can be opened into a window
    public Lore(string Name, string Desc, string[] Lore) : base(Name, Desc)
    {
        for (int i = 0; i < Lore.Length; i++) {
            if (i == Lore.Length-1) {
                this.lore = this.lore + Lore[i];
                return;
            }
            this.lore = this.lore + Lore[i] + "<page>";
        }
    }

    // Returns the lore of the object
    public string getLore()
    {
        return this.lore;
    }

    // Overrides ToString to show relvent information on one line
    // @Return String: the information about the item
    public override string ToString()
    {
        string results = "Name: " + this.getName() + "\nLore: " + this.lore;
        return results;
    }
}
