using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricFire : MonoBehaviour
{
    private Mesh mesh;
    private Material material;
    private Transform cam;

    [SerializeField, Range(1, 20), Tooltip("Controls the number of additional meshes to render in front of and behind the original mesh")]
    private int thickness = 1;
    [SerializeField, Range(0.01f,1f), Tooltip("Controls the total distance between the frontmost mesh and the backmost mesh")]
    private float spread = 0.2f;

    [SerializeField]
    private bool billboard = true;

    private MaterialPropertyBlock materialPropertyBlock;
    private int internalCount;

    private float initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        cam = Camera.main.transform;
        material = GetComponent<MeshRenderer>().sharedMaterial;
        mesh = GetComponent<MeshFilter>().sharedMesh;
        initialPosition = (transform.position.x + transform.position.y + transform.position.z);
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Internal Count = 
        // Thickness 1 : 0
        // Thickness 2 : 2
        // Thickness 3 : 4
        // Thickness 4 : 6
        internalCount = (thickness - 1) * 2;


        // Gap = 
        // Thickness 1 : 0
        // Thickness 2 : 0.5f
        // Thickness 3 : 0.25f
        // Thickness 4 : 0.125f
        float gap = 0;
        if(internalCount > 0)
        {
            gap = spread / internalCount;
        }

        Quaternion cameraRotation = Quaternion.identity;
        if (billboard)
        {
            cameraRotation = cam.rotation;
        }
        

        for(int i = 0; i <= internalCount; i++)
        {
            float itemNumber = i - (internalCount * 0.5f);

            // Moved this to Shader
            //float itemNumber01 = 1f - Mathf.Abs((itemNumber / internalCount) * 2f); 
            
            SetupMaterialPropertyBlock(itemNumber);
            CreateItem(gap, itemNumber, cameraRotation);
        }
    }

    void SetupMaterialPropertyBlock(float item)
    {
        materialPropertyBlock.SetFloat("_ITEMNUMBER", item);
        materialPropertyBlock.SetFloat("_INTERNALCOUNT", internalCount);
        materialPropertyBlock.SetFloat("_INITIALPOSITIONINT", initialPosition);
    }

    void CreateItem(float spacing, float item, Quaternion rotationForBillboard)
    {
        Quaternion newRot = Quaternion.identity;
        Vector3 position = Vector3.zero;
        if (billboard)
        {
            newRot *= rotationForBillboard;
            Vector3 direction = (transform.position - cam.position).normalized;
            position = transform.position - (direction * item * spacing);
        }
        else
        {
            newRot = transform.rotation;
            position = transform.position - (transform.forward * item * spacing);
        }
        

        Matrix4x4 matrix = Matrix4x4.TRS(position, newRot, transform.localScale);
        Graphics.DrawMesh(mesh, matrix, material, 0, null, 0, materialPropertyBlock, false, false, false);
    }
}
