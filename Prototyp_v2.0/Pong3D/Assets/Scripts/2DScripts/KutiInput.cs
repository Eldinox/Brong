﻿//using DNomad.RP3Input;
using UnityEngine;
/// <summary>
/// Static class that serves as an adapter for user Inputs.
/// </summary>
public class KutiInput : MonoBehaviour {


	// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
	private static string player1MiddleButtonMapping = "KUTI_P1_MID";

	private static string player1LeftButtonMapping = "KUTI_P1_LEFT";
	private static string player1RightButtonMapping = "KUTI_P1_RIGHT";

	//Direction respective to the player
	// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
	private static string player2MiddleButtonMapping = "KUTI_P2_MID";

	private static string player2LeftButtonMapping = "KUTI_P2_LEFT";
	private static string player2RightButtonMapping = "KUTI_P2_RIGHT";

	private static string menuButtonMapping = "KUTI_MENU";

	// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
	private static float lastMenuButtonValue = 0f;
	private static float lastP2MidButtonValue = 0f;

//	public enum KUTI_KEYCODE
//	{
//		PLAYER_1_LEFT = 0,
//		PLAYER_1_UP = 1,
//		PLAYER_1_RIGHT = 2,
//		MENU = 3,
//		PLAYER_2_LEFT= 4,
//		PLAYER_2_UP = 5,
//		PLAYER_2_RIGHT = 6
//	}

	private static string GetButtonMapping(EKutiButton button)
	{
		string returnValue;
		switch (button)
		{
			case EKutiButton.P1_LEFT:
				returnValue = player1LeftButtonMapping;
				break;
			case EKutiButton.P1_MID:
				returnValue =player1MiddleButtonMapping;
				break;
			case EKutiButton.P1_RIGHT:
				returnValue = player1RightButtonMapping;
				break;

			case EKutiButton.P2_LEFT:
				returnValue = player2LeftButtonMapping;
				break;
			case EKutiButton.P2_MID:
				returnValue = player2MiddleButtonMapping;
				break;
			case EKutiButton.P2_RIGHT:
				returnValue = player2RightButtonMapping;
				break;
			case EKutiButton.MENU:
				returnValue = menuButtonMapping;
				break;

			default:
				returnValue = "noInputMappingFound";
				break;
		}

		return returnValue;
	}

//	public static KUTI_KEYCODE getKeyCodeForKutiButton(EKutiButton button)
//	{
//		KUTI_KEYCODE returnValue;
//		switch (button)
//		{
//			case EKutiButton.P1_LEFT:
//				returnValue = KUTI_KEYCODE.PLAYER_1_LEFT;
//				break;
//			case EKutiButton.P1_MID:
//				returnValue = KUTI_KEYCODE.PLAYER_1_UP;
//				break;
//			case EKutiButton.P1_RIGHT:
//				returnValue = KUTI_KEYCODE.PLAYER_1_RIGHT;
//				break;
//
//			case EKutiButton.P2_LEFT:
//				returnValue = KUTI_KEYCODE.PLAYER_2_LEFT;
//				break;
//			case EKutiButton.P2_MID:
//				returnValue = KUTI_KEYCODE.PLAYER_2_UP;
//				break;
//			case EKutiButton.P2_RIGHT:
//				returnValue = KUTI_KEYCODE.PLAYER_2_RIGHT;
//				break;
//
//			case EKutiButton.MENU:
//				returnValue = KUTI_KEYCODE.MENU;
//				break;
//
//			default:
//				returnValue = KUTI_KEYCODE.MENU;
//				break;
//		}
//
//		return returnValue;
//	}

	public static bool GetKutiButton(EKutiButton button)
	{
		bool returnValue = false;

		// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
		if (GetButtonMapping (button) == player2MiddleButtonMapping) {
			if (Input.GetAxisRaw ("Vertical") > 0) {
				lastP2MidButtonValue = 1f;
				returnValue = true;
			}
		} 
		else if (GetButtonMapping (button) != player2MiddleButtonMapping && GetButtonMapping(button) != menuButtonMapping) {
			returnValue = Input.GetButton (GetButtonMapping (button));
		}

		if (GetButtonMapping (button) == menuButtonMapping && Input.GetAxisRaw ("Vertical") == -1) {
			returnValue = true;
		}

		return returnValue;
	}

	public static bool GetKutiButtonDown(EKutiButton button)
	{
		bool returnValue = false;

		// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
		if (GetButtonMapping (button) == player2MiddleButtonMapping) {
			if (Input.GetAxisRaw ("Vertical") > 0 && lastP2MidButtonValue == 0) {
				lastP2MidButtonValue = 1f;
				returnValue = true;
			}
		} 
		else if (GetButtonMapping (button) != player2MiddleButtonMapping && GetButtonMapping(button) != menuButtonMapping) {
			return (Input.GetButtonDown (GetButtonMapping (button)));
		}

		if (GetButtonMapping (button) == menuButtonMapping && Input.GetAxisRaw ("Vertical") == -1) {
			returnValue = true;
		}

		GetKutiButtonUp (button);
		return returnValue;
	}



	public static bool GetKutiButtonUp(EKutiButton button)
	{
		bool returnValue = false;

		// USB Controller + Android Version Bug - Unfortunately we have to use axis input for 2 buttons (P1 MID = V+, P2 MID = H+)
		if (GetButtonMapping (button) == player2MiddleButtonMapping) {
			if (Input.GetAxisRaw ("Vertical") == 0 && lastP2MidButtonValue == 1) {
				lastP2MidButtonValue = 0f;
				returnValue = true;
			}
		} 
		else if (GetButtonMapping (button) != player2MiddleButtonMapping && GetButtonMapping(button) != menuButtonMapping) {
			returnValue =  Input.GetButtonUp (GetButtonMapping (button));
		}
		return returnValue;
	}

	public static bool GetAnyButtonDownPlayer1()
	{
		return
			(
			GetKutiButtonDown(EKutiButton.P1_LEFT) ||
			GetKutiButtonDown(EKutiButton.P1_MID) ||
			GetKutiButtonDown(EKutiButton.P1_RIGHT)
			);
	}

	public static bool GetAnyButtonDownPlayer2()
	{
		return
			(
			GetKutiButtonDown(EKutiButton.P2_LEFT) ||
			Input.GetAxisRaw("Vertical") > 0 ||
			GetKutiButtonDown(EKutiButton.P2_RIGHT)
			);
	}

	public static bool GetAnyButtonDown()
	{
		return (GetAnyButtonDownPlayer1() || GetAnyButtonDownPlayer2());
	}
}

public enum EKutiButton{
	P1_LEFT,
	P1_MID,
	P1_RIGHT,
	P2_LEFT,
	P2_MID,
	P2_RIGHT,
	MENU
}