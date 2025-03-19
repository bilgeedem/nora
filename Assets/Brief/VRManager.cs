using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class VRManager : MonoBehaviour
{
       private static VRManager instance;

    // Referenz zum OVRManager (falls benötigt)
    private OVRManager ovrManager;

    void Awake()
    {
        // Singleton-Muster: Überprüfe, ob bereits eine Instanz existiert
        if (instance != null && instance != this)
        {
            // Wenn ja, zerstöre dieses GameObject
            Destroy(gameObject);
            return;
        }

        // Setze diese Instanz als Singleton
        instance = this;

        // Mache dieses GameObject persistent (wird beim Szenenwechsel nicht zerstört)
        DontDestroyOnLoad(gameObject);

        // Optional: Hole eine Referenz zum OVRManager
        ovrManager = GetComponent<OVRManager>();
        if (ovrManager == null)
        {
            Debug.LogWarning("OVRManager-Komponente nicht gefunden! Stelle sicher, dass sie an diesem GameObject angehängt ist.");
        }
    }

    void Start()
    {
        // Optional: Überprüfe, ob das EventSystem vorhanden ist, und erstelle es falls nötig
        EnsureEventSystemExists();
    }

    void EnsureEventSystemExists()
    {
        // Überprüfe, ob ein EventSystem in der Szene existiert
        if (FindObjectOfType<EventSystem>() == null)
        {
            // Erstelle ein neues EventSystem, falls keines vorhanden ist
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            Debug.Log("EventSystem wurde automatisch erstellt.");
        }
    }
}
