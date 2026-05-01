using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJsonArray<T>(string json)
    {
        return JsonUtility.FromJson<Wrapper<T>>(WrapJson(json)).Items;
    }

    private static string WrapJson(string rawJson)
    {
        return "{\"Items\":" + rawJson + "}";
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
