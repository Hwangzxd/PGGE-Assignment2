using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource soundPlayer;
    public AudioClip hover;
    public AudioClip pressed;
    public AudioClip start;

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
        soundPlayer.PlayOneShot(pressed);
        StartCoroutine(LoadSinglePlayerScene());
        //SceneManager.LoadScene("SinglePlayer");
    }

    public void OnClickMultiPlayer()
    {
        //Debug.Log("Loading multiplayer game");
        soundPlayer.PlayOneShot(pressed);
        StartCoroutine(LoadMultiPlayerScene());
        //SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void OnClickBack()
    {
        //Debug.Log("Loading menu");
        soundPlayer.PlayOneShot(pressed);
        StartCoroutine(LoadMenuScene());
        //SceneManager.LoadScene("Menu");
    }

    public void OnClickJoin()
    {
        soundPlayer.PlayOneShot(start);
    }

    public void OnHover()
    {
        soundPlayer.PlayOneShot(hover);
    }

    private IEnumerator LoadSinglePlayerScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SinglePlayer");
    }

    private IEnumerator LoadMultiPlayerScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Multiplayer_Launcher");
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
