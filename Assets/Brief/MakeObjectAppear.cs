using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectAppear : MonoBehaviour
{
    public GameObject objectToShow1; // Das Objekt, das erscheinen soll
    public GameObject objectToShow2;

    private void Start()
    {
        objectToShow1.SetActive(false); // Objekt unsichtbar machen
        objectToShow2.SetActive(false); // Objekt unsichtbar machen
    }

    public void ShowObjects()
    {
        objectToShow1.SetActive(true); // Objekt sichtbar machen
        objectToShow2.SetActive(true); // Objekt sichtbar machen
    }
}

