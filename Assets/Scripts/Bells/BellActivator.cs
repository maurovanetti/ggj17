using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellActivator : MonoBehaviour {

    public float m_activationDistance = 0.2f;

    GameObject player;
    Transform tr;

    NotMyTempo notMyTempoScript;
    Bell bellScript;

    bool val;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        notMyTempoScript = GetComponent<NotMyTempo>();
        bellScript = GetComponent<Bell>();
        tr = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        val = Vector3.Distance(player.transform.position, tr.position) <= m_activationDistance;
        SetActiveStateToScripts(val);
	}

    private void SetActiveStateToScripts(bool val)
    {
        notMyTempoScript.enabled = val;
        bellScript.enabled = val;
    }
}
