using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FortuneWheelManager : MonoBehaviour
{
	[Header("Game Objects for some elements")]			
	public Button FreeTurnButton;				// This button is showed when you can turn the wheel for free
	public GameObject Circle; 					// Rotatable GameObject on scene with reward objects
	public Text DeltaCoinsText; 				// Pop-up text with wasted or rewarded coins amount
	public Text CurrentCoinsText; 				// Pop-up text with wasted or rewarded coins amount
	

	[Header("How much currency one paid turn costs")]
	[Range(1,5)] public int TurnCost;					// How much coins user waste when turn whe wheel

	private bool _isStarted;					// Flag that the wheel is spinning

	[Header("Params for each sector")]
	public FortuneWheelSector[] Sectors;		// All sectors objects

	private float _finalAngle;					// The final angle is needed to calculate the reward
	private float _startAngle;    				// The first time start angle equals 0 but the next time it equals the last final angle
	private float _currentLerpRotationTime;		// Needed for spinning animation
	private int _currentCoinsAmount = 1000;		// Started coins amount. In your project it should be picked up from CoinsManager or from PlayerPrefs and so on
	private int _previousCoinsAmount;

	


	

	

	// Set TRUE if you want to let players to turn the wheel for coins while free turn is not available yet
	[Header("Can players turn the wheel for currency?")]
	public bool IsPaidTurnEnabled = true;

	

	// Flag that player can turn the wheel for free right now
	private bool _isFreeTurnAvailable;

	private FortuneWheelSector _finalSector;

	private void Awake ()
	{
		_previousCoinsAmount = _currentCoinsAmount;
		// Show our current coins amount
		CurrentCoinsText.text = _currentCoinsAmount.ToString ();

		// Show sector reward value in text object if it's set
		foreach (var sector in Sectors)
		{
			if (sector.ValueTextObject != null)
			{
                sector.RewardValue *= TurnCost;
                sector.ValueTextObject.GetComponent<Text>().text = sector.RewardValue.ToString();
            }

        }

		
	}

	private void TurnWheelForFree() { TurnWheel (true);	}
	private void TurnWheelForCoins() { TurnWheel (false); }

	private void TurnWheel (bool isFree)
	{
		_currentLerpRotationTime = 0f;

		// All sectors angles
		int[] sectorsAngles = new int[Sectors.Length];

		// Fill the necessary angles (for example if we want to have 12 sectors we need to fill the angles with 30 degrees step)
		// It's recommended to use the EVEN sectors count (2, 4, 6, 8, 10, 12, etc)
		for (int i = 1; i <= Sectors.Length; i++)
		{
			sectorsAngles[i - 1] =  360 / Sectors.Length * i;
		}

		//int cumulativeProbability = Sectors.Sum(sector => sector.Probability);

		double rndNumber = UnityEngine.Random.Range (1, Sectors.Sum(sector => sector.Probability));

		// Calculate the propability of each sector with respect to other sectors
		int cumulativeProbability = 0;
		// Random final sector accordingly to probability
		int randomFinalAngle = sectorsAngles [0];
		_finalSector = Sectors[0];

		for (int i = 0; i < Sectors.Length; i++) {
			cumulativeProbability += Sectors[i].Probability;

			if (rndNumber <= cumulativeProbability) {
				// Choose final sector
				randomFinalAngle = sectorsAngles [i];
				_finalSector = Sectors[i];
				break;
			}
		}

		int fullTurnovers = 5;

		// Set up how many turnovers our wheel should make before stop
		_finalAngle = fullTurnovers * 360 + randomFinalAngle;

		// Stop the wheel
		_isStarted = true;

		_previousCoinsAmount = _currentCoinsAmount;

		// Decrease money for the turn if it is not free turn
		if (!isFree) {
			_currentCoinsAmount -= TurnCost;

			// Show wasted coins
			DeltaCoinsText.text = String.Format ("-{0}", TurnCost);
			DeltaCoinsText.gameObject.SetActive (true);

			// Animations for coins
			StartCoroutine (HideCoinsDelta ());
			StartCoroutine (UpdateCoinsAmount ());
		} 
	}

	public void TurnWheelButtonClick ()
	{
		if (_isFreeTurnAvailable) {
			TurnWheelForFree ();
		} else {
			// If we have enabled paid turns
			if (IsPaidTurnEnabled) {
				// If player have enough coins
				if (_currentCoinsAmount >= TurnCost) {
					TurnWheelForCoins ();
				}
			}
		}
	}



	

	private void Update ()
	{
		

		if (!_isStarted)
			return;

		// Animation time
		float maxLerpRotationTime = 4f;

		// increment animation timer once per frame
		_currentLerpRotationTime += Time.deltaTime;

		// If the end of animation
		if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
			_currentLerpRotationTime = maxLerpRotationTime;
			_isStarted = false;
			_startAngle = _finalAngle % 360;

			//GiveAwardByAngle ();
			_finalSector.RewardCallback.Invoke();
			StartCoroutine (HideCoinsDelta ());
		} else {
			// Calculate current position using linear interpolation
			float t = _currentLerpRotationTime / maxLerpRotationTime;

			// This formulae allows to speed up at start and speed down at the end of rotation.
			// Try to change this values to customize the speed
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
			Circle.transform.eulerAngles = new Vector3 (0, 0, angle);	
		}
	}

	/// <summary>
	/// Sample callback for giving reward (in editor each sector have Reward Callback field pointed to this method)
	/// </summary>
	/// <param name="awardCoins">Coins for user</param>
	public void RewardCoins (int awardCoins)
	{
		_currentCoinsAmount += awardCoins;
		// Show animated delta coins
		DeltaCoinsText.text = String.Format("+{0}", awardCoins);
		DeltaCoinsText.gameObject.SetActive (true);
		StartCoroutine (UpdateCoinsAmount ());
	}

	// Hide coins delta text after animation
	private IEnumerator HideCoinsDelta ()
	{
		yield return new WaitForSeconds (1f);
		DeltaCoinsText.gameObject.SetActive (false);
	}

	// Animation for smooth increasing and decreasing the number of coins
	private IEnumerator UpdateCoinsAmount ()
	{
		const float seconds = 0.5f; // Animation duration
		float elapsedTime = 0;

		while (elapsedTime < seconds) {
			CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp (_previousCoinsAmount, _currentCoinsAmount, (elapsedTime / seconds))).ToString ();
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame ();
		}

		_previousCoinsAmount = _currentCoinsAmount;

		CurrentCoinsText.text = _currentCoinsAmount.ToString ();
	}

	

	private void EnableButton (Button button)
	{
		button.interactable = true;
		button.GetComponent<Image> ().color = new Color(255, 255, 255, 1f);
	}

	private void DisableButton (Button button)
	{
		button.interactable = false;
		button.GetComponent<Image> ().color = new Color(255, 255, 255, 0.5f);
	}

	// Function for more readable calls
	private void EnableFreeTurnButton () { EnableButton (FreeTurnButton); }
	private void DisableFreeTurnButton () {	DisableButton (FreeTurnButton);	}
	

	


}

/**
 * One sector on the wheel
 */
[Serializable]
public class FortuneWheelSector : System.Object
{
	[Tooltip("Text object where value will be placed (not required)")]
	public GameObject ValueTextObject;

	[Tooltip("Value of reward")]
	public int RewardValue = 100;

	[Tooltip("Chance that this sector will be randomly selected")]
	[RangeAttribute(0, 100)]
	public int Probability = 100;

	[Tooltip("Method that will be invoked if this sector will be randomly selected")]
	public UnityEvent RewardCallback;
}

