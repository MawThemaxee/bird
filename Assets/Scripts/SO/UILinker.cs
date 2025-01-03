using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UILinker")]
public class UILinker : ScriptableObject
{
    private UIManager UIManager;
    public void SetUIManager(UIManager uiManager) {
        UIManager = uiManager;
    }
    public UIManager GetUIManager() {
        return UIManager;
    }
}
