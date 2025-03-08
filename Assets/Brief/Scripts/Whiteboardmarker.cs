using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Whiteboardmarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;  // Größe des Markers
    [SerializeField] private Color _penColor = Color.black;  // Farbe des Markers

    private Renderer _renderer;
    private Color[] _colors;  // Array zur Speicherung der Farbdaten
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;  // Das Whiteboard, auf dem gezeichnet wird
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;  // Überwacht, ob der Marker in der letzten Frame berührt wurde
    private Quaternion _lastTouchRot;

    void Start()
    {
        // Initialisiere den Renderer und die Farbdaten
        _renderer = _tip.GetComponent<Renderer>();
        _colors = new Color[_penSize * _penSize];
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i] = _penColor;  // Setze alle Farben auf die Markerfarbe
        }

        _tipHeight = _tip.localScale.y;  // Höhe des Markers
    }

    void Update()
    {
        Draw();  // Zeichenfunktion in jedem Frame
    }

    private void Draw()
    {
        // Überprüfen, ob der Marker das Whiteboard berührt
        if (Physics.Raycast(_tip.position, -transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                // Wenn das Whiteboard noch nicht gesetzt wurde, hole es
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                // Berechne die Berührposition auf dem Whiteboard
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // Berechne die Pixelposition auf der Whiteboard-Textur
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                // Verhindere das Zeichnen außerhalb der Texturgrenzen
                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                // Zeichnen, wenn der Marker in der letzten Frame berührt wurde
                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    // Zeichnen zwischen den letzten und aktuellen Berührpunkten für eine glattere Linie
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    // Setze die Rotation des Markers auf die vorherige Rotation
                    transform.rotation = _lastTouchRot;

                    // Wende die Änderungen auf die Whiteboard-Textur an
                    _whiteboard.texture.Apply();
                }

                // Speichere die aktuelle Position und Rotation für die nächste Frame
                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;  // Markiere, dass der Marker diese Frame berührt hat
                return;
            }
        }

        // Wenn das Whiteboard nicht mehr berührt wird, setze alle Variablen zurück
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}