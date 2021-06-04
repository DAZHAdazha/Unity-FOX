using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public void playGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame(){
        Application.Quit();
    }
    public void UIEnable(){
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void pauseGame(){
        pauseMenu.SetActive(true);
        //游戏停止
        Time.timeScale = 0f;
    }
    public void resumeGame(){
        pauseMenu.SetActive(false);
        //游戏恢复
        //设置小数可以放慢加快速度
        Time.timeScale = 1f;
    }

    public void setVolume(float value){
        audioMixer.SetFloat("MainVolume",value);
    }
}
