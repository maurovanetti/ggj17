using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDetector : AbstractEnemyAi {

    public float detectionRadius;
    private bool detected;

	// Use this for initialization
	void Start () {
        detected = false;
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        if (!detected)
        {
            if (CharacterDistance() <= detectionRadius)
            {
                GetComponent<GoToTarget>().Target = character.transform;
                detected = true;
            }

        }
        else
        {
            CheckKill();
        } 

	}
}
