**DESIGN CONSIDERATIONS**

**Speech Recognition**  
    There are two options to include speech recognition functionality into the game   
   1.	Write the speech recognition algorithm yourself to get speech inputs and use Fourier Transforms to detected the words.   
   2.	Use Speech Recognition Application Programming Interface (API) which takes voice inputs and detects the words.     

The second option was the most promising one. Having said that next we need to choose what API to use (Yes! there are many)   

**Mode of Speech Recognition**    
    The Speech Recognition API comes in two forms, as a native application or like a web interface.   

* *Native Application* – Has the speech engine in your desktop environment where you can use them like any system resources.   
* *Web Interface* – The speech recognition engine is running at the web server and clients have to send voice inputs which will be  processes by the server. The server presents the detected words to the client. (Yes! it requires network connection)  

**Choice of Speech Recognition API’s**  

  1.	Google API  
  2.	ATT Speech API  (Needs premium access $99 per month)  
  3.	Nuance Voice Recognition (Free for one month)   
  4.	CMUSphinix (Open source - developed my CMU Research Team)  
  5.	Using Inbuilt Windows API     

**Windows API for Unity**  

I choose Windows API because it is a native interface and Unity game engine supports C# scripts for handling events in the game. Another advantage is that it is not confined to Windows environment, we can add plugins to make them run on other environment as well.

**Why I have UDP for Inter Process Communication (IPC)?**  

Usually in C# we use pipes as inter process communication for communicating between two console applications but the MonoBehavior compiler of Unity game engine is not allowing pipe for communicating between two processes which made me to choose UDP.

**What is *‘grammar.txt’*? Why is it required?**  

*‘grammar.txt’* is the vocabulary reference for the speech recognition engine. It contains the word list that will be used to detect the words for your application. It is an **important** part of the application, you can’t skip this file. You have to add words that will be used in your application.

**How the ‘grammar.txt’ file should be?**  

Grammar file has two sections, First section is the place where users can specify the flags required for the program. This will change according to their system environment. The below image shows a sample of how a grammar file should be 

  ![alt Image](https://cloud.githubusercontent.com/assets/8402606/13730742/f7873df2-e925-11e5-9e0e-b8e780ddd026.GIF)
  
_Support flags – *[P, I, T, E]*_
***
|Flags| Description                                                          |
|-----| :------------------------------------------------------------------: |
| --P | Specify the port number [Optional]                                   |
| --I | IP address of the server [Should change | Default: *192.168.0.20*]   |
| --T | Terminating word. [Optional | Default: *Exit*]                       |
| --E | Enable or Disable speech Recognition. [Optional | Default: *Enable*] |
| --  | This is a Comment, there **should** be space after **--**            |

**_Note:_** 'grammar.txt' file should be in the same folder as the *.exe* file

**Why my voice is not getting detected correctly?**

It is better to try to train your voice to make the windows environment as shown below. It will increase your quality of word detection.  

![alt Training](https://cloud.githubusercontent.com/assets/8402606/13731060/81c69816-e92e-11e5-9474-42e57a1c4d3d.gif)

  
