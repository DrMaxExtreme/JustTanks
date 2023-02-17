using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskCells;

    private Ray _ray;
    private RaycastHit _hit;
    private float _rayDistance = 100f;
    private Vector3 _oldTankPosition;
    private Cell _selectedCell;
    private bool _isSelectedTank = false;

    private void LateUpdate()
    {
        if (_isSelectedTank)
        {
            if (Input.GetMouseButton(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, _rayDistance))
                    _selectedCell.SetPositionTank(_hit.point);
                else
                    _selectedCell.SetPositionTank(_oldTankPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _selectedCell.SetPositionTank(_oldTankPosition);
                _isSelectedTank = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ActivateRay();

            if (_selectedCell)
                _selectedCell.Select();
        }
    }

    public void SelectedTank()
    {
        _isSelectedTank = true;
        _oldTankPosition = _selectedCell.transform.position;
    }

    private void ActivateRay()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, _rayDistance, _layerMaskCells))
            _selectedCell = _hit.collider.gameObject.GetComponent<Cell>();
    }
}
