using UnityEngine;

public class EditorGridGizmo : MonoBehaviour
{
    [Header("Cài Đặt Lưới Thể Tích (3D)")]
    [Tooltip("Tổng kích thước 3D của khối lưới (Dài, Cao, Rộng) (m). Mặc định là hình hộp 500x100x500.")]
    public Vector3 size = new Vector3(500f, 100f, 500f);

    [Tooltip("Kích thước của mỗi ô vuông 3D (m). Các ô sẽ là hình lập phương nếu dùng uniform scale cho size.")]
    public float cellSize = 100f;

    [Tooltip("Màu của đường lưới")]
    public Color gridColor = new Color(0f, 1f, 0f, 0.5f); // Màu xanh lá hơi trong suốt

    [Tooltip("Nếu đúng, vẽ lưới phân chia 3D bên trong cả khối. Nếu sai, chỉ vẽ khung hộp ngoài.")]
    public bool drawVolumetricCells = true;

    private void OnDrawGizmos()
    {
        // Gán màu cho Gizmo
        Gizmos.color = gridColor;

        // Lấy vị trí tâm của GameObject hiện tại
        Vector3 centerPos = transform.position;

        // 1. Vẽ KHUNG NGOÀI (hình hộp dây - wireframe cube)
        Gizmos.DrawWireCube(centerPos, size);

        if (!drawVolumetricCells) return;

        // 2. Tính toán tọa độ giới hạn của lưới 3D để lưới luôn nằm giữa GameObject
        Vector3 halfSize = size / 2f;

        // **BẮT ĐẦU VẼ LƯỚI PHÂN CHIA TRONG CẢ 3 CHIỀU**

        // --- ĐƯỜNG SONG SONG VỚI TRỤC X (CHẠY DỌC THEO Y VÀ Z) ---
        // Chúng ta lặp qua từng mức của Y và Z, vẽ các đoạn thẳng từ X min đến X max.
        for (float y = -halfSize.y; y <= halfSize.y; y += cellSize)
        {
            for (float z = -halfSize.z; z <= halfSize.z; z += cellSize)
            {
                Vector3 start = centerPos + new Vector3(-halfSize.x, y, z);
                Vector3 end = centerPos + new Vector3(halfSize.x, y, z);
                Gizmos.DrawLine(start, end);
            }
        }

        // --- ĐƯỜNG SONG SONG VỚI TRỤC Y (CHẠY DỌC THEO X VÀ Z) ---
        // Chúng ta lặp qua từng mức của X và Z, vẽ các đoạn thẳng từ Y min đến Y max.
        for (float x = -halfSize.x; x <= halfSize.x; x += cellSize)
        {
            for (float z = -halfSize.z; z <= halfSize.z; z += cellSize)
            {
                Vector3 start = centerPos + new Vector3(x, -halfSize.y, z);
                Vector3 end = centerPos + new Vector3(x, halfSize.y, z);
                Gizmos.DrawLine(start, end);
            }
        }

        // --- ĐƯỜNG SONG SONG VỚI TRỤC Z (CHẠY DỌC THEO X VÀ Y) ---
        // Chúng ta lặp qua từng mức của X và Y, vẽ các đoạn thẳng từ Z min đến Z max.
        for (float x = -halfSize.x; x <= halfSize.x; x += cellSize)
        {
            for (float y = -halfSize.y; y <= halfSize.y; y += cellSize)
            {
                Vector3 start = centerPos + new Vector3(x, y, -halfSize.z);
                Vector3 end = centerPos + new Vector3(x, y, halfSize.z);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}