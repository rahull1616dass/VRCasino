/*using static uniwue.hci.vilearn.EventManager;

namespace uniwue.hci.vilearn
{
    /// <summary>Uses the XRControllerInput script and triggers application events</summary>
    public class XRControllerInputEventDispatcherReverbG2 : XRControllerInputEventDispatcher
    {
        void Start()
        {
            StartCoroutine(Initialize());
        }

        private void Update()
        {
            if (!xrControllerInputLeft || !xrControllerInputRight) return;

            if (xrControllerInputLeft.primary2DAxisUp) ExecuteLocomotion(xrControllerInputLeft.Hand, WalkForward);
            if (xrControllerInputLeft.primary2DAxisDown) ExecuteLocomotion(xrControllerInputLeft.Hand, WalkBack);
            if (xrControllerInputRight.primary2DAxisUp) ExecuteLocomotion(xrControllerInputRight.Hand, WalkForward);
            if (xrControllerInputRight.primary2DAxisDown) ExecuteLocomotion(xrControllerInputRight.Hand, WalkBack);

            if (xrControllerInputLeft.primary2DAxisUp) Scroll(xrControllerInputLeft.Hand, 0f, 1f);
            if (xrControllerInputLeft.primary2DAxisDown) Scroll(xrControllerInputLeft.Hand, 0f, -1f);
            if (xrControllerInputRight.primary2DAxisUp) Scroll(xrControllerInputRight.Hand, 0f, 1f);
            if (xrControllerInputRight.primary2DAxisDown) Scroll(xrControllerInputRight.Hand, 0f, -1f);
        }

        protected override void ConfigureRightHandInput(XRControllerInput xrControllerInput)
        {
            xrControllerInput.OnPrimary2DAxisRightPress.AddListener(() => ExecuteDeckerEvent(xrControllerInput.Hand, DeckerSlideNext));
            xrControllerInput.OnPrimary2DAxisLeftPress.AddListener(() => ExecuteDeckerEvent(xrControllerInput.Hand, DeckerSlidePrevious));

            xrControllerInput.OnPrimary2DAxisRightPress.AddListener(() => ExecuteLocomotion(xrControllerInput.Hand, RotateRight));
            xrControllerInput.OnPrimary2DAxisLeftPress.AddListener(() => ExecuteLocomotion(xrControllerInput.Hand, RotateLeft));

            xrControllerInput.OnTriggerPress.AddListener(() => Execute(xrControllerInput.Hand, UpdateActiveInteractionAnchor));
            xrControllerInput.OnTriggerPress.AddListener(() => SurfaceInteractionButton(true, xrControllerInput.Hand));
            xrControllerInput.OnTriggerRelease.AddListener(() => SurfaceInteractionButton(false, xrControllerInput.Hand));

            xrControllerInput.OnPrimaryButtonPress.AddListener(() => Execute(xrControllerInput.Hand, UpdateActiveInteractionAnchor));
            xrControllerInput.OnPrimaryButtonPress.AddListener(() => SurfaceInteractionButton(true, xrControllerInput.Hand));
            xrControllerInput.OnPrimaryButtonRelease.AddListener(() => SurfaceInteractionButton(false, xrControllerInput.Hand));

            xrControllerInput.OnGripPress.AddListener(() => Execute(xrControllerInput.Hand, Grab));
            xrControllerInput.OnGripRelease.AddListener(() => Execute(xrControllerInput.Hand, Release));
        }

        protected override void ConfigureLeftHandInput(XRControllerInput xrControllerInput)
        {
            xrControllerInput.OnPrimary2DAxisRightPress.AddListener(() => ExecuteDeckerEvent(xrControllerInput.Hand, DeckerSlideNext));
            xrControllerInput.OnPrimary2DAxisLeftPress.AddListener(() => ExecuteDeckerEvent(xrControllerInput.Hand, DeckerSlidePrevious));

            xrControllerInput.OnPrimary2DAxisRightPress.AddListener(() => ExecuteLocomotion(xrControllerInput.Hand, RotateRight));
            xrControllerInput.OnPrimary2DAxisLeftPress.AddListener(() => ExecuteLocomotion(xrControllerInput.Hand, RotateLeft));

            xrControllerInput.OnTriggerPress.AddListener(() => Execute(xrControllerInput.Hand, UpdateActiveInteractionAnchor));
            xrControllerInput.OnTriggerPress.AddListener(() => SurfaceInteractionButton(true, xrControllerInput.Hand));
            xrControllerInput.OnTriggerRelease.AddListener(() => SurfaceInteractionButton(false, xrControllerInput.Hand));

            xrControllerInput.OnPrimaryButtonPress.AddListener(() => Execute(xrControllerInput.Hand, UpdateActiveInteractionAnchor));
            xrControllerInput.OnPrimaryButtonPress.AddListener(() => SurfaceInteractionButton(true, xrControllerInput.Hand));
            xrControllerInput.OnPrimaryButtonRelease.AddListener(() => SurfaceInteractionButton(false, xrControllerInput.Hand));

            xrControllerInput.OnGripPress.AddListener(() => Execute(xrControllerInput.Hand, Grab));
            xrControllerInput.OnGripRelease.AddListener(() => Execute(xrControllerInput.Hand, Release));
        }
    }
}*/