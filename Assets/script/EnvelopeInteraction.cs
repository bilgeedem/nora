using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnvelopeInteraction : MonoBehaviour  // Hier den Klassennamen ändern
{ public GameObject briefKlappe;  // Die Klappe des Briefumschlags
    public float movementSpeed = 2f;  // Geschwindigkeit des Bewegens
    public float rotationSpeed = 5f;  // Geschwindigkeit der Rotation

    private bool isOpen = false;  // Status, ob der Brief geöffnet ist oder nicht

    // Zielposition und Zielrotation für die Klappe, wenn sie geöffnet wird
    private Vector3 targetPosition = new Vector3(-0.328999996f, 1.01300001f, 0.194999993f);
    private Vector3 targetRotation = new Vector3(90f, 180f, 0f);

    // Startposition und Startrotation für die Klappe
    private Vector3 startPosition = new Vector3(-0.328999996f, 1.02400005f, 0.201000005f);
    private Vector3 startRotation = new Vector3(358.40509f, 0f, 180f);

    private void Start()
    {
        // Setze die Klappe auf die Startposition und Startrotation
        briefKlappe.transform.position = startPosition;
        briefKlappe.transform.rotation = Quaternion.Euler(startRotation);
    }

    public void OnSelectEntered()  // Wird vom XR Interactor ausgelöst
    {
        if (!isOpen)
        {
            StartCoroutine(OpenEnvelope());
        }
    }

    private IEnumerator OpenEnvelope()
    {
        Vector3 initialPosition = briefKlappe.transform.position;
        Quaternion initialRotation = briefKlappe.transform.rotation;

        // Zielrotation als Quaternion
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);

        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            // Position sanft interpolieren
            briefKlappe.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed);

            // Rotation sanft interpolieren
            briefKlappe.transform.rotation = Quaternion.Slerp(initialRotation, targetQuaternion, timeElapsed);

            timeElapsed += Time.deltaTime * movementSpeed;

            yield return null;
        }

        // Final sicherstellen, dass Position und Rotation exakt der Zielposition entsprechen
        briefKlappe.transform.position = targetPosition;
        briefKlappe.transform.rotation = targetQuaternion;

        isOpen = true;  // Status auf geöffnet setzen
    }
}
