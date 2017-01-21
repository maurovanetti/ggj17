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
        bool start = false;

        Canvas canvas;
        

        void Start()
        {
            /*
            if (OnStart != null)
                OnStart.Invoke();
            */
            PreloadScene();

            canvas = GetComponentInChildren<Canvas>();
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

        public void PreloadScene()
        {
            string sceneName = "Test0";
            StartCoroutine(HandlePreload(sceneName));
        }

        public void StartGame()
        {
            start = true;
        }

        private IEnumerator HandlePreload(string sceneName)
        {
            //yield return new WaitForSeconds(1.0f);

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadSceneAsync.allowSceneActivation = false;

            /*
            while (loadSceneAsync.progress < 0.89f)
            {
                yield return new WaitForFixedUpdate();
            }
            
            if (m_loadDoneAlert)
                m_loadDoneAlert.enabled = true;
            */
            while ((!Input.GetKeyDown(m_runKey)) || (!start))
            {
                yield return new WaitForFixedUpdate();
            }

            canvas.gameObject.SetActive(false);
            /*
            m_loadDoneAlert.enabled = false;
            m_holdAlert.enabled = true;
            */
            loadSceneAsync.allowSceneActivation = true;
        }
    }
}
