using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlayerDice {
    // Reference to the MonoBehaviour context (optional)
    [SerializeField] MonoBehaviour context;
    // Prefab for the die
    [SerializeField] GameObject diePrefab;
    // Particle effect for the dice
    [SerializeField] GameObject diceParticleEffect;
    // Spawn point for the dice
    [SerializeField] Transform diceSpawn;
    // Array to hold Rigidbody components for the current dice
    Rigidbody[] currentDiceRb;

    // Force to apply when rolling the dice
    [SerializeField] float rollForce = 10f;
    // Torque amount applied to the dice
    [SerializeField] float torqueAmount = 5f;

    // Layer mask for the ground
    [SerializeField] LayerMask groundLayer;

    // Method to throw the dice
    public void ThrowDice(int diceNb, Action<int[]> callBack, Action endCallBack) {
        currentDiceRb = new Rigidbody[diceNb];
        Vector3 currentSpawn = diceSpawn.position;

        for (int i = 0; i < diceNb; i++) {
            currentSpawn += Vector3.right * i * 3;

            currentDiceRb[i] = UnityEngine.Object.Instantiate(diePrefab, currentSpawn, Quaternion.identity).GetComponent<Rigidbody>();
            UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(diceParticleEffect, currentDiceRb[i].position, Quaternion.identity), 2f);
            AddForceToDice(currentDiceRb[i]);
        }

        context.StartCoroutine(WaitForDiceToStop(diceNb, callBack, endCallBack));
    }

    // Method to add force and torque to the dice
    void AddForceToDice(Rigidbody rb) {
        Vector3 targetPos = new Vector3(UnityEngine.Random.Range(-1f, 1f), 3, UnityEngine.Random.Range(-1f, 1f));
        Vector3 dir = targetPos - rb.transform.position;
        rb.AddForce(dir * rollForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * torqueAmount, ForceMode.Impulse);
    }

    // Coroutine to wait for the dice to stop rolling
    IEnumerator WaitForDiceToStop(int diceNb, Action<int[]> callBack, Action endCallBack) {
        bool isFinish = false;
        bool currentFinishState = true;

        while (!isFinish) {
            currentFinishState = true;

            for (int i = 0; i < currentDiceRb.Length; i++) {
                if (!currentDiceRb[i].IsSleeping())
                    currentFinishState = false;
            }

            if (currentFinishState)
                isFinish = true;

            yield return null;
        }

        yield return null;

        int[] diceResult = new int[diceNb];

        for (int i = 0; i < currentDiceRb.Length; i++) {
            diceResult[i] = GetDiceResult(currentDiceRb[i]);
        }

        callBack?.Invoke(diceResult);

        endCallBack?.Invoke();

        DestroyDice();
    }

    // Method to determine the result of the dice throw
    int GetDiceResult(Rigidbody rb) {
        Vector3[] dieFaces = new Vector3[] { rb.transform.right, -rb.transform.forward, rb.transform.up, -rb.transform.up, rb.transform.forward, -rb.transform.right };
        for (int i = 0; i < dieFaces.Length; i++) {
            if (Physics.Raycast(rb.transform.position, dieFaces[i], 6, groundLayer)) {
                return i + 1;
            }
        }

        return -1;
    }

    // Method to destroy the dice
    void DestroyDice() {
        for (int i = 0; i < currentDiceRb.Length; i++) {
            UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(diceParticleEffect, currentDiceRb[i].position, Quaternion.identity), 2f);
            UnityEngine.Object.Destroy(currentDiceRb[i].gameObject);
        }
    }
}
