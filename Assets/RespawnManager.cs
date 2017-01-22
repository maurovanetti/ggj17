using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour {

    public float exitTime;
    public string nextSceneName;
    public KeyCode keyToQuit;

    // Use this for initialization
    void Start () {
        StartCoroutine(DieSceneExit());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyToQuit))
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
