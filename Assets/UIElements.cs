using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIElements : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Start()
    {
        UpdateLevelNumber();
    }
    void UpdateLevelNumber()
    {
        levelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    public void NextLevel()
    {
        int totalLevels = SceneManager.sceneCountInBuildSettings;
        int currSceneID = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene((currSceneID + 1) % totalLevels);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
