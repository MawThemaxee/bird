using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class NetWorkAPI_Test
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetJsontoSaveData()
    {
        var json =  "{\"playerName\":\"test\",\"score\":100, \"date\":\"2022-01-01\"}";
        var data = JsonUtility.FromJson<SaveData>(json);
        Assert.AreEqual(data.playerName, "test");
    }
    private Manager _manager;
    [SetUp]
    public void SetUp() {
        // Créer un GameObject pour le Manager
        GameObject managerObject = new GameObject();
        _manager = managerObject.AddComponent<Manager>();

    }

    [Test]
    public void GetFiletoVersion()
    {
        // Appeler la méthode Version
        _manager.Version();
        
        // Ajoutez ici des assertions pour vérifier le comportement attendu
        // Par exemple, vérifier si le fichier Version.json a été créé ou a le contenu attendu
        string path = Application.persistentDataPath + "/Version.json";
        Assert.IsTrue(System.IO.File.Exists(path), "Le fichier Version.json devrait exister. " + path);

        var json = System.IO.File.ReadAllText(path);
        var data = JsonUtility.FromJson<Version>(json);
        Assert.AreEqual(data.version, "1.0", "La version devrait être 1.0.");
    }
    
    [Test]
    public void SavePoint() {
        _manager.Score = 100;
        _manager.SavePoint();

        // Ajoutez ici des assertions pour vérifier le comportement attendu
        string path = Application.persistentDataPath + "/"+ _manager.playerName +".json";
        Assert.IsTrue(System.IO.File.Exists(path), "Le fichier "+_manager.playerName+".json devrait exister.");
        var json = System.IO.File.ReadAllText(path);
        var data = JsonUtility.FromJson<SaveData>(json);
        System.IO.File.Delete(path);
        Assert.AreEqual(data.score, 100, "Le score devrait être 100.");
    }

}
