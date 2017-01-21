using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputCatcher : MonoBehaviour {

    TopDownCharacter topDownCharacrer;

    void Start ()
    {
        topDownCharacrer = GetComponent<TopDownCharacter>();
    }

    void Update ()
    {
        topDownCharacrer.SetAxis(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
