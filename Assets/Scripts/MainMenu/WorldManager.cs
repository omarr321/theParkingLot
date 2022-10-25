using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class has a instance of the loadSettings and is keep thought sceneLoading
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    // Set this gameObject to not be destory on scene load
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
