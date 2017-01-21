using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class AbstractEnemyAi : MonoBehaviour {
    
    public float killingRadius;

    const string characterTag = "Player";
    protected GameObject character;

    // Use this for initialization
    void Start() {
        Initialize();
    }

    protected void Initialize() {
        character = GameObject.FindGameObjectWithTag(characterTag);
	}

    protected void CheckKill()
    {
        if (CharacterDistance() <= killingRadius)
        {
            Debug.LogWarning(this.name + " killed " + character.name);
            character.GetComponent<TopDownCharacter>().OnDeath();
            GetComponent<GoToTarget>().Target = transform.position; // Freezes
        }
    }

    protected float CharacterDistance()
    {
        if (character)
        {
            Vector3 offset = character.transform.position - transform.position;
            offset.y = 0;
            return offset.magnitude;
        }
        else return float.PositiveInfinity;
        
    }
}
