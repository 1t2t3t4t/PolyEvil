using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private float ButtonHoverScale = 1.15f;
        [SerializeReference] private AudioSource AudioSource;

        [SerializeField] private AudioClip MainMenuSong;
        [SerializeField] private AudioClip ButtonClickSound;
        
        private VisualElement _rootElement;
        private Label _startLabel;
        
        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
            _rootElement = GetComponent<UIDocument>().rootVisualElement;
            _startLabel = _rootElement.Q<Label>("StartButton");
            SetUpView();
        }

        private void SetUpView()
        {
            SetUpButtonAnim(_startLabel);
        }

        private void SetUpButtonAnim(VisualElement btn)
        {
            btn.style.color = new StyleColor(Color.white);
            btn.RegisterCallback<MouseEnterEvent>(evt =>
            {
                btn.transform.scale = Vector3.one * ButtonHoverScale;
                btn.style.color = new StyleColor(CommonColor.BloodRed());
            });
            btn.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                btn.transform.scale = Vector3.one;
                btn.style.color = new StyleColor(Color.white);
            });
            btn.RegisterCallback<ClickEvent>(evt =>
            {
                AudioSource.PlayOneShot(ButtonClickSound);
                OnButtonClicked(btn, evt);
            });
        }

        private void OnButtonClicked(VisualElement btn, ClickEvent evt)
        {
            SceneManager.LoadScene("EntraceScene");
        }
    }
}