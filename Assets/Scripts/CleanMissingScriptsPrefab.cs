#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CleanMissingScriptsPrefab
{
    [MenuItem("Tools/Clear Missing Scripts in ALL Prefabs")]
    public static void ClearInPrefabs()
    {
        // Tìm tất cả các file Prefab (.prefab) đang có trong toàn bộ dự án
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                // Xóa missing script trên chính nó và tất cả các object con bên trong cấu trúc Prefab
                int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefab);
                if (removed > 0) count += removed;

                foreach (Transform child in prefab.GetComponentsInChildren<Transform>(true))
                {
                    int childRemoved = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(child.gameObject);
                    if (childRemoved > 0) count += childRemoved;
                }

                // Nếu có lỗi và đã sửa, đánh dấu để Unity lưu lại file asset gốc
                if (removed > 0)
                {
                    EditorUtility.SetDirty(prefab);
                }
            }
        }

        // Lưu lại toàn bộ thay đổi xuống ổ cứng
        AssetDatabase.SaveAssets();
        Debug.Log($"<color=green><b>[Thành công]</b></color> Đã quét và gỡ sạch {count} missing scripts ẩn bên trong các file Prefab gốc!");
    }
}
#endif