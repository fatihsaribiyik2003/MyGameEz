using System.Collections;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    public float speed = 2f;
    public float delayBetweenMovements = 1f;

    private Vector3 lastPosition; // Yeni eklenen değişken

    private void Start()
    {
        lastPosition = transform.position; // Başlangıçta lastPosition'ı başlangıç pozisyonuyla ayarla
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            // Platform waypoint1'e doğru hareket eder
            yield return MoveToWaypoint(waypoint1.position);

            // Belirli bir süre bekler
            yield return new WaitForSeconds(delayBetweenMovements);

            // Platform waypoint2'ye doğru hareket eder
            yield return MoveToWaypoint(waypoint2.position);

            // Belirli bir süre bekler
            yield return new WaitForSeconds(delayBetweenMovements);
        }
    }

    IEnumerator MoveToWaypoint(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            // Hedefe doğru yumuşak bir şekilde hareket eder
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            lastPosition = transform.position; // Her güncelleme sırasında son pozisyonu güncelle

            // Bir sonraki frame'e geçer
            yield return null;
        }
    }

    // lastPosition'u dışarıdan alabilmek için bir metot ekleyebilirsiniz
    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }
}