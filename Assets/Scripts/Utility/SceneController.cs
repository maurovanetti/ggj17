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
        public KeyCode m_endKey;
        public MaskableGraphic m_loadDoneAlert;
        public MaskableGraphic m_holdAlert;
        public float DieSceneExitTime;
        bool start = false;

        ManagerAudio Audio;
        Canvas canvas;

        void Start()
        {
            /*
            if (OnStart != null)
                OnStart.Invoke();
            */
            Scene ActiveScene = SceneManager.GetActiveScene();

            if (ActiveScene.name == "StartScene")
            {
                string sceneName = "Test0";
                PreloadScene(sceneName);
                canvas = GetComponentInChildren<Canvas>();
            }
            else if (ActiveScene.name == "DieScene")
            {
                BackToStart(true);
            }
            Audio = GetComponent<ManagerAudio>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(m_runKey))
                StartGame();
            if (Input.GetKeyDown(m_escKey))
                QuitGame();
            if (Input.GetKeyDown(m_endKey))
                BackToStart(false);
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

        public void UnloadOldScene()
        {
            string sceneName = "StartScene";
        }

        public void StartGame()
        {
            start = true;
            Audio.AudioOnStart();
            UnloadOldScene();
        }

        private IEnumerator HandlePreload(string sceneName)
        {
            //yield return new WaitForSeconds(1.0f);

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadSceneAsync.allowSceneActivation = false;

            
            while (loadSceneAsync.progress < 0.89f)
            {
                yield return new WaitForFixedUpdate();
            }
            /*
            if (m_loadDoneAlert)
                m_loadDoneAlert.enabled = true;
            */
            while (!start)
            {
                yield return new WaitForFixedUpdate();
            }

            canvas.gameObject.SetActive(false);
            /*
            m_loadDoneAlert.enabled = false;
            m_holdAlert.enabled = true;
            */
            loadSceneAsync.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        void BackToStart(bool value)
        {
            string sceneName = "StartScene";
            if (value)
            {
                StartCoroutine(DieSceneExit(sceneName));
            }
            else
            {
                OpenScene(sceneName);
            }
        }

        private IEnumerator DieSceneExit(string sceneName)
        {
            yield return new WaitForSeconds(DieSceneExitTime);
            OpenScene(sceneName);
        }
    }
}
