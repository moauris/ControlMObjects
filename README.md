# Control-M Manager (Ceased)

*This project is now ceased as of 2020-08-27*

Started as an educational and experimental project, the Control-M Manager Project was designed to synchronize user's request to a local database.

## Node Viewer Demo

![](.\ControlM_Manager_GUI\Docs\Project_Readme_demo1.jpg)

Validation rules applied in another version of the form.

Cluster info need to be compiled to x64 to be able to read from the database which contained the information on OS.

## Issues

I have encountered several issues on the way of developing this app. 

- Database compatibility

For development, I used Microsoft Access. In the production environment a database is already in being used to record data. However, building the WPF app by using ACE OLEDB driver in the development environment doesn't mean it can be used in production.

It would also mean to alter the old database schema, making other related project, such as the Excel Macro Synchronizer obsolete, rendering it useless.

- Inconsistent Validation and Formatting

As a beginner WPF programmer, I learn the way of validating and formatting every day, and each time there is a new technology presented, I think of integrating it into the project.

For the same educational purpose, I can do with other practice sets and examples from Microsoft Documentations site just fine.

## Aftermath

The project stays, it can still run in the environment. It shall serve as an example for future development tasks to come.




