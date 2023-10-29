using System;
using System.Collections.Generic;
using Rimaethon.Utility;
using UnityEngine;

namespace TheKiwiCoder
{
    public class BehaviourTreeInstance : MonoBehaviour
    {
        // The main behaviour tree asset
        [Tooltip("BehaviourTree asset to instantiate during Awake")]
        public BehaviourTree behaviourTree;

        [Tooltip("Run behaviour tree validation at startup (Can be disabled for release)")]
        public bool validate = true;

        [Tooltip("Logging object to use for debug messages")]
        public Logging logging;

        // Storage container object to hold game object subsystems
        private Context context;
        private BehaviourTree runtimeTree;

        [SerializeField]
        public BehaviourTree RuntimeTree
        {
            get
            {
                if (runtimeTree != null)
                    return runtimeTree;
                return behaviourTree;
            }
        }

        private void FixedUpdate()
        {
            if (runtimeTree) runtimeTree.Update();
        }

        private void OnEnable()
        {
            var isValid = ValidateTree();
            if (isValid)
            {
                context = CreateBehaviourTreeContext();
                runtimeTree = behaviourTree.Clone();
                runtimeTree.Bind(context, logging);
            }
            else
            {
                runtimeTree = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            if (!runtimeTree) return;

            BehaviourTree.Traverse(runtimeTree.rootNode, n =>
            {
                if (n.drawGizmos) n.OnDrawGizmos();
            });
        }


        private Context CreateBehaviourTreeContext()
        {
            return Context.CreateFromGameObject(gameObject);
        }

        private bool ValidateTree()
        {
            if (!behaviourTree)
            {
                Debug.LogWarning($"No BehaviourTree assigned to {name}, assign a behaviour tree in the inspector");
                return false;
            }

            var isValid = true;
            if (validate)
            {
                string cyclePath;
                isValid = !IsRecursive(behaviourTree, out cyclePath);

                if (!isValid) Debug.LogError($"Failed to create recursive behaviour tree. Found cycle at: {cyclePath}");
            }

            return isValid;
        }

        private bool IsRecursive(BehaviourTree tree, out string cycle)
        {
            // Check if any of the subtree nodes and their decendents form a circular reference, which will cause a stack overflow.
            var treeStack = new List<string>();
            var referencedTrees = new HashSet<BehaviourTree>();

            var cycleFound = false;
            var cyclePath = "";

            Action<Node> traverse = null;
            traverse = node =>
            {
                if (!cycleFound)
                    if (node is SubTree subtree && subtree.treeAsset != null)
                    {
                        treeStack.Add(subtree.treeAsset.name);
                        if (referencedTrees.Contains(subtree.treeAsset))
                        {
                            var index = 0;
                            foreach (var tree in treeStack)
                            {
                                index++;
                                if (index == treeStack.Count)
                                    cyclePath += $"{tree}";
                                else
                                    cyclePath += $"{tree} -> ";
                            }

                            cycleFound = true;
                        }
                        else
                        {
                            referencedTrees.Add(subtree.treeAsset);
                            BehaviourTree.Traverse(subtree.treeAsset.rootNode, traverse);
                            referencedTrees.Remove(subtree.treeAsset);
                        }

                        treeStack.RemoveAt(treeStack.Count - 1);
                    }
            };
            treeStack.Add(tree.name);

            referencedTrees.Add(tree);
            BehaviourTree.Traverse(tree.rootNode, traverse);
            referencedTrees.Remove(tree);

            treeStack.RemoveAt(treeStack.Count - 1);
            cycle = cyclePath;
            return cycleFound;
        }
    }
}