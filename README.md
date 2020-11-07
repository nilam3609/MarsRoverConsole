# MarsRoverConsole
Cardano Assignment:-
This repository includes solution to the problem statement linked to the assignment.  
Problem Statement :

5 5   
1 2 N    
LML  

The “5 5” part of the string that indicates the size of the plateau.  “1 2 N” indicates that the rover is positioned on grid square 1,2 and is pointing north.  So, if the rover moved, it would move in the direction it was facing, in this case north.  The last part of the command is the movement sequence.  “LML” means, ‘turn left, move, turn left’.

Additionally, the rover must not drive off the plateau. Should a human error occur, e.g. should an operator try to drive the multi-million-dollar rover off the plateau, the rover should stop and await rescue.  
There can also be two or more rovers, in which case the instructions for all rovers will be sent.  Of course, the size of the exploration plateau will be the same.  For example, the command below will instruct two rovers on the 5 by 5 plateau:

5 5  
1 2 N  
LML  
3 3 E    
MMR  

# Solution: 
This project is created using C# .NET Core Console Application. As this assingment doesnt require to have an UI, I have created solution as a .NET Core Console App.  
Prerequisite:
Visual Studio 2019, .NET Core SDK 3.xx 
Architecture: 
1) MarsRoverConsole: This is the entry point of the application and contains basic setup of Services(Dependency Injection).
2) MarsRover.Manager: This project includes Implementations and interfaces. There are in total 3 services which are used to cover all the implementation. 
3) MarsRover.Model: This project contains all the models and enums used in the application.
4) MarsRover.UnitTest: This project contains all the unit tests to cover piece of code in MarsRover.Manager  


# Assumption: 
* Assuming the below image as the cordinates for plateau to solve control the mars rover from earth: 
If (x,y) is the bottom position (0,0) then (x,y+n) is the direction to North, (x+n,y) is the direction to East.
![alt text](https://s3-us-west-2.amazonaws.com/courses-images-archive-read-only/wp-content/uploads/sites/924/2015/09/25200243/CNX_CAT_Figure_02_01_003.jpg)

* Assuming the size of plateaut if 5X5 and rover is deployed at 1 2 N and is successfully controlled from earth to move LML the final postion of rover would end up to cordinates 0 2 S.

* Assuming that multiple rovers are deployed at the same time, if one of rover fails to deploy control center should be able to continue deployment for rest of the rovers.

* Assuming the user sending the commands follows the correct sequence of command. 

* Assuming the initial part of problem only expects Output without selection of different rovers while running console app. Although, it is possible to control different rovers using commands if we expand the program.

