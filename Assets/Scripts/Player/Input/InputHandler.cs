using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool rightInput { get; private set; }
    public bool leftInput { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                leftInput = true;
            }
            else if (Input.mousePosition.x >= Screen.width / 2)

            {
                rightInput = true;
            }
        }
    }
}
