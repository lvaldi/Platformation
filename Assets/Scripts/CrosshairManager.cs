using UnityEngine;
using System.Collections;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField]
    private Crosshair[] _crosshairGOs;

    private void AssignBomb()
    {
        int randPlayerIndex = (int)Random.Range(0, _crosshairGOs.Length);

        
    }
}