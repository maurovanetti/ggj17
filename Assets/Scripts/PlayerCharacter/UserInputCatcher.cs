using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputCatcher : MonoBehaviour {

    ControlledTopDownCharacter topDownCharacrer;

    void Start ()
    {
        topDownCharacrer = GetComponent<ControlledTopDownCharacter>();
    }

    void Update ()
    {
        topDownCharacrer.SetAxis(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
