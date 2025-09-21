using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderReset : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene();
        }
    }
    public void LoadScene()
    {
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene);
    }
}
