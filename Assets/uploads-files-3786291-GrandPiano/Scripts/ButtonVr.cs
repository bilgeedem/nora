using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class ButtonVr : MonoBehaviour
{
    public GameObject button; // Die Pianotaste
    public UnityEvent onPress; // Event für das Drücken der Taste
    public UnityEvent onRelease; // Event für das Loslassen der Taste
    private AudioSource sound; // Der Audioclip der Taste
    private bool isPressed; // Status der Taste (gedrückt oder nicht)
    private Vector3 initialPosition; // Startposition der Taste
    private InputDevice rightController; // VR-Controller für den rechten Controller
    private InputDevice leftController;  // VR-Controller für den linken Controller

    void Start()
    {
        sound = GetComponent<AudioSource>(); // AudioSource des Objekts finden
        isPressed = false; // Taste ist standardmäßig nicht gedrückt
        initialPosition = button.transform.localPosition; // Startposition speichern

        // Versuchen, die Controller zu finden
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }

        devices.Clear();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }

    void Update()
    {
        if (rightController.isValid || leftController.isValid) // Wenn ein Controller gültig ist
        {
            // Überprüfen, ob der Trigger des Controllers gedrückt wird
            bool triggerValue;
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) || leftController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue))
            {
                if (triggerValue && !isPressed && button != null) // Wenn der Trigger gedrückt wird
                {
                    Debug.Log("Taste wird gedrückt.");
                    button.transform.localPosition = initialPosition + new Vector3(0, -0.3f, 0); // Taste nach unten bewegen
                    onPress.Invoke(); // onPress-Event auslösen

                    // Sound abspielen, wenn verfügbar
                    if (sound != null)
                    {
                        sound.Play();
                        Debug.Log("Sound wird abgespielt.");
                    }

                    isPressed = true; // Taste ist gedrückt
                }
                else if (!triggerValue && isPressed && button != null) // Wenn der Trigger losgelassen wird
                {
                    Debug.Log("Taste wird losgelassen.");
                    button.transform.localPosition = initialPosition; // Taste in Ausgangsposition bewegen
                    onRelease.Invoke(); // onRelease-Event auslösen
                    isPressed = false; // Taste ist nicht mehr gedrückt
                }
            }
        }
    }
}