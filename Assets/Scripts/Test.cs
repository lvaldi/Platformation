using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField]
    private GameObject _platformPrefab;
    [SerializeField]
    private Transform _transform;

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
            GameObject obj = Instantiate(_platformPrefab, _transform);
            Platform platformComponent = obj.GetComponent<Platform>();
            platformComponent.Init();
        }
	}
}