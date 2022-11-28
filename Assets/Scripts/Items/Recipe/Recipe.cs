using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* This class is a recipe
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class Recipe : IComparable<Recipe>
{
    private RecipeInput recInput;
    private Item[] output;

    // constructor
    // @Parms RecipeInput recIn : The recipe input
    // @Parms Item output : The item that is crafted from this recipe
    public Recipe(RecipeInput recIn, Item output) : this(recIn, new Item[]{output}) {}

    // constructor
    // @Parms RecipeInput recIn : The recipe input
    // @Parms Item output : The item array that is crafted from this recipe
    public Recipe(RecipeInput recIn, Item[] output)
    {
        this.recInput = recIn;
        this.output = output;
    }

    // Get the item crafted from this recipe
    // @Return Item : The Item that is crafted
    public Item[] getItemOut() {
        return this.output;
    }

    // Returns the Recipe Input
    // @Return RecipeInput : The Recipe Input of this Recipe
    public RecipeInput GetRecipeInput()
    {
        return this.recInput;
    }

    // Compares Recipe together for sorting
    // @Parms Recipe obj : The Recipe
    // @Return int : Returns less than 0 if the recipe comes before this recipe, greated than 0 if it comes after, 0 if equal
    public int CompareTo(Recipe obj)
    {
        return this.GetRecipeInput().CompareTo(obj.GetRecipeInput());
    }
}
