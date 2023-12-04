using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    protected Singleton()
    {
    }


    public static T Instance
    {
        get
        {
            return _instance;
        }
        protected set 
        { 
            _instance = value; 
        }
    }

    

}
