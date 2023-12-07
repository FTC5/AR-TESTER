using Assets.Scripts;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectObjectMenuContentController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject selectObjectPrefab;
    [SerializeField]
    private List<GameObject> objectsPrefab;
    private float defaultScale = 56f;
    private string parentTag = "ParentObject";

    void Start()
    {
        addContent();
    }

    public void AddContent(GameObject arObject)
    {
        var selectableObject = Instantiate(selectObjectPrefab, gameObject.transform);
        var parentComponent = getParentComponentForObjectsPrefab(selectableObject.transform);

        if (parentComponent != null)
        {
            GameObject newObject = Instantiate(arObject, parentComponent);
            var arGameObject = newObject.GetComponent<ARGameObject>();

            if (arGameObject != null)
            {
                arGameObject.SourceName = arObject.name;
            }

            newObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        }
    }

    private void addContent()
    {

        foreach (var obj in objectsPrefab)
        {
            AddContent(obj);
        }

        var savedGameObject = getSavedGameObject();

        if (savedGameObject != null)
        {
            var count = savedGameObject.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                var selectableObject = Instantiate(selectObjectPrefab, gameObject.transform);
                var parentComponent = getParentComponentForObjectsPrefab(selectableObject.transform);

                if (parentComponent != null)
                {
                    initSaveGameObject(parentComponent, savedGameObject[i]);
                }

                savedGameObject.RemoveAt(i);
            }
        }
    }

    private void initSaveGameObject(Transform parentComponent, ARGameObjectSaveData savedGameObject)
    {
        if (parentComponent != null)
        {
            var prefab = objectsPrefab.FirstOrDefault(obj => obj.name == savedGameObject.SourceName);

            if (prefab == null)
            {
                prefab = new GameObject(string.Empty);
            }

            GameObject newObject = Instantiate(prefab, parentComponent);
            var arGameObject = newObject.GetComponent<ARGameObject>();

            if (arGameObject != null)
            {
                arGameObject.SourceName = savedGameObject.SourceName;
            }

            if (savedGameObject.ParentID == Guid.Empty)
            {
                newObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
            }
            else
            {
                newObject.transform.localScale = new Vector3(savedGameObject.Scale[0], savedGameObject.Scale[1], savedGameObject.Scale[2]);
                newObject.transform.localPosition = new Vector3(savedGameObject.Position[0], savedGameObject.Position[1], savedGameObject.Position[2]);
                newObject.transform.localRotation = new Quaternion(savedGameObject.Rotation[0], savedGameObject.Rotation[1], savedGameObject.Rotation[2], savedGameObject.Rotation[3]);
            }

            var meshRender = newObject.GetComponent<MeshRenderer>();

            if (meshRender != null)
            {
                meshRender.material.color = new Color(savedGameObject.Color[0], savedGameObject.Color[1], savedGameObject.Color[1]);
            }

            foreach (var item in savedGameObject.chields)
            {
                initSaveGameObject(newObject.transform, item);
            }
        }
    }

    private Transform getParentComponentForObjectsPrefab(Transform selectableObject)
    {
        var childTransforms = selectableObject.GetComponentsInChildren<Transform>();

        if (childTransforms.Length > 0)
        {
            foreach (var childTransform in childTransforms)
            {
                if (childTransform.tag == parentTag)
                {
                    return childTransform;
                }
            }
        }

        return null;
    }
    private List<ARGameObjectSaveData> getSavedGameObject()
    {
        List<ARGameObjectSaveData> arGameObjects = null;
        using (var db = new LiteDatabase(@$"{Application.persistentDataPath}/ARObject.db"))
        {
            var arGameObjectsTableData = db.GetCollection<ARGameObjectSaveData>("ARGameObjects");
            arGameObjects = arGameObjectsTableData.Query().ToList();
        }

        List<ARGameObjectSaveData> arMainGameObjects = new List<ARGameObjectSaveData>();
        var count = arGameObjects.Count;

        for (int i = count - 1; i >= 0; i--)
        {
            if (arGameObjects[i].ParentID == Guid.Empty)
            {
                arMainGameObjects.Add(arGameObjects[i]);
                arGameObjects.RemoveAt(i);
            }
        }

        foreach (var arMainGameObject in arMainGameObjects)
        {
            arMainGameObject.AddAllChild(ref arGameObjects);
        }

        return arMainGameObjects;
    }
}
