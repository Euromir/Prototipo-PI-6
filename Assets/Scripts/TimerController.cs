using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManager;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float m_initialTime;
    private float m_currentTime;

    [SerializeField] private TextMeshProUGUI m_currentTimeText;

    private void Awake()
    {
        m_currentTime = m_initialTime;
    }

    private void Update()
    {
        if (m_currentTime > 0)
        {
            m_currentTime -= Time.deltaTime;

            TimerConverter();
        }
        else
        {
            TimeIsOver();
        } 
    }

    private void TimeIsOver()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    private void TimerConverter()
    {
        {
        int minutes = Mathf.FloorToInt(m_currentTime / 60);
        int seconds = Mathf.FloorToInt(m_currentTime % 60);

        m_currentTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
