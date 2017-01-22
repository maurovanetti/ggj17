using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Utility
{
    public class SceneController : MonoBehaviour
    {
        public UnityEvent OnStart;
         [Header("Preload Scene Params")]
        public KeyCode m_runKey;
        public KeyCode m_escKey;
        public MaskableGraphic m_loadDoneAlert;
        public MaskableGraphic m_holdAlert;
        public float DieSceneExitTime;
        bool start = false;

        AudioStart audio;
        Canvas canvas;

        void Start()
        {
            Scene ActiveScene = SceneManager.GetActiveScene();
            if (ActiveScene.name == "StartScene")
            {
                canvas = GetComponentInChildren<Canvas>();
                PreloadScene("ScenaViola");
            }
            audio = GetComponent<AudioStart>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(m_runKey))
                StartGame();
            if (Input.GetKeyDown(m_escKey))
                QuitGame();
        }

        public void OpenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void PreloadScene(string sceneName)
        {
            StartCoroutine(HandlePreload(sceneName));
        }        

        public void StartGame()
        {
            start = true;
            audio.AudioOnStart();
        }

        private IEnumerator HandlePreload(string sceneName)
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loadSceneAsync.allowSceneActivation = false;
            /*
            while (loadSceneAsync.progress < 0.89f)
            {
                yield return new WaitForFixedUpdate();
            }
            */
            while (!start)
            {
                yield return new WaitForFixedUpdate();
            }

            canvas.gameObject.SetActive(false);
            loadSceneAsync.allowSceneActivation = true;
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }
}
