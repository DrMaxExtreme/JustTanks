using UnityEngine;
using UnityEngine.UI;

namespace JustTanks.Decorations
{
    public class LandMover : MonoBehaviour
    {
        private const float Speed = 0.0275f;

        [SerializeField] private RawImage _land;

        private float _positionY;

        private void Update()
        {
            _positionY += Speed * Time.deltaTime;

            if (_positionY > 1)
                _positionY = 0;

            _land.uvRect = new Rect(0, _positionY, _land.uvRect.width, _land.uvRect.height);
        }
    }
}
