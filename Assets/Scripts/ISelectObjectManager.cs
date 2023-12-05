using UnityEngine;

namespace Assets.Scripts
{
    public interface ISelectObjectManager
    {
        void AddToARObject(GameObject gameObject);
        void ChangeColor(Color color);
        void DeleteSelectedObject();
        void SaveARObject();
    }
}