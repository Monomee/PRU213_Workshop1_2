#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CleanMissingScripts
{
    [MenuItem("Tools/Clear All Missing Scripts in Scene")]
    public static void ClearInScene()
    {
        // Tìm tất cả GameObject trong Scene (kể cả các object đang bị ẩn/Active = false)
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>(true);
        int count = 0;

        foreach (GameObject g in allObjects)
        {
            // Hàm native của Unity 2022 để xóa missing script
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(g);
            if (removed > 0) count += removed;
        }

        Debug.Log($"<color=green><b>[Thành công]</b></color> Đã tìm và xóa sạch {count} missing scripts trong Scene này!");
    }
}
#endif