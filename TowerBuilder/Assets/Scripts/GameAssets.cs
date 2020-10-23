using UnityEngine;


/// <summary>
///  This Class will hold all of our Assests so
///  in scripts we do not have to access by strings
///  Now this will live in the Resources Folder
///  Where all of our Game Prefabs or others
///  can be placed in normal folders
/// </summary> 
public class GameAssets : MonoBehaviour
{

    private static GameAssets instance;
    public static GameAssets Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameAssets>("GameAssets");
            }
            return instance;
        }
    }

    [Header("Game Prefabs")]
    [Space]
    [Header("Enemies")]
    public Transform pf_Enemy;
    public Transform pf_EnemyDeathParticles;
    [Header("Buildings")]
    public Transform pf_BuildingConstruction;
    public Transform pf_BuildingDestroyedParticles;
    public Transform pf_BuildingPlacedParticles;
    [Header("Other Items")]
    public Transform pf_arrowProjectile;
}
