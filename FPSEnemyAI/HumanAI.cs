using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class HumanAI : Attackable
{
#region AI Init Data

    [Header("AI Data")]
    [SerializeField] protected string human_Name = "";

    [Header("AI difficulty")]
    [Range(0, 10)] [SerializeField] private int shooting_Skills = 5;
    [Range(0, 10)] [SerializeField] private int walking_Speed = 3;
    [Range(0, 10)] [SerializeField] private int runnning_Speed = 7;

    [Header("Checking")]
    [SerializeField] protected Vector3 chasingPos = default;
    [SerializeField] protected GameObject chasingTarget = null;

    protected enum State{
        Idle, // Resting
        Searching, // Finding Player
        ChasePos,
        ChaseTarget
    }
    [SerializeField] protected State state;

    [Header("Eye Data")]
    [SerializeField] private float eye_angle = 60f;
    [SerializeField] private float eye_distance = 50f;
    [SerializeField] private LayerMask ob_layerMask = 1 << 10;
    [SerializeField] private GameObject inSightObject = null;

    protected NavMeshAgent navMesh;
    protected Rigidbody rigidbody;

#endregion

    protected virtual void Start(){
        AI_Init();
        StartCoroutine("DetectFunc");
    }

    protected virtual void Update(){
        switch(state){
            case State.Idle:
            Idle();
            break;
            case State.Searching:
            Searching();
            break;
            case State.ChasePos:
            ChasePos();
            break;
            case State.ChaseTarget:
            ChaseTarget();
            break;
            default:
            break;
        }
    }

#region DetectFunc

    private IEnumerator DetectFunc(){
        var wait = new WaitForSeconds(0.15f);
        while(true){
            SetTarget();
            yield return wait;
        }
    }
    private void SetTarget(){
        EyeFindTarget();
    }
    private void EyeFindTarget(){

        Collider[] ob_cols = Physics.OverlapSphere(transform.position, eye_distance, ob_layerMask);

        if(ob_cols.Length < 1)
            return;

        Transform tar_trans = ob_cols[0].transform;

        Vector3 eye_direction = (tar_trans.position - transform.position).normalized;
        float angle = Vector3.Angle(eye_direction, transform.forward);

        if(angle < eye_angle * 0.5f){

            if(Physics.Raycast(transform.position, eye_direction, out RaycastHit hit, eye_distance)){
                if(hit.transform.gameObject.layer == 10){
                    inSightObject = hit.transform.gameObject;
                    return;
                }
            }

        }
        inSightObject = null;
    }


#endregion

#region  AI_FSM
    protected virtual void Idle(){

    }
    protected virtual void Searching(){
        
    }
    protected virtual void ChasePos(){
        
    }
    protected virtual void ChaseTarget(){
        
    }
#endregion

#region Initializing
    protected virtual void AI_Init(){
        navMesh = this.GetComponent<NavMeshAgent>();
        rigidbody = this.GetComponent<Rigidbody>();
        this.gameObject.layer = 9;
        SetTag();
        SetState(State.Idle);
        InitTarget();

    }
    protected virtual void SetTag(){
        this.gameObject.tag = "Human";
    }
    protected virtual void SetState(State data){
        this.state = data;
    }
    protected void InitTarget(){
        chasingPos = default;
        chasingTarget = null;
    }

#endregion


}
