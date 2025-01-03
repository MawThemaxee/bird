using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UILinker _uiLinker;
    [SerializeField] private Button _replayButton;
    public Button ReplayButton {get { return _replayButton; }}
    [SerializeField] private GameObject _gameOverUI;
    public GameObject GameOverUI {get { return _gameOverUI; }}
    [SerializeField] private TextMeshProUGUI _pointUI;
    public TextMeshProUGUI PointUI {get { return _pointUI; } set { _pointUI = value; }}

    [SerializeField] private Button _leaderboardButton;
    public Button LeaderboardButton {get { return _leaderboardButton; }}
    [SerializeField] private Button _backToMainMenuButton;
    public Button BackToMainMenuButton {get { return _backToMainMenuButton; }}
    private void Awake() {
        _uiLinker.SetUIManager(this);
        _gameOverUI.SetActive(false);
    }


}
