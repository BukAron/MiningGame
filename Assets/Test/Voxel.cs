using UnityEngine;

namespace PixelReyn.VoxelSeries.Part2
{
    [System.Serializable]
    public struct Voxel
    {
        public byte ID;
        public byte Health; 

        public bool isSolid
        {
            get
            {
                return ID != 0;
            }
        }

        
        public static Voxel Create(byte id, byte maxHealth = 3)
        {
            return new Voxel { ID = id, Health = maxHealth };
        }
    }
}