using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RaycastPhysicsImagePlacer : MonoBehaviour
    {
        [SerializeField]
        private Image textureContainer;
        [SerializeField]
        private Camera mainCamera;
        private Shader shader;

        void Start()
        {
            if (textureContainer == null)
            {
                Logger.Instance.LogInfo("Image == null. RaycastImagePlacer disable");
                enabled = false;
            }

            shader = Shader.Find("Universal Render Pipeline/Lit"); ;
        }

        void Update()
        {
            if (Input.touchCount > 0 && textureContainer.IsActive())
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase != TouchPhase.Began || EventSystem.current.IsPointerOverGameObject())
                    return;

                Ray ray = mainCamera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    changeARGameObjectMaterial(hit);
                }
            }

        }

        private void changeARGameObjectMaterial(RaycastHit hit)
        {
            var arGameObject = hit.collider.gameObject.GetComponent<ARGameObject>();

            if (arGameObject == null)
            {
                return;
            }

            if (arGameObject.IsUVSupport)
            {
                var meshCollider = hit.collider as MeshCollider;

                if (meshCollider != null)
                {
                    Mesh mesh = meshCollider.sharedMesh;
                    int submeshIndex = FindSubmeshIndex(mesh, hit.triangleIndex);//mesh.triangles[hit.triangleIndex * 3];
                    meshCollider.GetComponent<Renderer>().materials[submeshIndex] = getNewMaterial();
                }
            }
            else
            {
                arGameObject.gameObject.GetComponent<Renderer>().material = getNewMaterial();
            }
        }

        private int FindSubmeshIndex(Mesh mesh, int triangleIndex)
        {
            int triangle = triangleIndex * 3;
            for (int submesh = 0; submesh < mesh.subMeshCount; submesh++)
            {
                int[] triangles = mesh.GetTriangles(submesh);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    if (triangles[i] == triangle || triangles[i + 1] == triangle || triangles[i + 2] == triangle)
                    {
                        return submesh;
                    }
                }
            }
            return mesh.triangles[triangle];
        }

        private Material getNewMaterial()
        {
            Material newMaterial = new Material(shader);

            if (textureContainer.sprite != null)
            {
                newMaterial.mainTexture = textureContainer.sprite.texture;
            }

            return newMaterial;
        }

    }
}
