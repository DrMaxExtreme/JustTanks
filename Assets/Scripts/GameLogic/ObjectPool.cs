using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JustTanks.GameLogic
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private int _capacity;

        private List<GameObject> _pool = new List<GameObject>();

        protected List<GameObject> Pool => _pool;

        protected void ClearPool()
        {
            _pool.Clear();
        }

        protected void Initialize(GameObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject spawned = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                spawned.SetActive(false);

                _pool.Add(spawned);
            }
        }

        protected bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.activeSelf == false);

            return result != null;
        }

        protected bool TryFindObject()
        {
            return _pool.All(p => p.activeSelf == false);
        }

        protected void Release()
        {
            foreach (var item in _pool)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
