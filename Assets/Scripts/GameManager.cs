using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager instance {
        get {
            if(managerInstance == null) managerInstance = FindObjectOfType<GameManager>();
            return managerInstance;
        }
    }

    private static GameManager managerInstance; // for singleton
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
