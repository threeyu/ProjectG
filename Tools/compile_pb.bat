@echo off

protoc --csharp_out=./../UnityProject/Assets/Scripts/PB -I ./../DataProject/Protocols/ ./../DataProject/Protocols/battle/*.proto
protoc --csharp_out=./../UnityProject/Assets/Scripts/PB -I ./../DataProject/Protocols/ ./../DataProject/Protocols/command/*.proto
protoc --csharp_out=./../UnityProject/Assets/Scripts/PB -I ./../DataProject/Protocols/ ./../DataProject/Protocols/lobby/*.proto
protoc --csharp_out=./../UnityProject/Assets/Scripts/PB -I ./../DataProject/Protocols/ ./../DataProject/Protocols/travel/*.proto

echo Success
pause
