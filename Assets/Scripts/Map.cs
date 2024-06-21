using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject _mapCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _mapCamera.SetActive(!_mapCamera.activeInHierarchy);
        }
    }
}