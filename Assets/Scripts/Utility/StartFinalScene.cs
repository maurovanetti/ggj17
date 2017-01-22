using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinalScene : MonoBehaviour {
    
    TextManager text;
    bool done = true;

    void Start()
    {
        text = GetComponent<TextManager>();
    }
    void Update()
    {
        if(done)
        {
            text.ShowText(true);
            done = false;
        }
    }
}
