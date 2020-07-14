# DictApp
Final project of NT106.K21.ANTN\
An English - Vietnamese dictionary.\

# Prerequisite
- SQL Server
- Visual Studio 2015 or newer
- .NET Framework 4.6 or newer
- System.Text.Json namespace
- System.Data.SqlClient namespace

# Table of Contents
- [Manual](#manual)
- [Proxy](#proxy)
- [Load Balance](#load-balance)
- [Note](#note)

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
| 400 | Ask server to handle new client |
| 402 | Server can handle new client |
| 404 | Server can't handle new client |
| 500 | A client has quitted |
| 502 | Server has a new slot to handle new client |
----------
- CODE which has value 40X or 50X are for Load Balancing feature.
- User's translation history is stored locally at User's machine in an Excel file.
- CODE 302 indicates both found and not found cases to tell Client site.

# Proxy
- Transparent proxy
- Proxy model applies only for one server model which means only one server is used. You should use Dictionary server 0 instead.

# Load Balance
- Load balancer is designed for model consist of 2 servers.
- Please run both dictionary server 0 and 1 before running Load Balancer.

# Note
- The application is for learning and experiment purposes.
- The application are not optimal enough to avoid wasting your machine's resources while running. (but clean afterward)
- Please ignore any Exception pop up due to closing connection or application.
- CODE values are personal convention.
- The application has not handle enough error cases yet.
- Load Balancer doesn't have encryption.
- If you only want to experiment simple client-server on this dictionary, go to Global class on Dictionary server 0 or Dictionary server 1, comment line of code which initialize server's port 8888 (9000 to server 1) and uncomment line of code initalize server's port 8080. After that, run the server you have just modified click on start button and run client site.
- If you want to reset Excel local database file, remember to reset index.txt in Records directrory to "1    0" afterward. (there's a tab between 1 and 0)
- There are still plenty of errors and bugs while closing connections between client and server.