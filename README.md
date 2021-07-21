# BowlingExercise
::Bowling Exercise for StrongMind by Nicole Babos::

A WinForms application written in C# using Visual Studio 2019 and the .NET Core 3.1 framework. 

:Architecture:
I leveraged User Controls programmatically embedded on the Main Form for scalability.
The user controls handle validation, data entry, and displaying the score for each frame.
I developed a static GameManager class to handle the rules and logic for the game of bowling.

:Design:
I applied a nostalgiac retro design that felt like an old school bowling alley. 
The minimum resolution target is 1600x1200. I designed this application with a fixed resolution for pixel precision.

:Usage:
The application allows you to add up to four (4) players and remove players once added.

The application allows you to enter your score using numeric entries, X (for a strike), and / (for a spare). 
You can only earn 10 points per frame, thus the application will prevent you from adding a character if the two values added together are over 10 points. 
(ie. You record/knock down 9 pins in the first shot. In the next shot, you try to enter 2. The application will prevent you from entering 2 since 9+2 > 10 and you cannot knock down more than 10 pins per frame.)
The Instruction Widget to the right of each score record will communicate the valid inputs to the score keeper.


::Assignment Information::
Criteria: As a bowler, I want to be able to enter a list of bowling frame scores so that I can see the progress and total score of my game.  
•  I will have a place to enter a series of scores per frame. 
•  A strike is scored as defined below. 
•  A spare is scored as defined below. 
•  An incomplete game is scored up to the last frame provided. 

--Definitions--
::Frame:: 
A frame consists of 2 opportunities to knock down 10 bowling pins with a bowling ball.  The 10 pins are then reset for the next frame.    
 
--How to Score--
::Strike:: 
If you knock down all 10 pins in the first shot of a frame, you get a strike.   
A strike earns 10 points plus the sum of your next two shots. 
::Spare:: 
If you knock down all 10 pins using both shots of a frame, you get a spare.  A spare earns 10 points plus the sum of your next one shot. 
::Open Frame::
If you do not knock down all 10 pins using both shots of your frame (9 or fewer pins knocked down), you have an open frame.  
An open frame only earns the number of pins knocked down. 
::The 10th Frame:: 
The 10th frame is a bit different. If you roll a strike in the first shot of the 10th frame, you get 2 more shots.  
The score for the 10th frame is the total number of pins knocked down in the 10th frame. 
If you roll a spare in the first two shots of the 10th frame, you get 1 more shot.  
If you leave the 10th frame open after two shots, the game is over and you do not get an additional shot.  
