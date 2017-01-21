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
            GetComponent<GoToTarget>().Target = transform; // Freezes
        }
    }

    protected float CharacterDistance()
    {
        if (character)
        {
            return Vector3.Distance(transform.position, character.transform.position);
        }
        else return float.PositiveInfinity;
        
    }
}
