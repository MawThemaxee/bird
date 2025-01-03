using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    [SerializeField] private UILinker _uiLinker;
    public UILinker UILinker { get => _uiLinker; set => _uiLinker = value; }
    public static Manager Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadSceneAsync((int)SceneName.MainMenu, LoadSceneMode.Additive);
            Version();
        } else {
            Destroy(gameObject);
        }
    }
    public void Version() {
        if (GetFile("Version")) {
            string path = Application.persistentDataPath + "/Version.json";
            string json = System.IO.File.ReadAllText(path);
            var data = JsonUtility.FromJson<Version>(json);
        } else {
            string path = Application.persistentDataPath + "/Version.json";
            string json = "{\"version\":\"1.0\"}";
            System.IO.File.WriteAllText(path, json);
        }
    }
    private int _score;
    public string playerName = "";
    public int Score {get { return _score; } set {_score = value;}}
    public void ResetScore() {
        _score = 0;
        _uiLinker.GetUIManager().PointUI.text = _score.ToString();
    }
    public void ResetGame() {
        Time.timeScale = 1;
        ResetScore();
        SceneManager.UnloadSceneAsync((int)SceneName.Game).completed += (AsyncOperation obj) => {
            SceneManager.LoadSceneAsync((int)SceneName.Game, LoadSceneMode.Additive);
        };        
    }
    public void AddScore(int score) {
        _score += score;
        _uiLinker.GetUIManager().PointUI.text = _score.ToString();    
    }
    public void GameOver() {
        SavePoint();
        Time.timeScale = 0;
        _uiLinker.GetUIManager().GameOverUI.SetActive(true);
        _uiLinker.GetUIManager().ReplayButton.onClick.AddListener(ResetGame);
        _uiLinker.GetUIManager().LeaderboardButton.onClick.AddListener(Load_leaderboard);
        _uiLinker.GetUIManager().BackToMainMenuButton.onClick.AddListener(backToMainMenu);
    }
    public void StarGame() {
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.MainMenu).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.MainMenu);
        }
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.leaderboard).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.leaderboard);
        }

        SceneManager.LoadSceneAsync((int)SceneName.Game, LoadSceneMode.Additive);
    }

    private bool GetFile(string playerName) {
        string path = Application.persistentDataPath + "/" + playerName +".json";
        if (File.Exists(path)) {
            return true;
        } else {
            return false;
        }
    }
    public void SavePoint() {
        var playerName = Manager.Instance.playerName;
        if (GetFile(playerName)) {
            string path = Application.persistentDataPath + "/" + playerName +".json";
            string newLine = "{\"playerName\":\"" + playerName + "\",\"score\":" + _score + ",\"date\":\"" + System.DateTime.Now.ToString() + "\"}";
            System.IO.File.AppendAllText(path, "\n" + newLine);
        } else {
            string path = Application.persistentDataPath + "/" + playerName +".json";
            string newLine = "{\"playerName\":\"" + playerName + "\",\"score\":" + _score + ",\"date\":\"" + System.DateTime.Now.ToString() + "\"}";
            System.IO.File.WriteAllText(path, newLine);
        }
    }
    public void Load_leaderboard() {
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.Game).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.Game);
        }
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.MainMenu).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.MainMenu);
        }
        SceneManager.LoadSceneAsync((int)SceneName.leaderboard, LoadSceneMode.Additive);
    }
    public void backToMainMenu() {
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.leaderboard).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.leaderboard);
        }
        if (SceneManager.GetSceneByBuildIndex((int)SceneName.Game).isLoaded) {
            SceneManager.UnloadSceneAsync((int)SceneName.Game);
        }

        SceneManager.LoadSceneAsync((int)SceneName.MainMenu, LoadSceneMode.Additive);
    }
}

public enum SceneName {
    Manager = 0,
    MainMenu = 1,
    Game = 2,
    leaderboard = 3,
}
[System.Serializable]
public class SaveData {
    public string playerName;
    public int score;
    public string date = System.DateTime.Now.ToString();
}
[System.Serializable]
public class Version {
    public string version;
}