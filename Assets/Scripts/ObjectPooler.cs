using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] tilesPrefabs;
    public List<GameObject> activeTiles;
    public List<GameObject> pooledObjects;
    public float tileHeight = 3.39f;
    int numberOfTiles = 3;
    [SerializeField] Transform playerTransform;
    [SerializeField] Camera cam;
    public static ObjectPooler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        activeTiles = new List<GameObject>();
        for(int i = 0; i < numberOfTiles; i++)
        {
            GameObject go = Instantiate(tilesPrefabs[i]);
            if (activeTiles.Count != 3)
            {
                go.SetActive(true);
                activeTiles.Add(go);
                pooledObjects.Add(go);
                if (activeTiles.Count == 3)
                    i = -1;
            }
            else
            {
                go.SetActive(false);
                pooledObjects.Add(go);
            }
        }
    }
    private void Update()
    {
        if (playerTransform.position.y > GetLastActiveTileY() - tileHeight)
        {
            GameObject poolObj = GetPooledObject();
            poolObj.transform.position = new Vector3(0, GetLastActiveTileY() + tileHeight, 0);
            activeTiles.Add(poolObj);
            poolObj.SetActive(true);
        }
    }


    public GameObject GetPooledObject()
    {
        List<GameObject> inactiveObjects = pooledObjects.FindAll(go => !go.activeInHierarchy);

        return inactiveObjects.Count > 0 ?
            inactiveObjects[Random.Range(0, inactiveObjects.Count)] : null;
    }
    public float GetLastActiveTileY()
    {
        return activeTiles[activeTiles.Count - 1].transform.position.y;
    }
    public void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles[0].GetComponentInChildren<Ground>().ResetObj();
        activeTiles.RemoveAt(0);
    }
}
