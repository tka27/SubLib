namespace ExtensionsMain
{
    using System.Collections.Generic;
    using UnityEngine;


    public static class ExtensionsMain
    {
        public static T GetRandomElement<T>(this List<T> collection)
        {
            int random = Random.Range(0, collection.Count);
            return collection[random];
        }

        public static T GetRandomElement<T>(this List<T> collection, out int index)
        {
            int random = Random.Range(0, collection.Count);
            index = random;
            return collection[random];
        }

        public static int GetSameItemsCount<T>(this T[] array, T item)
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(item)) count++;
            }
            return count;
        }

        public static Quaternion RandomRotation(this Quaternion rotation)
        {
            float xRandom = Random.Range(0, 360);
            float yRandom = Random.Range(0, 360);
            float zRandom = Random.Range(0, 360);
            Vector3 angles = new Vector3(xRandom, yRandom, zRandom);
            return Quaternion.Euler(angles);
        }

        public static Quaternion RandomYRotation(this Quaternion rotation)
        {
            float random = Random.Range(0, 360);
            Vector3 angles = new Vector3(0, random, 0);
            return Quaternion.Euler(angles);
        }

        public static List<Transform> GetActiveChilds(this GameObject go)
        {
            var array = go.GetComponentsInChildren<Transform>(go);
            List<Transform> childs = new List<Transform>();

            for (int i = 1; i < array.Length; i++) //first is parent
            {
                if (array[i].gameObject.activeSelf) childs.Add(array[i]);
            }
            return childs;
        }

        public static bool TrySetActive(this GameObject go, bool value)
        {
            if (go.activeSelf == value) return false;
            go.SetActive(value);
            return true;
        }

        public static void HorizontalLookAt(this Transform transform, Vector3 target)
        {
            target.y = transform.position.y;
            Vector3 lookVector = target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookVector);
            transform.rotation = rotation;
        }

        public static void HorizontalSoftLookAt(this Transform transform, Vector3 target, float rotationSpeed = 5)
        {
            target.y = transform.position.y;
            Vector3 lookVector = target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        public static bool IsNearby(this Transform transform, in Vector3 target, float distance = 1)
        {
            var tempTgt = target;
            tempTgt.y = transform.position.y;

            if ((transform.position - tempTgt).magnitude < distance) return true;
            return false;
        }

        public static float DistanceTo(this Transform transform, Transform target)
        {
            return (transform.position - target.position).magnitude;
        }

        public static float DistanceTo(this Transform transform, Vector3 targetPosition)
        {
            return (transform.position - targetPosition).magnitude;
        }

        public static void MoveInHierarchy(this Transform transform, int index = int.MaxValue)
        {
            transform.SetSiblingIndex(index);
        }

        public static T[] CreateTypeArray<T>(this T type, int count) where T : struct
        {
            T[] array = new T[count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = type;
            }
            return array;
        }

        public static T GetNearestObject<T>(this Transform transform, List<T> objects) where T : MonoBehaviour
        {
            T nearestObject = default;
            var nearestDistance = float.MaxValue;

            foreach (var item in objects)
            {
                var distance = item.transform.DistanceTo(transform.position);

                if (distance > nearestDistance) continue;

                nearestDistance = distance;
                nearestObject = item;
            }
            return nearestObject;
        }

        public static List<T> TryRemoveItem<T>(this List<T> list, T item, out bool isChanged) where T : Object
        {
            isChanged = false;
            int index = -1;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == item)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) return list;

            isChanged = true;
            list.RemoveAt(index);
            Debug.Log(item + " removed from " + list);
            return list;
        }

    }
}

