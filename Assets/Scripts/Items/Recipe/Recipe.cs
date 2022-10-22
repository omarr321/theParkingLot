using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is a recipe
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class Recipe
{
    private RecipeInput recInput;
    private Item output;

    // constructor
    // @Parms RecipeInput recIn : The recipe input
    // @Parms Item output : The item that is crafted from this recipe
    public Recipe(RecipeInput recIn, Item output) {
        this.recInput = recIn;
        this.output = output;
    }

    // Get the item crafted from this recipe
    // @Return Item : The Item that is crafted
    public Item getItemOut() {
        return this.output;
    }

    // Compares the Recipe Input to this recipe to see if they match
    // @Parms RecipeInput recIn : The RecipeInput to compare
    // @Return bool : true if they are the same, false if otherwise
    public bool compareInput(RecipeInput recIn) {
        return this.recInput.checkEqual(recIn);
    }
}
