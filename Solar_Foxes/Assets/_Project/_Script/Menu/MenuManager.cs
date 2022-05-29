using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject Label;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject buttonBack;
    public GameObject buttonBack2;
    public GameObject info;
    public GameObject credit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Choosing Level");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Instruction()
    {
        Label.SetActive(false);
        Item1.SetActive(false);
        Item2.SetActive(false);
        buttonBack.SetActive(true);
        info.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Label.SetActive(true);
        Item1.SetActive(true);
        Item2.SetActive(true);
        buttonBack.SetActive(false);
        info.SetActive(false);
    }

    public void Credit()
    {
        Label.SetActive(false);
        Item1.SetActive(false);
        Item2.SetActive(false);
        buttonBack2.SetActive(true);
        credit.SetActive(true);
    }

    public void BackToMainMenu2()
    {
        Label.SetActive(true);
        Item1.SetActive(true);
        Item2.SetActive(true);
        buttonBack2.SetActive(false);
        credit.SetActive(false);
    }
}
