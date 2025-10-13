//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LineUpManager : MonoBehaviour
//{
//    public List<GameObject> npcs = new List<GameObject>();
//    public float speed = 3;
//    public List<Vector2>npcsPositions = new List<Vector2>();
//    // Start is called before the first frame update
//    void Start()
//    {
//        StartLine();
//        for (int i = 0; i < npcs.Count; i++) 
//        {
//            npcsPositions.Add(npcs[i].transform.position);
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void StartLine()
//    {
//        while (npcs.Count > 0)
//        {
//            StartCoroutine(LineUpTimer());

//        }

//    }

//    public IEnumerator LineUpTimer()
//    {
//        yield return new WaitForSeconds(3f);
//        int counter = 1;
//        //npcs[i - 1];
//        npcs.RemoveAt(npcs.Count - 1);
//        for (int i = npcs.Count-1; i >= 0; i--)
//        {
//            npcs[i].SetActive(false);

//            while(npcs[i - (counter+1)].transform.position !>= npcsPositions)
//            {
//                npcs[i - (counter + 1)].transform.Translate(Vector3.right * speed * Time.deltaTime);
//            }

//        }

//    }

//}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCLineManager : MonoBehaviour
{
    public List<GameObject> npcs = new List<GameObject>();
    public float speed = 3f;
    public float moveDelay = 3f; // Time between NPC movements
    private List<Vector3> npcStartPositions = new List<Vector3>();

    void Start()
    {
        // Store initial positions
        for (int i = 0; i < npcs.Count; i++)
        {
            npcStartPositions.Add(npcs[i].transform.position);
        }

        StartCoroutine(LineUpRoutine());
    }

    IEnumerator LineUpRoutine()
    {
        while (npcs.Count > 0)
        {
            yield return new WaitForSeconds(moveDelay); // Wait before next move

            if (npcs.Count == 0) break; // Safety check

            // Remove and disable the front NPC
            GameObject frontNPC = npcs[0];
            npcs.RemoveAt(0);
            frontNPC.SetActive(false);

            // Move remaining NPCs to the right
            for (int i = 0; i < npcs.Count; i++)
            {
                Animator animator;

                animator = npcs[i].GetComponent<Animator>();

                Vector3 targetPosition = new Vector3(npcStartPositions[i].x, npcs[i].transform.position.y, npcs[i].transform.position.z); // Move to the next position

                animator.SetBool("isWalking", true);

                while (Vector3.Distance(npcs[i].transform.position, targetPosition) > 0.01f)
                {
                    npcs[i].transform.position = Vector3.MoveTowards(
                        npcs[i].transform.position,
                        targetPosition,
                        speed * Time.deltaTime
                    );
                    yield return null; // Wait until next frame
                }
                npcs[i].transform.position = targetPosition; // Snap to exact position
                animator.SetBool("isWalking", false);
            }
        }
    }
}
