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
        public MaskableGraphic m_loadDoneAlert;
        public MaskableGraphic m_holdAlert;

        void Start()
        {
            if (OnStart != null)
                OnStart.Invoke();
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

        private IEnumerator HandlePreload(string sceneName)
        {
            yield return new WaitForSeconds(1.0f);

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loadSceneAsync.allowSceneActivation = false;

            while (loadSceneAsync.progress < 0.89f)
            {
                yield return new WaitForFixedUpdate();
            }

            if (m_loadDoneAlert)
                m_loadDoneAlert.enabled = true;

            while (!Input.GetKeyDown(m_runKey))
            {
                yield return new WaitForFixedUpdate();
            }

            m_loadDoneAlert.enabled = false;
            m_holdAlert.enabled = true;
            loadSceneAsync.allowSceneActivation = true;
        }
    }
}
