using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainSlime;
    public Button walkBut;
    public Camera cam;
    private void Start()
    {
        
        walkBut.onClick.AddListener(delegate {  ChangeStateTo(SlimeAnimationState.Walk); });
    }
    void Idle()
    {
        LookAtCamera();
        mainSlime.GetComponent<EnemyAi>().CancelGoNextDestination();
        ChangeStateTo(SlimeAnimationState.Idle);
    }
    public void ChangeStateTo(SlimeAnimationState state)
    {
       if (mainSlime == null) return;    
       if (state == mainSlime.GetComponent<EnemyAi>().currentState) return;

       mainSlime.GetComponent<EnemyAi>().currentState = state ;
    }
    void LookAtCamera()
    {
       mainSlime.transform.rotation = Quaternion.Euler(new Vector3(mainSlime.transform.rotation.x, cam.transform.rotation.y, mainSlime.transform.rotation.z));   
    }
}
