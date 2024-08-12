using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Road[] _roadPrefabs;

    private RoomGenerator _roomGenerator;
    private RoadGenerator _roadGenerator;

    private void Awake()
    {
        _roomGenerator = new RoomGenerator(_roomPrefabs);
        _roadGenerator = new RoadGenerator(_roadPrefabs);
    }
}
