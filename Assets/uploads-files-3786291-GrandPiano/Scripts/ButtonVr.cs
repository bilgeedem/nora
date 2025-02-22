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
    private bool isPressed; // Status der Taste (gedrückt oder nicht)
    private Vector3 initialPosition; // Startposition der Taste

    void Start()
    {
        sound = GetComponent<AudioSource>(); // AudioSource des Objekts finden
        isPressed = false; // Taste ist standardmäßig nicht gedrückt
        initialPosition = button.transform.localPosition; // Startposition speichern
    }

    // Wird aufgerufen, wenn ein Mauszeiger auf das Objekt klickt
    private void OnMouseDown()
    {
        if (!isPressed && button != null) // Prüfen, ob die Taste nicht schon gedrückt ist
        {
            Debug.Log("Taste wird gedrückt."); // Debug-Meldung
            button.transform.localPosition = initialPosition + new Vector3(0, -0.3f, 0); // Taste nach unten bewegen
            onPress.Invoke(); // onPress-Event auslösen

            // Sound abspielen, wenn verfügbar
            if (sound != null) 
            {
                sound.Play(); 
                Debug.Log("Sound wird abgespielt."); 
            }

            isPressed = true; // Taste ist jetzt gedrückt
        }
    }

    // Wird aufgerufen, wenn die Maus losgelassen wird
    private void OnMouseUp()
    {
        if (isPressed && button != null) // Prüfen, ob die Taste gedrückt war
        {
            Debug.Log("Taste wird losgelassen."); // Debug-Meldung
            button.transform.localPosition = initialPosition; // Taste in Ausgangsposition bewegen
            onRelease.Invoke(); // onRelease-Event auslösen
            isPressed = false; // Taste ist nicht mehr gedrückt
        }
    }
}
