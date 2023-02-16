using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskCells;
    [SerializeField] private LayerMask _layerMaskBoxes;
    [SerializeField] private LayerMask _layerMaskTanks;

    private Ray _ray;
    private RaycastHit _hit;
    private float _rayDistance = 100f;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ActivateRay();

            if (Physics.Raycast(_ray, out _hit, _rayDistance, _layerMaskCells))
            {
                Cell cell = _hit.collider.gameObject.GetComponent<Cell>();

                print(cell);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ActivateRay();

            if (Physics.Raycast(_ray, out _hit, _rayDistance, _layerMaskBoxes))
            {
                Box box = _hit.collider.gameObject.GetComponent<Box>();

                //box. нажал на cell в которой есть box, вызов boxOpen
            }
        }
    }

    private void ActivateRay()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
