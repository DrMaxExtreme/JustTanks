using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _level;

    private const int MinHealth = 1;
    private const int MaxHealth = 3;
    private const int OffsetZ = 1;
    
    public void Generate()
    {
        for (int i = 0; i < _level; i++)
        {
            foreach (var point in _points)
            {
                var position = point.position;
                Vector3 newPoint = new Vector3(position.x, position.y, position.z + 1);
                
                //Instantiate(_cubePrefab, newPoint);
            }
            
            
        }
    }
}
