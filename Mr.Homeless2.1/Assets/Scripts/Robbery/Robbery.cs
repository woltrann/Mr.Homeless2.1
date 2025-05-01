using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Robbery : MonoBehaviour
{
    public InputActionAsset InputActions;
    
    private InputAction RobAction;
    private float robInput;

    [Header("UI")]
    public Slider progressBar;
    public Slider suspicionBar;

    [Header("Settings")]
    public float moveSpeed = 0.3f;
    public float suspicionGainPerSecond = 0.2f;
    public float suspicionGainOnFastMove = 0.4f;
    public float successThreshold = 0.95f;

    [Header("NPC Behavior")]
    public float npcLookIntervalMin = 2f;
    public float npcLookIntervalMax = 5f;
    public float npcLookDuration = 1f;

    private float npcLookTimer = 0f;
    private bool isNPCLooking = false;

    private bool isGameActive = true;

    private void Awake()
    {
        // Initialize input actions
        RobAction = InputActions.FindAction("RobController/Rob");

    }

    private void OnEnable()
    {
        // Enable input actions
        InputActions.FindActionMap("RobController").Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        InputActions.FindActionMap("RobController").Disable();
    }

    void Start()
    {
        npcLookTimer = Random.Range(npcLookIntervalMin, npcLookIntervalMax);
        progressBar.value = 0f;
        suspicionBar.value = 0f;
    }

    void Update()
    {
        if (!isGameActive) return;
        robInput = RobAction.ReadValue<float>();
        HandleInput();
        HandleNPCLookLogic();
        CheckGameOverConditions();
    }

    void HandleInput()
    {
        float movement = robInput * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(movement) > 0.01f)
        {
            progressBar.value += Mathf.Abs(movement);

            // Eðer NPC bakýyorsa veya hareket hýzlýysa þüphe artar
            if (isNPCLooking || Mathf.Abs(robInput) > 2f)
                suspicionBar.value += suspicionGainOnFastMove * Time.deltaTime;
            else
                suspicionBar.value += suspicionGainPerSecond * Mathf.Abs(robInput) * Time.deltaTime;
        }
    }

    void HandleNPCLookLogic()
    {
        npcLookTimer -= Time.deltaTime;

        if (npcLookTimer <= 0)
        {
            isNPCLooking = true;
            npcLookTimer = npcLookDuration;
            Debug.Log("NPC bakýyor!");

            Invoke(nameof(StopNPCLook), npcLookDuration);
        }
    }

    void StopNPCLook()
    {
        isNPCLooking = false;
        npcLookTimer = Random.Range(npcLookIntervalMin, npcLookIntervalMax);
        Debug.Log("NPC baþka yöne baktý.");
    }

    void CheckGameOverConditions()
    {
        if (suspicionBar.value >= 1f)
        {
            isGameActive = false;
            Debug.Log("YAKALANDIN!");
        }
        else if (progressBar.value >= successThreshold)
        {
            isGameActive = false;
            Debug.Log("BAÞARIYLA CÜZDAN ÇALINDI.");
        }
    }
}
