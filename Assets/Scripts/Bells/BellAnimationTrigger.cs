using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellAnimationTrigger : MonoBehaviour {

	public void TriggerAnimation()
    {
        GetComponent<Animator>().SetTrigger("swing");
    }
}
