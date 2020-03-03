using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField]
    private Animator transitionAnim;

    private void Start()
    {
       StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

    //// Progress bar???
    //[SerializeField]
    //private Image _progressBar;
    //[SerializeField]
    //private Text m_text;

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    // Start async operation
    //    StartCoroutine(LoadAsyncOperation());
    //}

    //private IEnumerator LoadAsyncOperation()
    //{
    //    yield return null;

    //    // Begin load scene
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
    //    // Don't let the Scene activate until you allow it to
    //    operation.allowSceneActivation = false;
    //    Debug.Log("Pro : " + operation.progress);
    //    // When the load is still in progress, output the progress bar
    //    while(!operation.isDone)
    //    {
    //        _progressBar.fillAmount = operation.progress;

    //        if(operation.progress >= 0.9f)
    //        {
    //            m_text.text = "Press the space bar to continue";
    //            // anything to happen when loading have reach 100% like Press space bar to continue or something?
    //            if (Input.GetKeyDown(KeyCode.Space))
    //                operation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
}