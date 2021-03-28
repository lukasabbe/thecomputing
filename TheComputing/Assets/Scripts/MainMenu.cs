using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button[] b = new Button[4];
    public GameObject QuitMenu;
    void Start()
    {
        QuitMenu.SetActive(false);
        b[0].onClick.AddListener(OnPlay);
        b[1].onClick.AddListener(OnSetings);
        b[2].onClick.AddListener(OnQuit);
        b[3].onClick.AddListener(OnQuitYes);
        b[4].onClick.AddListener(OnQuitNo);
    }
    void OnPlay()
    {
        SceneManager.LoadScene(0);
    }
    void OnQuit()
    {
        QuitMenu.SetActive(true);
    }
    void OnSetings()
    {
        
    }
    void OnQuitYes()
    {
        Application.Quit();
    }
    void OnQuitNo()
    {
        QuitMenu.SetActive(false);
    }
}
