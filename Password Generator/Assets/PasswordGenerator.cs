using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordGenerator : MonoBehaviour
{
    public TMP_Dropdown prefixDropdown;
    public TMP_Dropdown bodyLengthDropdown;
    public TMP_Dropdown bodyContentDropdown;
    public TMP_Dropdown suffixDropdown;
    public TMP_Dropdown finalCharDropdown;

    [SerializeField] TextMeshProUGUI prefix;
    [SerializeField] TextMeshProUGUI bodyLength;
    [SerializeField] TextMeshProUGUI bodyContent;
    [SerializeField] TextMeshProUGUI suffix;
    [SerializeField] TextMeshProUGUI finalChar;
    [SerializeField] TextMeshProUGUI outputPanel;
    [SerializeField] Toggle autoModeToggle;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text inputPlaceholder;
    [SerializeField] TMP_Text finalPasswordDisplay;
    [SerializeField] GameObject helpPanel;


    private string inputPasswordBase;
    private string prefixTxt; 
    private string bodyLengthTxt;
    private string bodyChar;
    private string bodyContentTxt; 
    private string suffixTxT; 
    private string finalCharTxt;

    private string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
    private string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string allCaseChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string chars1337 = "4bcd3fgh1jklmn0pqr5tuvwxyz4BCD3FGH1JKLMN0PQR5TUVWXYZ";
    private string numbers = "1234567890";
    private string specialChars = "~`!@#$%^&*()_-+={[}]|:'<,>.?/";

    private string lowerCaseCharsDefault = "abcdefghijklmnopqrstuvwxyz";
    private string upperCaseCharsDefault = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string allCaseCharsDefault = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string lettersAndNumbers = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private string chars1337Default = "4bcd3fgh1jklmn0pqr5tuvwxyz4BCD3FGH1JKLMN0PQR5TUVWXYZ";
    private string numbersDefault = "1234567890";
    private string specialCharsDefault = "!@#$%^&*()";

    const string SPCHAR = "Special character";
    const string CAPLETTER = "Capital letter";
    const string SMLETTER = "Small letter";
    const string MXLETTER = "Mixed case";
    const string LETTERNUMBER = "Letters+numbers";
    const string L33T = "1337+letters";
    const string NUMBER = "Number";
    const string PERIOD = "Period";

    private bool helpActive;
    private string generatedPassword;

    private void Start()
    {
        helpPanel.SetActive(false);
        helpActive = false;
        inputField.characterLimit = 22;
        CheckMode();
    }
    public void GenerateBody()
    {
        int i = 0;
        bodyLengthTxt = bodyLengthDropdown.options[bodyLengthDropdown.value].text;
        Debug.Log(bodyLengthTxt);
        int bodySize = int.Parse(bodyLengthTxt);
        Debug.Log(bodySize);
        while (i < bodySize)
        {
            bodyContentTxt = bodyContentTxt + GenerateBodyCharacter();
            i += 1;
            Debug.Log(i);
        }
    }
    public void GeneratePrefix()
    {
        if (prefixDropdown.options[prefixDropdown.value].text == SPCHAR)
        {
            prefixTxt = GetRandomCharacter(specialChars).ToString();

        }
        else if (prefixDropdown.options[prefixDropdown.value].text == CAPLETTER)
        {
            prefixTxt = GetRandomCharacter(upperCaseChars).ToString();
        }
        else if (prefixDropdown.options[prefixDropdown.value].text == SMLETTER)
        {
            prefixTxt = GetRandomCharacter(lowerCaseChars).ToString();
        }
        else
        {
            prefixTxt = GetRandomCharacter(numbers).ToString();
        }
    }
    public string GenerateBodyCharacter()
    {
        if (bodyContentDropdown.options[bodyContentDropdown.value].text == CAPLETTER)
        {
            bodyChar = GetRandomCharacter(upperCaseChars).ToString();
            Debug.Log(bodyChar);
            return bodyChar;
        }
        else if (bodyContentDropdown.options[bodyContentDropdown.value].text == SMLETTER)
        {
            bodyChar = GetRandomCharacter(lowerCaseChars).ToString();
            Debug.Log(bodyChar);
            return bodyChar;
        }
        else if (bodyContentDropdown.options[bodyContentDropdown.value].text == MXLETTER)
        {
            bodyChar = GetRandomCharacter(allCaseChars).ToString();
            Debug.Log(bodyChar);
            return bodyChar;
        }
        else if (bodyContentDropdown.options[bodyContentDropdown.value].text == L33T)
        {
            bodyChar = GetRandomCharacter(chars1337).ToString();
            return bodyChar;
        }
        else
        {
            bodyChar = GetRandomCharacter(lettersAndNumbers).ToString();
            return bodyChar;
        }
    }
    
    public void GenerateSuffix()
    {
        if (suffixDropdown.options[suffixDropdown.value].text == NUMBER)
        {
            suffixTxT = GetRandomCharacter(numbers).ToString();

        }
        else if (suffixDropdown.options[suffixDropdown.value].text == SPCHAR)
        {
            suffixTxT = GetRandomCharacter(specialChars).ToString();
        }
        else if (suffixDropdown.options[suffixDropdown.value].text == PERIOD)
        {
            suffixTxT = ".";
        }
        else
        {
            suffixTxT = "";
        }
    }
    public void GenerateFinalChar()
    {
        if (finalCharDropdown.options[finalCharDropdown.value].text == NUMBER)
        {
            finalCharTxt = GetRandomCharacter(numbers).ToString();

        }
        else if (finalCharDropdown.options[finalCharDropdown.value].text == SPCHAR)
        {
            finalCharTxt = GetRandomCharacter(specialChars).ToString();
        }
        else if (finalCharDropdown.options[finalCharDropdown.value].text == PERIOD)
        {
            finalCharTxt = ".";
        }
        else
        {
            finalCharTxt = "";
        }
    }
    public void GeneratePassword()
    {
        if (autoModeToggle.isOn)
        {
            ClearVariables();
            GeneratePrefix();
            GenerateBody();
            GenerateSuffix();
            GenerateFinalChar();
            ConstructPassword();
            outputPanel.text = "Prefix: " + prefixTxt + "\n" + "Body: " + bodyContentTxt
                                 + "\n" + "Suffix: " + suffixTxT + "\n" + "Final character: " + finalCharTxt
                                 + "\n" + "Generated password: " + generatedPassword;
        }
        else
        {
            ClearVariables();
            GetInputText();
            GeneratePrefix();
            GenerateSuffix();
            GenerateFinalChar();
            ConstructPassword();
            outputPanel.text = "Prefix: " + prefixTxt + "\n" + "Input password: " + inputPasswordBase
                                 + "\n" + "Suffix: " + suffixTxT + "\n" + "Final character: " + finalCharTxt
                                 + "\n" + "Generated password: " + generatedPassword;
        }

    }
    private void ConstructPassword()
    {
        if (autoModeToggle.isOn)
        {
            generatedPassword = prefixTxt + bodyContentTxt + suffixTxT + finalCharTxt;
            DisplayFinalPassword();
        }
        else
        {
            generatedPassword = prefixTxt + inputPasswordBase + suffixTxT + finalCharTxt; ;
            DisplayFinalPassword();
        }
    }
    public void DisplayFinalPassword()
    {
        finalPasswordDisplay.text = generatedPassword;
    }
    public void L33tChars(string value)
    {
        string newpassword = generatedPassword;
        if (value == "a" || value == "all")
        {
            newpassword = generatedPassword.Replace('a', '4');
            newpassword = newpassword.Replace('A', '4');
        }
        if (value == "e" || value == "all")
        {
            newpassword = newpassword.Replace('e', '3');
            newpassword = newpassword.Replace('E', '3');
        }
        if (value == "i" || value == "all")
        {
            newpassword = newpassword.Replace('i', '1');
            newpassword = newpassword.Replace('I', '1');
        }
        if (value == "g" || value == "all")
        {
            newpassword = newpassword.Replace('g', '6');
            newpassword = newpassword.Replace('G', '6');
        }
        if (value == "o" || value == "all")
        {

            newpassword = newpassword.Replace('o', '0');
            newpassword = newpassword.Replace('O', '0');
        }
        if (value == "s" || value == "all")
        {
            newpassword = newpassword.Replace('s', '5');
            newpassword = newpassword.Replace('S', '5');
        }
        if (value == "t" || value == "all")
        {
            newpassword = newpassword.Replace('t', '7');
            newpassword = newpassword.Replace('T', '7');
        }
        generatedPassword = newpassword;
        DisplayFinalPassword();
    }
    public void RNGSpecialChars()
    {
        int random = Random.Range(0, generatedPassword.Length);

        char[] passwordAsChars = generatedPassword.ToCharArray();
        passwordAsChars[random] = GetRandomCharacter(specialChars);

        generatedPassword = new string(passwordAsChars);
        DisplayFinalPassword();
    }
    public void SplitInTheMid()
    {
        int mid = generatedPassword.Length/2;
        char period = '.';

        char[] passwordAsChars = generatedPassword.ToCharArray();
        passwordAsChars[mid] = period;

        generatedPassword = new string(passwordAsChars);
        DisplayFinalPassword();
    }
    private void ClearVariables()
    {
        inputPasswordBase = null;
        prefixTxt = null;
        bodyChar = null;
        bodyContentTxt = null;
        suffixTxT = null;
        finalCharTxt = null;
        generatedPassword = null;
    }
    private char GetRandomCharacter(string charGroup)
    {
        char rngChar = charGroup[Random.Range(0, charGroup.Length)];
        return rngChar;
    }
    public string GetInputText()
    {
        inputPasswordBase = inputField.text;
        return inputPasswordBase;
    }
    public void CheckMode()
    {
        if (autoModeToggle.isOn)
        {
            inputField.enabled = false;
            inputPlaceholder.text = "Auto mode, text entry unavailable.";
            bodyLengthDropdown.gameObject.SetActive(true);
            bodyContentDropdown.gameObject.SetActive(true);
        }
        else
        {
            inputField.enabled = true;
            inputPlaceholder.text = "Enter password base (max 22 chars)...";
            bodyLengthDropdown.gameObject.SetActive(false);
            bodyContentDropdown.gameObject.SetActive(false);
        }
    }
    public void ToggleHelp()
    {
        if (helpActive == true)
        {
            helpPanel.SetActive(false);
            helpActive = false;
        }
        else if (helpActive == false)
        {
            helpPanel.SetActive(true);
            helpActive = true;
        } else
        {
            return;
        }
    }    public void CopyToClipboard()
    {
        if (generatedPassword != null)
        {
            //TODO: for mobile apps, minimize app after password copied? maybe toggole option
            GUIUtility.systemCopyBuffer = generatedPassword;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
