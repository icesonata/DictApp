# DictApp
Big-3 - Final project - NT106 Introduction to Computer Network Programming\
An English - Vietnamese dictionary application.

# Prerequisite
Those complements and environments below, which require manual installation, are used in building this application. To avoid errors and bugs, user should consider to install packets/framework/applications listed below in the same or later version.
- SQL Server 18
- Visual Studio 2019
- .NET Framework 4.6
- System.Text.Json namespace
- System.Data.SqlClient namespace

# Table of Contents
- [Manual](#manual)
- [Proxy](#proxy)
- [Load Balance](#load-balance)
- [Storage](#storage)
- [Note](#note)

# Manual
## Configuration and running application
- Run CREATE.sql in Database to create new database on your machine (You must ensure the database named Dictionary created in localhost server of your local machine).
- Manually run the application following steps below:
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
- 40X and 50X are for load balancer.
- CODE 302 indicates whether the word requested is found in the database or not.

## Storage
- Translation history of each user is stored locally at User's machine in an Excel file, which can be found in *<Path-to-directory>/DictApp/ClientDictApp/records/*.

# Proxy
- Proxy type: Transparent proxy
- Proxy model applies only for one server model which means only one server is used at a time.

# Load Balance
- Designed for a topology consisting of 2 servers and clients.
- Requires at least 2 servers in running state to execute, there are two ways to create multiple server from a server source code are either executing *.sln* file using Ctrl + F5 or running server's *.exe* file in */DictApp/ServerDictApp/ServerDictApp/bin/Debug* or */DictApp/ServerDictApp/ServerDictApp/bin/Release* depending on which type of running we conducted as many time as number of servers we need. For *Debug* or *Release* folder to existed, user must run the application once with the our corresponding *Active solution configuration* option in Visual Studio.
- Requires at least 2 servers running with port 8888 and 9000 at simultaneously, moreover, we can change this configuration in the source code of the load balancer.

# Note
- After closing the application, there might be some bugs come up due to being turned off suddenly of the sockets. These bugs do not cause any harm to either the computer or the application hence we could ignore them. We can uncomment try-catch statement to avoid these bugs popping up. However, it is not a good practice to ignore or hide bugs.
- The application has not been optimized hence leading to huge consumption in computer resource.
- Note that by default client will connect to server application at port 8080 so the proxy server and load balancing server are set with port 8080 to suit client's preconfiguration. Thus, make sure the required ports for this application are available on our machine or we can change it directly in the source code.
- Capacity of server has a default value which is up to 2 clients are handled by the server. 
- To delete translation history, delete all data except the header in the Excel file locating in *<Path-to-directory>/DictApp/ClientDictApp/records/*, also reset *index.txt*'s content in the same directory to "1   0" (there is a tab between 1 and 0).

Big-3 members:
- **Nguyen Hoang Long**
- **Nguyen Thanh Tien**
- **Trinh Huynh Trong Nhan**
