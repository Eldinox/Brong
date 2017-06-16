using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnleitungStart : MonoBehaviour
{
  	// Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update(){}

    public void onClick()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Test");
    }

    public void BackToMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Menu");
    }
}