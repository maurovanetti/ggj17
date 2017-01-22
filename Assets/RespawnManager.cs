using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour {

    public float exitTime;
    public string nextSceneName;

    // Use this for initialization
    void Start () {
        StartCoroutine(DieSceneExit());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(nextSceneName);
    }

    void OnGUI() { 
}

    private IEnumerator DieSceneExit()
    {
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene(nextSceneName);
    }
}
