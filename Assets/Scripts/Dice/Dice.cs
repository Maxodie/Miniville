using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlayerDice {
    [SerializeField] MonoBehaviour context;
    [SerializeField] GameObject diePrefab;
    [SerializeField] Transform diceSpawn; 
    Rigidbody[] currentDiceRb;

    [SerializeField] float rollForce = 10f;
    [SerializeField] float torqueAmount = 5f;

    [SerializeField] LayerMask groundLayer;

    public void ThrowDice(int diceNb, Action<int[]> callBack, Action endCallBack) {
        currentDiceRb = new Rigidbody[diceNb];
        Vector3 currentSpawn = diceSpawn.position;

        for(int i=0; i<diceNb; i++) {
            currentSpawn += Vector3.right * i * 3;
            
            currentDiceRb[i] = UnityEngine.Object.Instantiate(diePrefab, currentSpawn, Quaternion.identity).GetComponent<Rigidbody>();
            currentDiceRb[i].GetComponent<Animator>().SetTrigger("CallAnim");
            AddForceToDice(currentDiceRb[i]);
        }

        context.StartCoroutine(WaitForDiceToStop(diceNb, callBack, endCallBack));
    }

    void AddForceToDice(Rigidbody rb) {
        Vector3 targetPos = new Vector3(UnityEngine.Random.Range(-1f, 1f), 3, UnityEngine.Random.Range(-1f, 1f));
        Vector3 dir = targetPos - rb.transform.position;
        rb.AddForce(dir * rollForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * torqueAmount, ForceMode.Impulse);
    }

    IEnumerator WaitForDiceToStop(int diceNb, Action<int[]> callBack, Action endCallBack) {
        bool isFinish = false;
        bool currentFinishState = true;

        while(!isFinish) {
            currentFinishState = true;

            for(int i=0; i<currentDiceRb.Length; i++) {
                if(!currentDiceRb[i].IsSleeping())
                    currentFinishState = false; 
            }

            if(currentFinishState)
                isFinish = true;

            yield return null;
        }

        yield return null;

        int[] diceResult = new int[diceNb];

        for(int i=0; i < currentDiceRb.Length; i++) {
            diceResult[i] = GetDiceResult(currentDiceRb[i]);
        }

        callBack?.Invoke(diceResult);

        endCallBack?.Invoke();

        DestroyDice();
    }

    int GetDiceResult(Rigidbody rb) {
        Vector3[] dieFaces = new Vector3[] {-rb.transform.right, -rb.transform.forward, rb.transform.up, -rb.transform.up, rb.transform.forward, rb.transform.right};
        for(int i=0; i < dieFaces.Length; i++) {
            if(Physics.Raycast(rb.transform.position, dieFaces[i], 2, groundLayer)) {
                return i+1;
            }
        }

        return 0;
    }

    void DestroyDice() {
        for(int i=0; i<currentDiceRb.Length; i++) {
            currentDiceRb[i].GetComponent<Animator>().SetTrigger("CallAnim");
            UnityEngine.Object.Destroy(currentDiceRb[i].gameObject);
        }
    }
}
