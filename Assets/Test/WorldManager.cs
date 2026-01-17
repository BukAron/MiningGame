using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelReyn.VoxelSeries.Part2
{
    public class WorldManager : MonoBehaviour
    {
        public Material worldMaterial;

        private Container container;


        // Start is called before the first frame update
        void Start()
        {
            GameObject cont = new GameObject("Container");
            cont.transform.parent = transform;
            container = cont.AddComponent<Container>();
            container.Initialize(worldMaterial, Vector3.zero);

            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    int randomYHeight = Random.Range(1, 16);
                    for (int y = 0; y < randomYHeight; y++)
                    {
                    // Use the helper we made to set ID 1 and Health 3
                    container[new Vector3(x, y, z)] = Voxel.Create(1, 3);
                    }               
                }
            }


            container.GenerateMesh();
            container.UploadMesh();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}