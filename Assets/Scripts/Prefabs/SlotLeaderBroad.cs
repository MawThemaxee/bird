using TMPro;
using UnityEngine;
public class SlotLeaderBroad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    public TextMeshProUGUI Name {get { return _name; } set { _name = value; } }
    [SerializeField] private TextMeshProUGUI _score;
    public TextMeshProUGUI Score {get { return _score; } set { _score = value; } }
    [SerializeField] private TextMeshProUGUI _date;
    public TextMeshProUGUI Date {get { return _date; } set { _date = value; } }
    public void SetSlot(string name, int score, string date) {
        Name.text = name;
        Score.text = score.ToString();
        Date.text = date;
    }
}