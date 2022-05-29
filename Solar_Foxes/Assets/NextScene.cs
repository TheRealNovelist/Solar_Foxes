using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine("NextScenePlay");
    }

    // Start is called before the first frame update
    IEnumerator NextScenePlay()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Main Menu");
        
    }
}
