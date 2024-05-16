using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceLoader : MonoBehaviour
{
    public static T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}