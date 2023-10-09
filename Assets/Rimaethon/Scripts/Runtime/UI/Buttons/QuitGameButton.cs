using Rimaethon.Runtime.UI;

namespace Rimaethon.Scripts.UI
{
    public class QuitGameButton : UIButton
    {
        protected override void Awake()
        {
            base.Awake();

            Button.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {

        }
    }
}