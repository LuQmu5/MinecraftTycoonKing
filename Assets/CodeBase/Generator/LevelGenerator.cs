using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private Road _roadPrefab;
    [SerializeField] private int _roomsCount = 10;

    private Room _lastCreatedRoom;
    private Transform _lastCreatedRoadExitPoint;

    private void Awake()
    {
        _lastCreatedRoom = Instantiate(_roomPrefab, Vector3.zero, Quaternion.identity);

        for (int i = 0; i < _roomsCount; i++)
        {
            CreateRoad();
            CreateRoom();
        }
    }

    private void CreateRoad()
    {
        Transform randomExitPoint = _lastCreatedRoom.ExitPoints[Random.Range(0, _lastCreatedRoom.ExitPoints.Length)];

        Road road = Instantiate(_roadPrefab, randomExitPoint);
        road.transform.localPosition = Vector3.zero;
        road.transform.localEulerAngles = Vector3.zero;
        road.transform.parent = null;

        _lastCreatedRoadExitPoint = road.ExitPoint;
    }

    private void CreateRoom()
    {
        _lastCreatedRoom = Instantiate(_roomPrefab, _lastCreatedRoadExitPoint);
        Transform randomExitPoint = _lastCreatedRoom.ExitPoints[Random.Range(0, _lastCreatedRoom.ExitPoints.Length)];

        if (randomExitPoint.eulerAngles.y == 0)
        {
            _lastCreatedRoom.transform.localPosition = randomExitPoint.localPosition;
            _lastCreatedRoom.transform.eulerAngles = Vector3.up * - 180;
        }
        else if (randomExitPoint.eulerAngles.y == 90)
        {
            _lastCreatedRoom.transform.localPosition = new Vector3(-randomExitPoint.localPosition.z, randomExitPoint.localPosition.y, randomExitPoint.localPosition.x);
            _lastCreatedRoom.transform.eulerAngles = Vector3.up * 90;
        }
        else if (randomExitPoint.eulerAngles.y == 180)
        {
            _lastCreatedRoom.transform.localPosition = -1 * randomExitPoint.localPosition;
            _lastCreatedRoom.transform.eulerAngles = Vector3.zero;
        }
        else if (randomExitPoint.eulerAngles.y == 270)
        {
            _lastCreatedRoom.transform.localPosition = new Vector3(randomExitPoint.localPosition.z, randomExitPoint.localPosition.y, -randomExitPoint.localPosition.x);
            _lastCreatedRoom.transform.eulerAngles = Vector3.up * 270;
        }

        _lastCreatedRoom.transform.parent = null;
    }
}

// 1. создаем комнату
// 2. создаем дорогу в рандомных екзит пойнтах, сбрасываем локал ротейшен и локал позишион
// 3. создаем комнату на екзитпойнте дороги
// 4. Выбираем случайный екзит пойтн комнаты:
// если поворот точки по У равен 0, то ставим те же координаты и -180 поворот по У
// если поворот точки по У равен 90, то меняем х и z местами и помножаем x на минус 1, 90 поворот по У
// если поворот точки по У равен 180, то ставим те же коорды, но со знаком минус и 0 поворот по У
// если поворот точки по У равен 270, то менеяем х и z местами и помножаем z на минус 1, 270 поворот по У


// добавляем одну комнату в первый список, проходимся по этому списку и на каждый элемент создаем
// комнату, проходимся по каждой точке выхода и на ней спавним дорогу *
// на екзит пойнте дороге чекаем сферой (или вхождением в триггер) что она касается другой комнаты (так нельзя) удаляем дорогу и вместо нее спавним
// комнату с сундуком/ тупик и т.д.
// елси же дорога успешно создана, то создаем на ее екзит пойнте комнату и добавляем ее во второй список (и так для каждой дороги)
// проходимся по 2 списку и для каждой комнаты для каждого екзитпойнта кроме того через который вошла дорога создаем новую дорогу
// и на екзит пойнте дороге комнату сделав всю ту же самую проверку на возможность создания, добавляем каждую комнату в первый список
// а из 2 списка убираем ту комнату по которой прошлись и так со всеми  комнатами из 2 списка
// затем повторяем алгоритм по новой