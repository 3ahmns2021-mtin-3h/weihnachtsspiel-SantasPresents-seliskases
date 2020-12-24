using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singletons")]
    public GameObject weihnachtsmannPrefab;
    public Canvas canvasObject;
    [Space]
    public Transform spawnPosition;

    private void Awake()
    {
        SetWeihnachtsmann(Instantiate(weihnachtsmannPrefab, spawnPosition.position, Quaternion.identity));
        SetCanvas();

        weihnachtsmann.transform.SetParent(canvas.transform);
    }

    #region Singleton
    private void SetWeihnachtsmann(GameObject instaniatedObject)
    {
        weihnachtsmann = instaniatedObject;
    }
    public static GameObject weihnachtsmann;
    #endregion

    #region Singleton
    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;
    #endregion
}
