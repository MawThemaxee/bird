using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerName;
    public void playButton() {
        if (playerName.text != "") {
            Manager.Instance.playerName = playerName.text;
            Manager.Instance.StarGame();
        }
    }
    public void QuitButton() {
        Application.Quit();
    }
    public void leaderboardformGame_Button() {
        Manager.Instance.Load_leaderboard();
    }

}
