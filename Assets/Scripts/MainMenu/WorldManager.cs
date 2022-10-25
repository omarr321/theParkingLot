using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/* This class has a instance of the loadSettings and is keep thought sceneLoading
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    private int seed;
    private string worldName;
    private string folderPath;

    private Dictionary<string, object> playerVal = new Dictionary<string, object>();
    private Dictionary<string, Item> playerInv = new Dictionary<string, Item>();

    private bool init = false;

    // Set this gameObject to not be destory on scene load
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void setSeed(int seed)
    {
        this.seed = seed;
    }

    public void setWorldName(string name)
    {
        this.worldName = name;
        this.folderPath = Path.Combine(Application.persistentDataPath, "saves", this.worldName);
    }

    public void setPlayerVal(Dictionary<string, object> playerVal)
    {
        this.playerVal = playerVal;
    }

    public void setPlayerInv(Dictionary<string, Item> playerInv)
    {
        this.playerInv = playerInv;
    }

    public void savePlayerValToFile()
    {
        StreamWriter writer = new StreamWriter(Path.Combine(this.folderPath, "world.dat"));
        writer.WriteLine("seed:" + this.seed);
        foreach(KeyValuePair<string, object> data in this.playerVal){
                writer.WriteLine(data.Key + ":" + data.Value.ToString());
        }
        foreach(KeyValuePair<string, Item> data in this.playerInv){
            writer.WriteLine(data.Key + ":" + data.Value.getSaveName());
        }
        writer.Close();
    }

    public void loadPlayerValFromFile()
    {
        if (!init) {
            throw new Exception("Error: The world has not been initlized yet");
        }
        StreamReader reader = new StreamReader(Path.Combine(this.folderPath, "world.dat"));
        this.playerInv.Clear();
        this.playerVal.Clear();
        while(!reader.EndOfStream) {
            string[] data = reader.ReadLine().Split(":");
            switch(data[0]) {
                case "seed":
                    this.seed = int.Parse(data[1].ToString());
                    break;
                case "playerRotX":
                case "playerRotY":
                case "playerPosX":
                case "playerPosY":
                case "playerHealth":
                case "playerHunger":
                case "playerThirst":
                    this.playerVal.Add(data[0], float.Parse(data[1].ToString()));
                    break;
                case "inv0":
                case "inv1":
                case "inv2":
                case "inv3":
                case "inv4":
                case "inv5":
                case "inv6":
                case "inv7":
                case "inv8":
                case "inv9":
                case "inv10":
                case "inv11":
                case "inv12":
                case "inv13":
                case "inv14":
                case "inv15":
                case "inv16":
                case "inv17":
                case "inv18":
                case "inv19":
                    if (data[1] == "none")
                    {
                        this.playerInv.Add(data[0], null);
                    } else {
                        this.playerInv.Add(data[0], ItemDB.getItem(data[1]));
                    }
                    break;
            }
        }
        Debug.Log("Val loaded from file!");
    }

    public int getSeed()
    {
        return this.seed;
    }

    public string getWorldName()
    {
        return this.worldName;
    }

    public string getWorldPath()
    {
        return this.folderPath;
    }

    public Dictionary<string, object> getPlayerVal()
    {
        return this.playerVal;
    }

    public Dictionary<string, Item> getPlayerInv()
    {
        return this.playerInv;
    }

    public void initWorld()
    {
        if (System.IO.Directory.Exists(this.folderPath)) {
            init = true;
        } else {
            System.IO.Directory.CreateDirectory(this.folderPath);
            FileStream worldBasic= File.Create(Path.Combine(this.folderPath, "world.dat"));
            worldBasic.Close();
        
            StreamWriter writer = new StreamWriter(Path.Combine(this.folderPath, "world.dat"));
            writer.WriteLine("seed:" + this.seed);
            writer.WriteLine("playerRotX:0.00");
            writer.WriteLine("playerRotY:0.00");
            writer.WriteLine("playerPosX:0.00");
            writer.WriteLine("playerPosY:0.00");
            writer.WriteLine("playerHealth:100.00");
            writer.WriteLine("playerHunger:100.00");
            writer.WriteLine("playerThirst:100.00");
            for(int i = 0; i < 20; i++) {
                writer.WriteLine("inv" + i + ":none");
            }
            writer.WriteLine("timeStamp:" + System.DateTime.Now);
            writer.Close();
            init = true;
        }
    }
}
