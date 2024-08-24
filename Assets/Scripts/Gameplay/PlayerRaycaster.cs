using UnityEngine;

namespace JustTanks.Gameplay
{
    public class PlayerRaycaster : MonoBehaviour
    {
        private const float RayDistance = 100f;

        [SerializeField] private LayerMask _layerMaskCells;

        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _oldTankPosition;
        private Cell _selectedCell;
        private bool _isSelectedTank = false;

        private void LateUpdate()
        {
            if (Time.timeScale > 0)
            {
                if (_isSelectedTank)
                {
                    if (Input.GetMouseButton(0))
                    {
                        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(_ray, out _hit, RayDistance))
                            _selectedCell.SetPositionTank(_hit.point);
                        else
                            _selectedCell.SetPositionTank(_oldTankPosition);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        Cell newSelectedCell = null;

                        if (Physics.Raycast(ray, out var hit, RayDistance, _layerMaskCells))
                            newSelectedCell = hit.collider.gameObject.GetComponent<Cell>();

                        if (newSelectedCell != null)
                        {
                            if (newSelectedCell.TryFindHaveObject() == false)
                            {
                                newSelectedCell.TakeTank(_selectedCell.GiveTank());
                                _selectedCell.ClearTank();
                            }
                            else if (newSelectedCell.CurrentTank != null)
                            {
                                if (newSelectedCell.IsHaveTankForUpgrade(_selectedCell.CurrentTank.Level) &&
                                    newSelectedCell != _selectedCell)
                                {
                                    _selectedCell.DestroyTank();
                                    newSelectedCell.UpgradeTank();
                                }
                                else
                                {
                                    RevertTankOldPosition();
                                }
                            }
                            else
                            {
                                RevertTankOldPosition();
                            }
                        }
                        else
                        {
                            RevertTankOldPosition();
                        }

                        _isSelectedTank = false;
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    ActivateRay();

                    if (_selectedCell)
                    {
                        _selectedCell.Select();

                        if (_selectedCell.CurrentTank == null)
                            _selectedCell = null;
                    }
                }
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

            if (Physics.Raycast(_ray, out _hit, RayDistance, _layerMaskCells))
                _selectedCell = _hit.collider.gameObject.GetComponent<Cell>();
        }

        private void RevertTankOldPosition()
        {
            _selectedCell.SetPositionTank(_oldTankPosition);
            _isSelectedTank = false;
            _selectedCell.SetTankAttackMode();
            _selectedCell = null;
        }
    }
}
