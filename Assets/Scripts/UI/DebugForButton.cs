using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugForButton : MonoBehaviour
{
    TextMeshProUGUI _textMesh;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _textMesh.text = "↑";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _textMesh.text = "↓";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _textMesh.text = "→";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _textMesh.text = "←";
        }
    }
}
