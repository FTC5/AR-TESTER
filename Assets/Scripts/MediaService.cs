
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MediaService : MonoBehaviour
{
    [SerializeField]
    private XRInteractionManagerBlockUI xrInteractionManager;
    private Image image;


    public void Start()
    {
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        xrInteractionManager.AddOnBlock(BlockInteractors);
    }

    void OnDisable()
    {
        xrInteractionManager.RemoveOnBlock(BlockInteractors);
    }

    public bool BlockInteractors()
    {
        return true;
    }

    public void GetMedia()
    {
        if (Application.isEditor)
        {
            Logger.Instance.LogInfo("Try get media");
        }
        else
        {
            NativeGallery.GetImageFromGallery((path) =>
            {
                if(string.IsNullOrEmpty(path))
                {
                    return;
                }

                Texture2D texture = NativeGallery.LoadImageAtPath(path);

                if (texture == null)
                {
                    Logger.Instance.LogInfo("Couldn't load texture from " + path);
                    return;
                }

                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            });
        }
    }

}
