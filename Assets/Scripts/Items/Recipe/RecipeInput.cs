using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* This class is a recipe input
 * @Author Omar Radwan
 * @Version 1.0.0
 * @Throws System.Exception
 */
public class RecipeInput : IComparable<RecipeInput>
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

    // Compares RecipeInput together for sorting
    // @Parms RecipeInput obj : The RecipeInput
    // @Return int : Returns less than 0 if the recipe comes before this recipe, greated than 0 if it comes after, 0 if equal
    public int CompareTo(RecipeInput obj)
    {
        if (this.getBinding() == null || obj.getBinding() == null || this.getHareware() == null || obj.getHareware() == null) {
            if (this.getMain().CompareTo(obj.getMain()) != 0) {
                return this.getMain().CompareTo(obj.getMain());
            } else if (this.getSecond().CompareTo(obj.getSecond()) != 0) {
                return this.getSecond().CompareTo(obj.getSecond());
            }

            if (!(this.getBinding() == null && obj.getBinding() == null)) {
                if (this.getBinding() == null) {
                    return 1;
                } else {
                    return -1;
                }
            } else if (!(this.getHareware() == null && obj.getHareware() == null)) {
                if (this.getHareware() == null) {
                    return 1;
                } else {
                    return -1;
                }
            } else {
                return 0;
            }
        } else {
            if (this.getMain().CompareTo(obj.getMain()) != 0) {
                return this.getMain().CompareTo(obj.getMain());
            } else if (this.getSecond().CompareTo(obj.getSecond()) != 0) {
                return this.getSecond().CompareTo(obj.getSecond());
            } else if (this.getBinding().CompareTo(obj.getBinding()) != 0) {
                return (this.getBinding().CompareTo(obj.getBinding()));
            } else if (this.getHareware().CompareTo(obj.getHareware()) != 0) {
                return this.getHareware().CompareTo(obj.getHareware());
            } else {
                return 0;
            }
        }
    }
}
