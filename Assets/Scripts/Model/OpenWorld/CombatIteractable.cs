using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.OpenWorld
{
    public class CombatIteractable : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorLock;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }


        private void OnMouseEnter()
        {
            Cursor.SetCursor(cursorLock,Vector2.zero, CursorMode.Auto);
        }


        private void OnMouseExit()
        {
            Cursor.SetCursor(null,Vector2.zero, CursorMode.Auto);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}