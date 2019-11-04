using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private string levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("l"))
        {
            FadeToPlatformer("Upgrade");
        }
    }

    public void FadeToPlatformer(string levelName)
    {
        animator.SetTrigger("T2PSceneChange");
        levelToLoad = levelName;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
