using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CarLoadManager : MonoBehaviour
{
    private string folderPath;
    // Start is called before the first frame update
    void Start()
    {
        this.folderPath = GameObject.Find("LoadSetting").GetComponent<WorldManager>().getWorldPath();
    }

    public bool checkCarSave(string carName) {
        return System.IO.File.Exists(Path.Combine(this.folderPath, carName.Split(" ")[0], carName.Split(" ")[1] + ".dat"));
    }

    public void saveCar(string carName, WorldManager worldMan, Item[] items, int currEnd) {
        //Debug.Log(Path.Combine(this.folderPath, carName.Split(" ")[0], carName.Split(" ")[1] + ".dat"));
        if(!Directory.Exists(Path.Combine(worldMan.getWorldPath(), carName.Split(" ")[0]))) {
            Directory.CreateDirectory(Path.Combine(worldMan.getWorldPath(), carName.Split(" ")[0]));
        }
        FileStream carSave = File.Create(Path.Combine(worldMan.getWorldPath(), carName.Split(" ")[0], carName.Split(" ")[1] + ".dat"));
        carSave.Close();
        
        StreamWriter writer = new StreamWriter(Path.Combine(worldMan.getWorldPath(), carName.Split(" ")[0], carName.Split(" ")[1] + ".dat"));
        for(int i = 0; i < items.Length; i++) {
            string temp;
            if (items[i] != null) {
                temp = "inv-" + i + ":" + items[i].getSaveName();
            } else {
                temp = "inv-" + i + ":none";
            }
            writer.WriteLine(temp);
        }
        writer.WriteLine("currEnd:" + currEnd);
        writer.Close();
    }

    public Dictionary<string, object> loadCar(string carName, WorldManager worldMan) {
        Dictionary<string, object> temp = new Dictionary<string, object>();
        StreamReader carData = new StreamReader(Path.Combine(worldMan.getWorldPath(), carName.Split(" ")[0], carName.Split(" ")[1] + ".dat"));
        while(!carData.EndOfStream) {
            string[] data = carData.ReadLine().Split(":");
            switch(data[0]) {
                case "inv-0":
                case "inv-1":
                case "inv-2":
                case "inv-3":
                case "inv-4":
                    if (data[1] == "none") {
                        temp.Add(data[0], null);
                    } else {
                        temp.Add(data[0], data[1]);
                    }
                    break;
                case "currEnd":
                    temp.Add(data[0], data[1]);
                    break;
            }
        }
        carData.Close();
        return temp;
    }
}
