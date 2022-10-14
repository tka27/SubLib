using System.Collections.Generic;
using UnityEngine;
using ExtensionsMain;

namespace Utils
{
    public static class Collider
    {
        public static List<UnityEngine.Collider> Overlap(in BoxCollider collider)
        {
            var result = new List<UnityEngine.Collider>();
            result.AddRange(Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation));
            return result.TryRemoveItem(collider, out _);
        }
    }

    public static class Vector3
    {
        public static UnityEngine.Vector3 DisplaceXZ(float range = 1)
        {
            UnityEngine.Vector3 displace = new();
            displace.x = Random.Range(-range, range);
            displace.z = Random.Range(-range, range);
            return displace;
        }

        public static UnityEngine.Vector3 Displace(float range = 1)
        {
            UnityEngine.Vector3 displace = new();
            displace += DisplaceXZ(range);
            displace.y = Random.Range(-range, range);
            return displace;
        }
    }
}

