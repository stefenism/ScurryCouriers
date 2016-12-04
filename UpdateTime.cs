using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

		public Text TimeText;
		private string TimeOutput;

		public float StartTime;
		public float TotalTime = 300f;
		public float CurrentRawTime;

		private int Minutes;
		private int Seconds;
		private int fraction;



		void Awake()
		{
			StartTime = TotalTime - Time.time;


		}

		void Update()
		{

			CurrentRawTime = StartTime - Time.time;

			Minutes = (int)CurrentRawTime / 60;
			Seconds = (int)CurrentRawTime % 60;
			//fraction = (int)(CurrentRawTime * 100) % 100;

			TimeOutput = string.Format ("{0:00}:{1:00}", Minutes, Seconds);

			UpdateTimer();

			//EndGame();

		}

		void UpdateTimer()
		{

		    TimeText.text = "Time Left: " + TimeOutput;

		}

		public void EndGame()
		{
			//Enter in EndGame stuff I.E. display score screen
		}
	}
