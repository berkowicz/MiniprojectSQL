# MiniprojectSQL

This is a time registration application where the user can register time to sertain project. <br>
The user can also add new users & projects, change users & projects.

![MiniProjectSQLRecording](https://user-images.githubusercontent.com/112638774/224285770-bf96c119-c0d0-437f-bafa-ba8dc92cc372.gif)

## Introduction to the code

 - **Program.cs** Prints the main menu
 - **Menusystem.cs** Contains the methods that prints and handles input to the menu.
 - **Methods.cs** Contains the methods called uppon in the menu.
 - **DataAccess.cs** Contains all the connectionstrings talking to the DB.
 - **...Model.cs** Is objects to store data from the DB.


## Changes
The program fetch the entire DB when trying to load data. I've tried to make is so it only fetch data from the specific input the user uses but wasn't able to make it work (As you can see in the commented code). This is something I like to make possible.
