co=2;
s='name';
print(co);
print(s)

co,c,d =1,2,4

print(co);
print(c)
print(d)
if 11==1:
    print('2')
else :
    print('22')
#!/usr/bin/python
# -*- coding: UTF-8 -*-
#while True:
import win32com.client as win

speak = win.Dispatch("SAPI.SpVoice")
speak.Speak("come on")
speak.Speak("你好")


import pyttsx3
engine = pyttsx3.init()
engine.say("Good")
engine.runAndWait()
