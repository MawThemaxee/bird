using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject slotLeaderBroadPrefab;
    [SerializeField] private Transform content;
    [SerializeField] List<SlotLeaderboard_SO> slots = new List<SlotLeaderboard_SO>();

    [SerializeField] private GameObject SearchBar;

    public void backToMainMenu() {
        Manager.Instance.backToMainMenu();
    }
    private void Start() {
        CreateLeaderboardSlot();
        SortListPoint();
        CreateUISlot(slots);
    }
    
    void CreateLeaderboardSlot() {
        string[] Files = System.IO.Directory.GetFiles(Application.persistentDataPath);
        foreach (string file in Files) {
            if (file.Contains(".json")) {
                string[] lines = System.IO.File.ReadAllLines(file);
                foreach (string line in lines) {
                    SaveData data = JsonUtility.FromJson<SaveData>(line);
                    SlotLeaderboard_SO slot = ScriptableObject.CreateInstance<SlotLeaderboard_SO>();
                    slot.playerName = data.playerName;
                    slot.score = data.score;
                    slot.date = data.date;
                    slots.Add(slot);
                }
            }
        }
    }
    public void SearchBar_Func() {
        string searchText = SearchBar.GetComponent<TMP_InputField>().text;
        int _searchTextLength = searchText.Length;
        int _searchSlot = 0;
        List<SlotLeaderboard_SO> _searchResult = new List<SlotLeaderboard_SO>();
        foreach (SlotLeaderboard_SO slot in slots) {
            
            if (slot.playerName.Substring(0, _searchTextLength).ToLower() == searchText.ToLower()) {
                _searchResult.Add(slot);
            }
            else {
                if (_searchResult.Find(x => x == slot) != null) {
                    _searchResult.Remove(slot);
                }
            }
            _searchSlot++;
        }
        ClearUISlot();
        if (searchText == "") { CreateUISlot(slots); return; }
        CreateUISlot(_searchResult);
    }

    private void CreateUISlot(List<SlotLeaderboard_SO> list) {
        foreach (SlotLeaderboard_SO listslot in list) {
            GameObject newSlot = Instantiate(slotLeaderBroadPrefab, content);
            newSlot.GetComponent<SlotLeaderBroad>().SetSlot(listslot.playerName, listslot.score, listslot.date);
        }
    }


    private void ClearUISlot() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
    }
    // Filter order by User
    private void SortListName() {
        slots.Sort(SortName);
    }
    private int SortName(SlotLeaderboard_SO a, SlotLeaderboard_SO b) {
        if (a.playerName.CompareTo(b.playerName) < 0) {
            return 1;
        } else if (a.playerName.CompareTo(b.playerName) > 0) {
            return -1;
        }
        else {
            return 0;
        }
    }
    public void NameButton() {
        SortListName();
        ClearUISlot();
        CreateUISlot(slots);
    }

    // Filter order by Score
    private void SortListPoint() {
        slots.Sort(SortPoint);
    }
    private int SortPoint(SlotLeaderboard_SO a, SlotLeaderboard_SO b) {
        if (a.score < b.score) {
            return 1;
        } else if (a.score > b.score) {
            return -1;
        } else {
            return 0;
        }
    }
    public void PointButton() {
        SortListPoint();
        ClearUISlot();
        CreateUISlot(slots);
    }
    // Filter order by Date
    private void SortListDate() {
        slots.Sort(SortDate);
    }
    private int SortDate(SlotLeaderboard_SO a, SlotLeaderboard_SO b) {
        if (a.date.CompareTo(b.date) < 0) {
            return 1;
        } else if (a.date.CompareTo(b.date) > 0) {
            return -1;
        } else {
            return 0;
        }
    }
    public void DateButton() {
        SortListDate();
        ClearUISlot();
        CreateUISlot(slots);
    }


}