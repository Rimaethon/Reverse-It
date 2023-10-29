using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace TheKiwiCoder
{
    [CustomEditor(typeof(BehaviourTreeInstance))]
    public class BehaviourTreeInstanceEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var treeField = new PropertyField();
            treeField.bindingPath = nameof(BehaviourTreeInstance.behaviourTree);

            var loggerField = new PropertyField();
            loggerField.bindingPath = nameof(BehaviourTreeInstance.logging);

            var validateField = new PropertyField();
            validateField.bindingPath = nameof(BehaviourTreeInstance.validate);


            container.Add(treeField);
            container.Add(validateField);
            container.Add(loggerField);

            return container;
        }
    }
}