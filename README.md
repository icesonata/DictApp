# DictApp
Big-3 Team - Final project - NT106.K21.ANTN\
An English - Vietnamese dictionary application.

# Prerequisite
Those complements and environments below are used in building this application.
- SQL Server 18
- Visual Studio 2019
- .NET Framework 4.6
- System.Text.Json namespace
- System.Data.SqlClient namespace

# Table of Contents
- [Manual](#manual)
- [Proxy](#proxy)
- [Load Balance](#load-balance)
- [Note](#note)

# Manual
## Configuration and running application
- Run CREATE.sql in Database to create new database on your machine (You must ensure the database named Dictionary created in localhost server of your local machine).
- Manual running application following these steps:
1. Run Server with ServerDictApp.
2. Set your manual configuration such as IP to the server (optional).
3. Run client with ClientDictApp.
4. Register and log in at the client to experience.

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
- CODE values are personal convention.

# Proxy
- Transparent proxy
- Proxy model applies only for one server model which means only one server is used.

# Load Balance
- Load balancer is designed for model consist of 2 servers.
- Load Balancer doesn't do neither encryption nor decryption.
- Run at least 2 server by simply executing sln file by using Ctrl + F5 before starting running Load Balancer. Require a server has localhost address with port 8888 and one has locahost address with port 9000.

# Note
- The application is for learning and experiment purposes. And I will not hide any error by using try-catch statement.
- The application are not optimal enough to avoid wasting your machine's resources only while running.
- The application has not handle enough error cases yet, especially cleaning stage afterward.
- Note that by default client will connect to server application at port 8080 so the proxy server and load balancing server are set with port 8080 to suit client's preconfiguration. Make sure to consider whether your machine's ports are available or not. You can change the code to suit your machine depended on your flexibility.  
- Note that if you don't configure capacity at Server site, it will be automatically set by default value which is equal to 2. 
- If you want to reset Excel local database file, remember to reset index.txt in Records directrory to "1    0" afterward. (there's a tab between 1 and 0)
- This application belongs to Big-3 team including:
    - **Nguyen Hoang Long**
    - **Nguyen Thanh Tien**
    - **Trinh Huynh Trong Nhan**
