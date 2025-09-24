using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public static ReturnToMenu instance;

    [SerializeField] private string menuSceneName = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        bool minusPressed = Input.GetKey(KeyCode.KeypadMinus);

        bool multiplyPressed = Input.GetKey(KeyCode.KeypadMultiply);

        if (minusPressed && multiplyPressed)
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
