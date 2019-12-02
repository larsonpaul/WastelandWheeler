using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private string levelToLoad;


    public void FadeToPlatformer()
    {
        animator.SetTrigger("T2PSceneChange");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(1);
    }
}
