using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBoxes : MonoBehaviour
{
    [SerializeField] private int _countAddBoxes;
    [SerializeField] private SpawnerBoxes _spawnerBoxes;

    public void Activate()
    {
        _spawnerBoxes.Activate(_countAddBoxes);
    }
}
