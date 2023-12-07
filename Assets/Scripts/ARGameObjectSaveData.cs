using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ARGameObjectSaveData
    {
        [NonSerialized]
        public string tag = "ARObject";
        [NonSerialized]
        public List<ARGameObjectSaveData> chields = new List<ARGameObjectSaveData>();
        [BsonId]
        public Guid ID { get; set; }
        public Guid ParentID { get; set; }
        public float[] Position { get; set; } = new float[0];
        public float[] Scale { get; set; } = new float[0];
        public float[] Rotation { get; set; } = new float[0];
        public string SourceName { get; set; }


        public float[] Color { get; set; }
        //public Collider collider;
        //public MeshRenderer meshRenderer;
        public ARGameObjectSaveData()
        {

        }

        public ARGameObjectSaveData(Transform objectTransform, Guid parentId) : this(objectTransform)
        {
            ParentID = parentId;
            if (parentId != Guid.Empty)
            {
                Position = new float[]
                {
                    objectTransform.transform.localPosition.x,
                    objectTransform.transform.localPosition.y,
                    objectTransform.transform.localPosition.z
                };

                Scale = new float[]
                {
                    objectTransform.transform.localScale.x,
                    objectTransform.transform.localScale.y,
                    objectTransform.transform.localScale.z
                };

                Rotation = new float[]
                {
                    objectTransform.transform.localRotation.x,
                    objectTransform.transform.localRotation.y,
                    objectTransform.transform.localRotation.z,
                    objectTransform.transform.localRotation.w
                };
            }
        }

        public ARGameObjectSaveData(Transform objectTransform)
        {
            if (objectTransform.tag != tag)
            {
                return;
            }

            ID = Guid.NewGuid();



            var colorObj = objectTransform.GetComponent<MeshRenderer>().material.color;
            Color = new float[]
            {
                colorObj.r,
                colorObj.g,
                colorObj.b,
            };

            var arGameObject = objectTransform.GetComponent<ARGameObject>();

            if (arGameObject != null)
            {
                SourceName = arGameObject.SourceName;
            }

            for (int i = 0; i < objectTransform.transform.childCount; i++)
            {
                var chieldObj = objectTransform.transform.GetChild(i);
                if (chieldObj.tag == tag)
                {
                    var obj = new ARGameObjectSaveData(chieldObj, ID);
                    chields.Add(obj);
                }

            }

        }

        public List<ARGameObjectSaveData> GetAll()
        {
            var arChieldGameObjects = new List<List<ARGameObjectSaveData>>();

            foreach (var chield in chields)
            {
                arChieldGameObjects.Add(chield.GetAll());
            }

            var arGameObjects = arChieldGameObjects.SelectMany(gameObjects => gameObjects).ToList();
            arGameObjects.Add(this);

            return arGameObjects;
        }

        public void AddAllChild(ref List<ARGameObjectSaveData> arGameObjects, bool removeChieldFromList = true)
        {
            var count = arGameObjects.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (arGameObjects[i].ParentID == ID)
                {
                    chields.Add(arGameObjects[i]);
                    if (removeChieldFromList)
                    {
                        arGameObjects.RemoveAt(i);
                    }
                }
            }

            foreach (var chield in chields)
            {
                chield.AddAllChild(ref arGameObjects, removeChieldFromList);
            }
        }
        //public List<ARGameObjectSaveData> GetAll()
        //{
        //    var arChieldGameObjects = new List<List<ARGameObjectSaveData>>();

        //    foreach (var chield in chields)
        //    {
        //        arChieldGameObjects.Add(chield.GetAll());
        //    }

        //    var arGameObjects = arChieldGameObjects.SelectMany(gameObjects => gameObjects).ToList();
        //    arGameObjects.Add(this);

        //    return arGameObjects;
        //}
    }
}
