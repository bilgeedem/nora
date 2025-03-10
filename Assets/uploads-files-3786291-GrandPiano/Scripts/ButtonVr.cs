using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{
    public GameObject button; // Die Pianotaste
    public UnityEvent onPress; // Event für das Drücken der Taste
    public UnityEvent onRelease; // Event für das Loslassen der Taste
    private AudioSource sound; // Der Audioclip der Taste
    private bool isPressed = false; // Status der Taste (gedrückt oder nicht)
    private Vector3 initialPosition; // Startposition der Taste

    void Start()
    {
        sound = GetComponent<AudioSource>(); // AudioSource des Objekts finden
        initialPosition = button.transform.localPosition; // Startposition speichern
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && button != null && other.CompareTag("VRController")) // Prüfen, ob ein VR-Controller die Taste berührt
        {
            Debug.Log("VR-Controller drückt Taste.");
            button.transform.localPosition = initialPosition + new Vector3(0, -0.03f, 0); // Taste nach unten bewegen
            onPress.Invoke(); // Event auslösen

            // Sound abspielen, wenn verfügbar
            if (sound != null)
            {
                sound.Play();
                Debug.Log("Sound wird abgespielt.");
            }

            isPressed = true; // Taste ist jetzt gedrückt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed && button != null && other.CompareTag("VRController")) // Prüfen, ob der VR-Controller die Taste verlässt
        {
            Debug.Log("VR-Controller lässt Taste los.");
            button.transform.localPosition = initialPosition; // Taste in Ausgangsposition bewegen
            onRelease.Invoke(); // Event für das Loslassen
            isPressed = false; // Taste ist nicht mehr gedrückt
        }
    }
}
