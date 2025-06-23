using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName = "FlurScene"; // Szene, die geladen werden soll

    void Start()
    {
        // Den Button automatisch holen und die Methode registrieren
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(SwitchScene);
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
