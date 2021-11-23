using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern {
    public class InputHandler : MonoBehaviour {
        public Transform boxTrans;
        private Command buttonW, buttonS, buttonD, buttonA, buttonB, buttonZ, buttonR;
        public static List<Command> oldCommands = new List<Command>();
        private Vector3 boxStartPos;
        private Coroutine replayCoroutine;
        public static bool shouldStartReplay;
        private bool isReplaying;

        private void Start() {
            buttonB = new DoNothing();
            buttonW = new MoveForward();
            buttonS = new MoveReverse();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            buttonZ = new UndoCommand();
            buttonR = new ReplayCommand();

            boxStartPos = boxTrans.position;
        }

        private void Update() {
            if (!isReplaying) {
                HandleInput();
            }

            StartReplay();
        }

        public void HandleInput() {
            if (Input.GetKeyDown(KeyCode.A)) {
                buttonA.Execute(boxTrans, buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.B)) {
                buttonB.Execute(boxTrans, buttonB);
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                buttonD.Execute(boxTrans, buttonD);
            }
            else if (Input.GetKeyDown(KeyCode.R)) {
                buttonR.Execute(boxTrans, buttonR);
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                buttonS.Execute(boxTrans, buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                buttonW.Execute(boxTrans, buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.Z)) {
                buttonZ.Execute(boxTrans, buttonZ);
            }
        }

        void StartReplay() {
            if (shouldStartReplay && oldCommands.Count > 0) {
                shouldStartReplay = false;

                if (replayCoroutine != null) {
                    StopCoroutine(replayCoroutine);
                }

                replayCoroutine = StartCoroutine(ReplayCommands(boxTrans));
            }
        }

        IEnumerator ReplayCommands(Transform boxTrans) {
            isReplaying = true;

            boxTrans.position = boxStartPos;

            for (int i = 0; i < oldCommands.Count; i++) {
                oldCommands[i].Move(boxTrans);

                yield return new WaitForSeconds(0.3f);
            }

            isReplaying = false;
        }
    }
}
