using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable 
{
    private Item[] items;
    private int currEnd;

    private bool seedSet = false;
    private int seed;

    public LootTable()
    {
        this.currEnd = 0;
        this.seed = 0;
        this.items = new Item[1000];
    }

    public LootTable(Item[] items, float[] percent) : this()
    {
        for(int i = 0; i < items.Length; i++) {
                this.addItem(items[i], percent[i]);
        }
    }

    public void setSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(this.seed);
        this.seedSet = true;
    }

    public void addItem(Item item, float chance)
    {
        int tempCount = Mathf.FloorToInt(chance * 1000);
        for (int i = 0; i < tempCount; i++) {
            if (i+currEnd >= this.items.Length) {
                throw new System.Exception("Error: Tried add items that chances do not add up to 100%");
            }
            this.items[i+currEnd] = item;
        }
        this.currEnd += tempCount;
    }

    public void addItem(Item item)
    {
        this.addItem(item, 0.10f);
    }

    public void fillRest(Item item)
    {
        int tempCount = this.items.Length - currEnd;
        this.addItem(item, tempCount/1000.0f);
    }

    public Item getItem(int index) {
        return items[index];
    }

    public Item getRandomItem()
    {
        if (!this.seedSet || this.currEnd < this.items.Length) {
            throw new System.Exception("Error: The LootTable has not been init propertlly!");
        }

        return this.items[Random.Range(0, items.Length)];
    }

    public int getRandomItemNum()
    {
        int val = Random.Range(0, 100);
        if (val < 60) {
            return 1;
        }
        if (val < 90) {
            return 2;
        }
        if (val < 95) {
            return 3;
        }
        if (val < 98) {
            return 4;
        }
        return 5;
    }

    public int getCurrEnd() {
        return this.currEnd;
    }

    public Item[] getTableList() {
        Item[] temp = new Item[currEnd];
        for(int i = 0; i < currEnd; i++) {
            temp[i] = items[i];
        }
        return temp;
    }

    public LootTable combineTables(LootTable[] lootTables) {
        int total = 0;
        foreach (LootTable table in lootTables) {
            total = total + table.getCurrEnd();
        }
        if (total > 1000) {
            throw new System.Exception("Error: The total items is greater than 1000!");
        }
        LootTable temp = new LootTable();
        foreach (LootTable table in lootTables) {
            for(int i = 0; i < table.getCurrEnd(); i++) {
                temp.addItem(table.getItem(i), 0.001f);
            }
        }
        return temp;
    }
}
