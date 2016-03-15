# DESIGN CONSIDERATIONS

### Implementation

The Implementation of my project is as shown in the figure below. 

 ![alt Image](https://cloud.githubusercontent.com/assets/8402606/13730742/f7873df2-e925-11e5-9e0e-b8e780ddd026.GIF)
 
Game contains two application, first one is the Unity application which runs on top of Unity Game Engine and the other one is a console application.

Console Application is responsible for implementing speech recognition functionality. It loads the vocabulary list from grammar.txt file into the speech recognition engine. It uses the default audio device to takes voice inputs from users, the voice inputs are then processed to texts. This console application uses Windows API  for making this conversion possible. 

After converting the user’s voice inputs these inputs are emulated as keyboard events based on the events required by the game. Keyboard events are emulated for Jump, Left (Looks left), Right (Looks right), Move Left, Move Right, Bomb and Fire controls. As, Unity game engine uses keyboard inputs for handling the user controls, emulating similar events triggers the present implementations. By doing so, we can accomplish integrating voice recognition to unity game engine.   

### Features 
 * Configure the grammar.txt with flags to change the terminating word and enabling/disabling the speech recognizer 
 * Responds to voice inputs without any lag 
 * Game can work as a standalone application in Windows platform 

### Speech Recognition
    There are two options to include speech recognition functionality into the game   
   1.	Write the speech recognition algorithm yourself to get speech inputs and use Fourier Transforms to detected the words.   
   2.	Use Speech Recognition Application Programming Interface (API) which takes voice inputs and detects the words.     

The second option was the most promising one. Having said that next we need to choose what API to use (Yes! there are many)   

### Mode of Speech Recognition    
    The Speech Recognition API comes in two forms, as a native application or like a web interface.   

* *Native Application* – Has the speech engine in your desktop environment where you can use them like any system resources.   
* *Web Interface* – The speech recognition engine is running at the web server and clients have to send voice inputs which will be  processes by the server. The server presents the detected words to the client. (Yes! it requires network connection)  

### Choice of Speech Recognition API’s

  1.	Google API  
  2.	ATT Speech API  (Needs premium access $99 per month)  
  3.	Nuance Voice Recognition (Free for one month)   
  4.	CMUSphinix (Open source - developed my CMU Research Team)  
  5.	Using Inbuilt Windows API     

#### Windows API for Unity 

I choose Windows API because it is a native interface and Unity game engine supports C# scripts for handling events in the game. Another advantage is that it is not confined to Windows environment, we can add plugins to make them run on other environment as well.

#### What is *‘grammar.txt’*? Why is it required?  

*‘grammar.txt’* is the vocabulary reference for the speech recognition engine. It contains the word list that will be used to detect the words for your application. It is an **important** part of the application, you can’t skip this file. You have to add words that will be used in your application.

#### How the ‘grammar.txt’ file should be?  

Grammar file has two sections, First section is the place where users can specify the flags required for the program. This will change according to their system environment. The below image shows a sample of how a grammar file should be 

  ![alt Image](https://cloud.githubusercontent.com/assets/8402606/13730742/f7873df2-e925-11e5-9e0e-b8e780ddd026.GIF)
  
_Support flags – *[T, E]*_
***
|Flags| Description                                                          |
|-----| :------------------------------------------------------------------: |
| --T | Terminating word. [Optional | Default: *Exit*]                       |
| --E | Enable or Disable speech Recognition. [Optional | Default: *Enable*] |
| !-  | This is a Comment, there **should** be space after **--**            |

**_Note:_** 'grammar.txt' file should be in the same folder as the *.exe* file

#### Why my voice is not getting detected correctly?

It is better to try to train your voice to make the windows environment as shown below. It will increase your quality of word detection.  

![alt Training](https://cloud.githubusercontent.com/assets/8402606/13731060/81c69816-e92e-11e5-9474-42e57a1c4d3d.gif)

  
