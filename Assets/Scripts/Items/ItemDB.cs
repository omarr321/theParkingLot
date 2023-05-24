using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* This class is the Item DB and Receipes (Not Yet Inplemented)
 * @Author Omar Radwan
 * @Version 0.5.0
 * @Throws System.Exception
 */
public static class ItemDB
{
    private static bool initDB = false;
    private static Dictionary<string, Item> itemDB = new Dictionary<string, Item>();
    private static Recipe[] recipes = new Recipe[2];
    private static Dictionary<string, LootTable> lootTableDB = new Dictionary<string, LootTable>();
    private static int currIndexRec = 0;

    // Add items to the Dictionary
    // @Parms PlayerManager playerMan : The PlayerManager to use
    public static void initDatabase(PlayerManager playerMan) {
        if (!initDB) {
            // Liquids
            Eatable water = new Eatable("waterBottle", "Water Bottle", "A plasic water bottle full of water... I think.", playerMan, 5.0f, 25.0f, 0.0f);
            addItem("waterBottle", water);
            Eatable juiceA = new Eatable("juiceBoxApple", "Apple Juice Box", "A box of apple juice.", playerMan, 2.5f, 30.0f, 0.0f);
            addItem("juiceBoxApple", juiceA);
            Eatable juiceO = new Eatable("juiceBoxOrange", "Orange Juice Box", "A box of orange juice.", playerMan, 2.5f, 30.0f, 0.0f);
            addItem("juiceBoxOrange", juiceO);
            Eatable sodaC = new Eatable("sodaCherry", "Cherry Soda", "A can of cherry soda.", playerMan, 0.0f, 35.0f, -5.0f);
            addItem("sodaCherry", sodaC);
            Eatable sodaCr = new Eatable("sodaCream", "Cream Soda", "A can of cream soda... just plain cream.", playerMan, 0.0f, 35.0f, -5.0f);
            addItem("sodaCream", sodaCr);
            Eatable sodaG = new Eatable("sodaGrape", "Grape Soda", "A can of grape soda.", playerMan, 0.0f, 35.0f, -5.0f);
            addItem("sodaGrape", sodaG);
            Eatable sodaO = new Eatable("sodaOrange", "Orange Soda", "A can of orange soda.", playerMan, 0.0f, 35.0f, -5.0f);
            addItem("sodaOrange", sodaO);
            Eatable sodaB = new Eatable("sodaBanana", "Banana Soda...", "I don't know... soda is soda.", playerMan, 0.0f, 35.0f, -5.0f);
            addItem("sodaBanana", sodaB);

            // Foods
            Eatable cerealBar = new Eatable("cerealBar", "Cereal Bar", "A cereal bar still in the wrapper. Feels like it broken into pieces thought.", playerMan, 10.0f, -2.0f, 0.0f);
            addItem("cerealBar", cerealBar);
            Eatable cocoBar = new Eatable("cocoBar", "Chocolate Bar", "A bar of chocolate... do you really need a description?", playerMan, 5.0f, 5.0f, 0.0f);
            addItem("cocoBar", cocoBar);
            Eatable candyBar = new Eatable("candyBar","Candy Bar", "A candy bar... that is all.", playerMan, 5.0f, 5.0f, 0.0f);
            addItem("candyBar", candyBar);
            Eatable beefStick = new Eatable("beefStick","Beef Stick", "A beef stick loaded with beef and salt.", playerMan, 5.0f, -5.0f, 0.0f);
            addItem("beefStick", beefStick);
            Eatable potatoChips = new Eatable("potatoChips","Potato Chips", "A bag of chips.", playerMan, 15.0f, -2.0f, 0.0f);
            addItem("potatoChips", potatoChips);
            Eatable sourCandy = new Eatable("sourCandy","Sour Candy", "Some sour candy.", playerMan, 3.0f, -0.5f, 0.0f);
            addItem("sourCandy", sourCandy);
            Eatable canC = new Eatable("canCorn","Caned Corn", "A can of sweet corn.", playerMan, 30.0f, 5.0f, 0.0f);
            addItem("canCorn", canC);
            Eatable canCa = new Eatable("canCarrots","Caned Carrots", "A can of carrots", playerMan, 30.0f, 5.0f, 0.0f);
            addItem("canCarrots", canCa);
            Eatable canB = new Eatable("canBeets","Caned Beets", "A can of beets.", playerMan, 30.0f, 5.0f, 0.0f);
            addItem("canBeets", canB);
            Eatable canBa = new Eatable("canBanana","Caned Bananas...", "Why?", playerMan, 30.0f, 5.0f, 0.0f);
            addItem("canBanana", canBa);
            Eatable canP = new Eatable("canPeach","Caned Peaches...", "Sweet and tasty", playerMan, 30.0f, 5.0f, 0.0f);
            addItem("canPeach", canP);

            // Medical
            Eatable pills = new Eatable("pills", "Pills", "A bunch of random pills. Not sure what type but what do you have to lose?", playerMan, 0.0f, 0.0f, 20.0f);
            addItem("pills", pills);
            Eatable bandage = new Eatable("bandage", "Bandage", "A Bandage to help you heal wounds.", playerMan, 0.0f, 0.0f, 5.0f);
            addItem("bandage", bandage);
            Eatable gaze = new Eatable("gaze", "Gaze", "A gaze that can heal heavy duty wounds.", playerMan, 0.0f, 0.0f, 10.0f);
            addItem("gaze", gaze);
            Eatable basicMedKit = new Eatable("medKit", "Med Kit", "A basic med kit.", playerMan, 0.0f, 0.0f, 15.0f);
            addItem("medKit", basicMedKit);
            Eatable advMedKit = new Eatable("advMedKit", "Advange Med Kit", "A more advange med kit for advange wounds.", playerMan, 0.0f, 0.0f, 25.0f);
            addItem("advMedKit", advMedKit);
            Eatable tourniquet = new Eatable("tourniquet", "Tourniquet", "This is a tourniquet, a item to restrict blood flow.", playerMan, 0.0f, 0.0f, 30.0f);
            addItem("tourniquet", tourniquet);

            // Testing the crafting system
            Item paper = new Item("paper", "Paper", "Just some paper... what more do you want?");
            addItem("paper", paper);
            Item ink = new Item("ink", "Ink", "Ink to write with... and to not drink");
            addItem("ink", ink);

            initRecipes();
            initLootTables();
        }
        initDB = true;
    }

    public static void reinitDatabase(PlayerManager playerMan)
    {
        itemDB.Clear();
        initDB = false;
        currIndexRec = 0;
        lootTableDB.Clear();
        initDatabase(playerMan);
    }

    // Add the item the dictionary
    // @Parms string name : The name to store the item by
    // @Parms Item item : Item to add
    private static void addItem(string name, Item item) {
        itemDB.Add(name, item);
    }

    // Get an item from the DB
    // @Parms string name : The name to search by
    // @Return Item : the item from the DB, null if none is found
    // @Throws System.Exception : Will throw a System.Exception if the DB has not been init
    public static Item getItem(string name) {
        if (!initDB) {
            throw new System.Exception("Error: The ItemDB has not been initlized!");
        }
        Item temp = null;
        itemDB.TryGetValue(name, out temp);
        return temp;
    }

    private static Item getItemInit(string name) {
        Item temp = null;
        itemDB.TryGetValue(name, out temp);
        return temp;
    }

    // Initlizes the recipes for the game
    private static void initRecipes() {
        RecipeInput testStress = new RecipeInput(getItemInit("cerealBar"), getItemInit("cerealBar"), null, null);
        Recipe recStress = new Recipe(testStress, getItemInit("paper"));
        addRecipe(recStress);

        RecipeInput makeNoteBook = new RecipeInput(getItemInit("paper"), getItemInit("ink"), null, null);
        Recipe noteBook = new Recipe(makeNoteBook, new Item[]{getItemInit("notebook"), getItemInit("paper")});
        addRecipe(noteBook);

        Array.Sort(recipes);
    }

    // Add a recipe to the database
    // @Parms Recipe rec : The recipe to add
    private static void addRecipe(Recipe rec) {
        //Debug.Log(currIndex);
        recipes[currIndexRec] = rec;
        currIndexRec++;
    }

    private static void initLootTables() {
        LootTable temp = new LootTable();
        temp.addItem(getItemInit("waterBottle"), .20f);
        temp.addItem(getItemInit("ink"), .20f);
        temp.addItem(getItemInit("notebook"), .20f);
        //temp.addItem(getItemInit("cerealBar"), .20f);
        Debug.Log(temp.toLog());
        LootTable temp2 = new LootTable();
        temp2.addItem(getItemInit("ink"), .20f);
        Debug.Log(temp2.toLog());
        LootTable temp3 = LootTable.combineTables(new LootTable[] {temp, temp2});
        Debug.Log(temp3.toLog());

        LootTable temp4 = new LootTable();
        temp4.addItem(getItemInit("ink"), .10f);
        temp4.addItem(getItemInit("notebook"), .10f);
        LootTable temp5 = LootTable.combineTables(new LootTable[] {temp, temp2, temp4});
        Debug.Log(temp5.toLog());
        addLootTables("test", temp);
    }

    private static void addLootTables(string name, LootTable loot) {
        lootTableDB.Add(name, loot);
    }

    public static LootTable getLootTable(string name) {
        if (!initDB) {
            throw new System.Exception("Error: The ItemDB has not been initlized!");
        }
        LootTable temp = null;
        lootTableDB.TryGetValue(name, out temp);
        return temp;
    }

    // Gets an item from a recipe
    // @Parms RecipeInput rec : The recipe to check
    // @Return Item : Returns an item if the recipe is valid, null if otherwise
    public static Item[] getItemFromRecipe(RecipeInput rec) {
        if (!initDB) {
            throw new System.Exception("Error: The ItemDB has not been initlized!");
        }
        foreach (Recipe item in recipes) {
            if (item.GetRecipeInput().CompareTo(rec) == 0) {
                return item.getItemOut();
            }
        }
        return null;
    }
}
