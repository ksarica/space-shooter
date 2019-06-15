using KS.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.instance.CoreGameEvents += OnGameEventFired;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameEventFired(string name)
    {
        if (name == "moved")
        {
        }
    }
}
