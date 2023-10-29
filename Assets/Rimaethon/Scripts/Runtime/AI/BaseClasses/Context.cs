using Rimaethon.Runtime.AI.EnemyComponentScripts;
using Rimaethon.Scripts.Enemy;
using UnityEngine;

namespace TheKiwiCoder
{
    // The context is a shared object every node has access to.
    // Commonly used components and subsytems should be stored here
    // It will be somewhat specfic to your game exactly what to add here.
    // Feel free to extend this class 
    public class Context
    {
        public Animator Animator;


        public GameObject GameObject;
        public GroundCheck GroundCheck;
        public EnemyCommonData PlayerData;
        public Rigidbody2D Rigidbody2D;
        public SpriteRenderer SpriteRenderer;
        public Transform Transform;
        public WallColliderCheck WallColliderCheck;


        public static Context CreateFromGameObject(GameObject gameObject)
        {
            var context = new Context();
            context.GameObject = gameObject;
            context.Transform = gameObject.transform;
            context.Animator = gameObject.GetComponent<Animator>();
            context.Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            context.GroundCheck = gameObject.GetComponentInChildren<GroundCheck>();
            context.WallColliderCheck = gameObject.GetComponentInChildren<WallColliderCheck>();
            context.PlayerData = gameObject.GetComponentInParent<EnemyCommonData>();
            context.SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();


            return context;
        }
    }
}