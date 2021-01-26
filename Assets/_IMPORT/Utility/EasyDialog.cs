using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

//***************************************************************
//  EasyDialog.cs       Author: Glauz
//
//  Very Easy Dialog script not used with a fancy node system to
//  get NPC's to talk. Npc will rotate to Listener and camera will
//  move into cinematic view. Can Play sfx for characters appending 
//  into the sentence and when the message ends.
//
//***************************************************************

public class EasyDialog : MonoBehaviour
    {
        public Text textGui;        
        public string[] messages;
        public AudioClip sfxText, sfxTextEnd, sfxTextFinished;
        public bool faceListener;
        public bool useCamera;

        private AudioSource _audioSource;
        private GameObject _listener;
        private bool _isFacingListener;
        private Vector3 _previousRotation;
        private float _rotationSpeed = 500f;

        private int currentMsg = 0;
        private int totalChar, currentChar = 0;
        private readonly float _charSetDelay = 0.013f; //Time of each character displayed on textGui
        private float _charDelay = 0;

        //Camera
        public Vector3 camOffset, focusOffset;
        private float camSpeed = 5f;
        private bool isCameraInPosition;
        //private UnityEvent OnCameraInPosition, OnCameraPrevPosition;

        public UnityEvent OnStartDialog, OnEndDialog;

        private Speaker state;
        private enum Speaker { Idling, Rotating, Speaking, Finished }

        public void Start()
        {
            _audioSource = Camera.main.GetComponent<AudioSource>();
            if (_audioSource == null) Debug.LogError("AUDIO SOURCE NOT ASSIGNED!");
            if (textGui == null) Debug.LogError("textGUI NOT ASSIGNED!"); 
        }

        //Use this method to speak **Call this class
        public void Speak(GameObject listenerPosition)
        {
            if (messages.Length <= 0) { Debug.LogError("This Speaker has nothing to say!"); return; }
            if (state != Speaker.Idling) { Debug.LogError("This Speaker is already busy!"); return; }

            Input.GetAxisRaw("Vertical"); 

            OnStartDialog.Invoke();
            _charDelay = 0f;
            _previousRotation = transform.rotation.eulerAngles;

            //Store this
            _listener = listenerPosition;

            //Rotate to Listener 
            if (faceListener && state == Speaker.Idling)
                state = Speaker.Rotating;

            //Speak Here
            else
                state = Speaker.Speaking;
        }

        private void LateUpdate()
        {
            //Iterate Until Target is Facing Listener
            if (state == Speaker.Rotating)
                RotateToListener();

            //Slowly Iterate each char for text;
            if (state == Speaker.Speaking)            
                SpeakToListener();            

            if (state == Speaker.Finished)
                WaitForPlayerInput();

            if (state != Speaker.Idling && useCamera)
                FocusCameraOnSpeaker();
        }

        private void RotateToListener()
        {
            var targetRot = Quaternion.LookRotation(_listener.transform.position - transform.position);
            targetRot.x = 0;
            targetRot.z = 0;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);

            //When it looks at listener (rotation finished) go into Speaking State
            if (transform.rotation.eulerAngles.y == targetRot.eulerAngles.y)
                state = Speaker.Speaking;
        }

        private void SpeakToListener()
        {

            if (currentChar == 0)
            {
                totalChar = messages[currentMsg].Length;

                if (currentMsg == 0)
                    _charDelay = _charSetDelay;
            }

            if (_charDelay >= _charSetDelay)
            {
                var message = messages[currentMsg];
                textGui.text += message[currentChar];
                _audioSource.PlayOneShot(sfxText, .25f);
                currentChar++;
                _charDelay = 0;

                if (currentChar >= totalChar) state = Speaker.Finished;
            }

            else
                _charDelay += Time.deltaTime;

            if (Input.GetMouseButton(0))
                _charDelay += Time.deltaTime;

        }

        private void WaitForPlayerInput()
        {
            //_audioSource.PlayOneShot(sfxTextEnd);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentMsg != messages.Length - 1)
                    _audioSource.PlayOneShot(sfxTextEnd, .25f);
                else
                    _audioSource.PlayOneShot(sfxTextFinished, .25f);

                if (currentMsg < messages.Length - 1)
                {
                    textGui.text = string.Empty;
                    currentMsg++;
                    currentChar = 0;
                    _charDelay = -.4f;                    
                    state = Speaker.Speaking;
                }

                //If End on Messages
                else
                {                   
                    print("Chat with " + name + " finished.");
                    ResetSpeaker();
                    OnEndDialog.Invoke();
                }
            }
        }

        private void ResetSpeaker()
        {
            state = Speaker.Idling;
            currentChar = 0;
            currentMsg = 0;
            textGui.text = string.Empty;
            _charDelay = 0f;

            //Temporary
            //transform.rotation = Quaternion.Euler(_previousRotation);
        }

        private void FocusCameraOnSpeaker()
        {
            var cam = Camera.main.gameObject;
            cam.transform.LookAt(transform.position + focusOffset);

            //var targetRot = Quaternion.LookRotation(cam.transform.position - transform.position);
            //cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);
            
            if (cam.transform.position != transform.position + camOffset)
                cam.transform.position = Vector3.Slerp(cam.transform.position,
                                         _listener.transform.position + (_listener.transform.rotation * camOffset),
                                         camSpeed * Time.deltaTime);
        }
    } 

