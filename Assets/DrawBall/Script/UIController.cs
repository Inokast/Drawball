using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Level Connections")]
    [SerializeField] public Text scoreText;
    [SerializeField] GameObject keyOutlineIcon;
    [SerializeField] GameObject keyIcon;
    private LevelTransitionScreen transition;

    [Header("Menu Connections")]
    [SerializeField] GameObject[] instructionPanels;
    private int currentPage = 0;

    private SoundFXController sfx;
    private BGMController bgm;

    // Start is called before the first frame update
    void Start()
    {
        sfx = FindObjectOfType<SoundFXController>();
        bgm = FindObjectOfType<BGMController>();
        transition = FindObjectOfType<LevelTransitionScreen>();

        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            HideKey();
            bgm.StopMusic();
            bgm.PlayLevelMusic();
        }

        else if (SceneManager.GetActiveScene().name == "EndScreen")
        {
            bgm.StopMusic();
            bgm.PlayVictoryMusic();
        }

        else if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "Instructions")
        {
            bgm.StopMusic();
            bgm.PlayMenuMusic();
        }
    }

    public void UpdateScore(int currentScore) // Updates the star counter in the UI during a level
    {
        scoreText.text = "X " + currentScore.ToString();
    }

    public void ColorScore() // Changes star counter color to yellow
    {
        scoreText.color = Color.yellow;
    }

    public void ShowKey() // Shows key icon in UI when player is holding a key
    {
        keyIcon.SetActive(true);
        keyOutlineIcon.SetActive(false);
    }

    public void HideKey() // Hides key icon when player is not holding a key
    {
        keyOutlineIcon.SetActive(true);
        keyIcon.SetActive(false);
    }

    public void NextLevel() 
    {
        StartCoroutine(LoadNextLevel());
    }

    public void Restart() 
    {
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel() 
    {
        transition.StartTransition();

        yield return new WaitForSeconds(transition.transitionTime);

        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentScene);
    }

    IEnumerator LoadNextLevel()
    {
        transition.StartTransition();

        yield return new WaitForSeconds(transition.transitionTime);

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Menu")
        {
            SceneManager.LoadScene("Level1");
        }

        if (currentScene == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }

        if (currentScene == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }

        if (currentScene == "Level3")
        {
            SceneManager.LoadScene("EndScreen");
        }

    }

    public void LoadInstructions()
    {
        sfx.PlayMenuClick();
        SceneManager.LoadScene("Instructions");
    }

    public void LoadCredits()
    {
        sfx.PlayMenuClick();
        SceneManager.LoadScene("Credits");
    }

    public void LoadMenu()
    {
        sfx.PlayMenuClick();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        sfx.PlayMenuClick();
        Application.Quit();
    }

    public void NextPage() // On instructions menu
    {
        sfx.PlayMenuClick();
        int lastPage = currentPage;
        currentPage++;

        if (currentPage >= instructionPanels.Length)
        {
            currentPage = 0;
        }

        instructionPanels[currentPage].SetActive(true);
        instructionPanels[lastPage].SetActive(false);
    }
}
