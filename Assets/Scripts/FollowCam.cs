using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform target;
    ObjectPooler ObjectPooler;

    public float smoothing;
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0f, ObjectPooler.Instance.GetLastActiveTileY() - ObjectPooler.Instance.tileHeight,transform.position.z), smoothing * Time.deltaTime);
    }
    private void Update()
    {
        if (transform.position.y > ObjectPooler.Instance.activeTiles[0].transform.position.y + (2 * ObjectPooler.Instance.tileHeight))
        {
            ObjectPooler.Instance.DeleteTile();
        }
    }
}
