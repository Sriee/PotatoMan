# PotatoMan

2D Game from Unity with support for speech controls  

![alt Title](https://cloud.githubusercontent.com/assets/8402606/13778438/c92d7020-ea83-11e5-9d53-5953244ff622.png)

## Getting Started 

### Prerequisities 
 * .Net Framework installed 
 * Sufficient memory to store and run the program 
 * Works **_only_** with Windows OS (Windows 7, 8/8.1 and 10)
 * Head phones should the connected to your PC/Desktop/etc. 

### Configuring grammar.txt 
1. The words supported for the game has been added to the file
2. Default word to terminate the program is *Exit*
3. Flags can be used to 
	 - Enable or Disable speech recognition
	 - Change the terminating word 
4. Usage 
	- -E Enable  - Enables Speech Recognition   
	- -E Disable - Disable Speech Recognition  
	- -T End     - Sets End as the terminating word for the recognizer  
	- !- Comment - Use this to add any comments in the grammar  

###Steps to run the file
1. grammar.txt file should be present in the same folder where PotatoMan.exe and 
   SpeechKeyboard.exe. 
2. Double click PotatoMan.exe to start the game. When the game starts you will 
   have two windows opened. One is the game and the other one is the speech 
   recognition console application (As shown in figure Fire_Event_Working.png). 
3. Loosing the game won't close the console application. Either say *Exit* or 
   close the application manually.

## Licence 

This project is licensed under the GPL License - see the [LICENCE](../master/LICENSE) file for details

## Acknowledgements 

This is a special project for me. My C# skills were little rusty, so I took this challenging project of integrating speech recognition to Unity Engine. I had spend just 24 hours to complete the entire project setup. 
