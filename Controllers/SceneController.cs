using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoSingletonPersistent<SceneController>
    {
        [SerializeField] private float waitTime = 3f;

        private void Start()
        {
            LoadScene("Game");
        }

        private void LoadScene(string sceneName)
        {
            StartCoroutine(WaitForIntro(sceneName));
        }

        private IEnumerator WaitForIntro(string sceneName)
        {
            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
