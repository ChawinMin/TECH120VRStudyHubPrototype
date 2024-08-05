using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteBoardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private WhiteBoard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _LastTouchRot;

    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(origin: _tip.position, direction: transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<WhiteBoard> ();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;
              

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x,y, blockWidth: _penSize, blockHeight: _penSize, _colors);

                    for(float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(a: _lastTouchPos.x, b: x, t: f);
                        var lerpY = (int)Mathf.Lerp(a: _lastTouchPos.y, b: y, t: f);
                        _whiteboard.texture.SetPixels(x, y, blockWidth: _penSize, blockHeight: _penSize, _colors);
                    }

                    transform.rotation = _LastTouchRot;

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _LastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }

            _whiteboard = null;
            _touchedLastFrame = false;
        }
    }
}
