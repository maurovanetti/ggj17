using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour {

    public float exitTime;
    public string nextSceneName;

    void Start ()
    {
        StartCoroutine(DieSceneExit());
    }
	
	void Update ()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private IEnumerator DieSceneExit()
    {
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene(nextSceneName);
    }
}
