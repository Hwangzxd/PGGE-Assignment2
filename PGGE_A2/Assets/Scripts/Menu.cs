using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSinglePlayer()
    {
        //Debug.Log("Loading singleplayer game");
        soundPlayer.Play();
        SceneManager.LoadScene("SinglePlayer");
    }

    public void OnClickMultiPlayer()
    {
        //Debug.Log("Loading multiplayer game");
        soundPlayer.Play();
        SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void OnClickBack()
    {
        //Debug.Log("Loading menu");
        SceneManager.LoadScene("Menu");
    }

}
