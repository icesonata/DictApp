# DictApp
An English - Vietnamese dictionary.\
Final project of NT106.K21.ANTN

# Prerequisite
- SQL Server
- Visual Studio 2015 or newer
- .NET Framework 4.6 or newer
- System.Text.Json namespace
- System.Data.SqlClient namespace

# Table of Contents
- [Manual](#manual)
- [Note](#note)
- [Proxy](#proxy)

# Manual
## Configuration
- Run CREATE.sql in Database to create new database on your machine (You must ensure the database named Dictionary created in your local machine).
- Run application following these steps:
1. Run Dictionary Server form named ServerDictApp and click "Start Server".
2. Run Proxy Server form named ProxyDictApp and click "Turn on".
3. Run Client form named ClientDictApp and log in to use the application.

## CODE field description
Meaning of number of CODE field in Data class.
| CODE  | Description |
| ----- | -----       |
| 100   | Authentication request (login) |
| 102   | Authentication succeed |
| 104   | Authentication request failed  |
| 200 | Registration request |
| 202 | Registration succeed |
| 204 | Registration failed |
| 300 | Translation request |
| 302 | Translation succeed |
| 304 | Translation failed |
| 400 | New connection request |
| 402 | New connection established successfully |
| 404 | New connection was failed to established |
----------
- CODE values 40X are for expansion feature Load Balancing.
- User's translation history is stored locally at User's machine in an Excel file.
- CODE 300 indicates both found and not found cases.

# Proxy
- Transparent proxy

# Note
- The application is for learning and experiment purposes.
- The application are not optimal enough to avoid wasting your machine's resources while running. (but clean afterward)
- Please ignore any Exception pop up due to closing connection or application