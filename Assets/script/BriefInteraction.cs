using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // XR Interaction Toolkit einbinden

public class BriefInteraction : MonoBehaviour
{
    public GameObject smallBrief;  // Das kleine Brief-Objekt
    public GameObject largeBrief;  // Das große Brief-Objekt, das zuerst versteckt ist

    private void Start()
    {
        // Stelle sicher, dass das große Brief-Objekt zu Beginn versteckt ist
        largeBrief.SetActive(false);

        // Optional: Sicherstellen, dass das kleine Brief-Objekt aktiv ist
        if (smallBrief != null)
        {
            smallBrief.SetActive(true);
        }
    }

    public void OnBriefSelected()
    {
        // Zeige das große Brief-Objekt, wenn das kleine ausgewählt wurde
        if (smallBrief != null && largeBrief != null)
        {
            largeBrief.SetActive(true);
        }
    }
}
