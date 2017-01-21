using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour {

    public bool xMagicButton;
    public float ringRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (xMagicButton)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.LogWarning("CHEAT ring");
                Ring();
            }
        }
    }

    public void Ring()
    {
        GameObject[] shadows = GameObject.FindGameObjectsWithTag("Shadow");
        foreach (GameObject shadow in shadows)
        {
            Vector3 offset = shadow.transform.position - transform.position;
            offset.y = 0f;
            float penetrationInBellRange = ringRange - offset.magnitude;
            if (penetrationInBellRange > 0)
            {
                shadow.GetComponent<CharacterDetector>().ScareAway(this.transform, penetrationInBellRange);
            }
        }

    }
}
