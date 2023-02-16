using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTank : MonoBehaviour
{
    private Camera _mainCamera;

    private void OnMouseEnter()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit = Physics.Raycast(ray, 

        //if (groundPlane.Raycast(ray, out float position))
        {
            //Vector3 worldPosition = ray.GetPoint(position);
        }
    }
}
