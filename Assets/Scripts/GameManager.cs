using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject panelGameOver;
    public Image imageWin;
    public Image imageLoss;
    // Start is called before the first frame update
    void Start()
    {
        panelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool win)
    {
        panelGameOver.SetActive(true);
        if (win)
        {
            imageWin.gameObject.SetActive(true);
            imageLoss.gameObject.SetActive(false);
        }
        else
        {
            imageWin.gameObject.SetActive(false);
            imageLoss.gameObject.SetActive(true);
        }
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
