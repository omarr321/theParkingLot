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
}
