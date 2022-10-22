using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is a recipe input
 * @Author Omar Radwan
 * @Version 1.0.0
 * @Throws System.Exception
 */
public class RecipeInput
{
    private Item mainItem;
    private Item secondItem;
    private Item binding;
    private Item hardware;

    // Constructor
    // @Parms Item main : main item
    // @Parms Item second : second item
    // @Parms Item binding : binding item
    // @Parms Item hardware : hardware item
    public RecipeInput(Item main, Item second, Item binding, Item hardware) {
        this.mainItem = main;
        this.secondItem = second;
        this.binding = binding;
        this.hardware = hardware;
    }

    // Constructor
    // @Parms Item[] items : Array of items to use for the recipe input
    // @Throws System.Exception : Will throw an error if the array is less than 4
    public RecipeInput(Item[] items) {
        if (items.Length < 4) {
            throw new System.Exception("Error: The lenght of the item array is not atleast 4");
        }
        this.mainItem = items[0];
        this.secondItem = items[1];
        this.binding = items[2];
        this.hardware = items[3];
    }

    // Gets the main item
    public Item getMain() {
        return this.mainItem;
    }

    // Gets the second item
    public Item getSecond() {
        return this.secondItem;
    }

    // Gets the binding Item
    public Item getBinding() {
        return this.binding;
    }

    // Gets the Hareware Item
    public Item getHareware() {
        return this.hardware;
    }

    // Checks to see if two recipe inputs are equal
    // @Parms RecipeInput obj : The Recipe input to compare to
    // @Return bool : True if they are equal, false if otherwise
    public bool checkEqual(RecipeInput obj) {
        if (this.getMain() != obj.getMain()) {
            return false;
        }
        if (this.getSecond() != obj.getSecond()) {
            return false;
        }
        if (this.getBinding() != obj.getBinding()) {
            return false;
        }
        if (this.getHareware() != obj.getHareware()) {
            return false;
        }
        return true;
    }
}
